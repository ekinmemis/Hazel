using Hazel.Core;
using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Services.Events;
using Hazel.Services.Localization;
using Hazel.Services.Security;
using System;
using System.Linq;

namespace Hazel.Services.ApplicationUsers
{
    /// <summary>
    /// ApplicationUser registration service.
    /// </summary>
    public partial class ApplicationUserRegistrationService : IApplicationUserRegistrationService
    {
        /// <summary>
        /// Defines the _applicationUserSettings.
        /// </summary>
        private readonly ApplicationUserSettings _applicationUserSettings;

        /// <summary>
        /// Defines the _applicationUserService.
        /// </summary>
        private readonly IApplicationUserService _applicationUserService;

        /// <summary>
        /// Defines the _encryptionService.
        /// </summary>
        private readonly IEncryptionService _encryptionService;

        /// <summary>
        /// Defines the _eventPublisher.
        /// </summary>
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Defines the _localizationService.
        /// </summary>
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Defines the _workContext.
        /// </summary>
        private readonly IWorkContext _workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserRegistrationService"/> class.
        /// </summary>
        /// <param name="applicationUserSettings">The applicationUserSettings<see cref="ApplicationUserSettings"/>.</param>
        /// <param name="applicationUserService">The applicationUserService<see cref="IApplicationUserService"/>.</param>
        /// <param name="encryptionService">The encryptionService<see cref="IEncryptionService"/>.</param>
        /// <param name="eventPublisher">The eventPublisher<see cref="IEventPublisher"/>.</param>
        /// <param name="localizationService">The localizationService<see cref="ILocalizationService"/>.</param>
        /// <param name="workContext">The workContext<see cref="IWorkContext"/>.</param>
        public ApplicationUserRegistrationService(ApplicationUserSettings applicationUserSettings,
            IApplicationUserService applicationUserService,
            IEncryptionService encryptionService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IWorkContext workContext)
        {
            _applicationUserSettings = applicationUserSettings;
            _applicationUserService = applicationUserService;
            _encryptionService = encryptionService;
            _eventPublisher = eventPublisher;
            _localizationService = localizationService;
            _workContext = workContext;
        }

        /// <summary>
        /// Check whether the entered password matches with a saved one.
        /// </summary>
        /// <param name="applicationUserPassword">ApplicationUser password.</param>
        /// <param name="enteredPassword">The entered password.</param>
        /// <returns>True if passwords match; otherwise false.</returns>
        protected bool PasswordsMatch(ApplicationUserPassword applicationUserPassword, string enteredPassword)
        {
            if (applicationUserPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = string.Empty;
            switch (applicationUserPassword.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    savedPassword = enteredPassword;
                    break;
                case PasswordFormat.Encrypted:
                    savedPassword = _encryptionService.EncryptText(enteredPassword);
                    break;
                case PasswordFormat.Hashed:
                    savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, applicationUserPassword.PasswordSalt, _applicationUserSettings.HashedPasswordFormat);
                    break;
            }

            if (applicationUserPassword.Password == null)
                return false;

            return applicationUserPassword.Password.Equals(savedPassword);
        }

        /// <summary>
        /// Validate applicationUser.
        /// </summary>
        /// <param name="usernameOrEmail">Username or email.</param>
        /// <param name="password">Password.</param>
        /// <returns>Result.</returns>
        public virtual ApplicationUserLoginResults ValidateApplicationUser(string usernameOrEmail, string password)
        {
            var applicationUser = _applicationUserSettings.UsernamesEnabled ?
                _applicationUserService.GetApplicationUserByUsername(usernameOrEmail) :
                _applicationUserService.GetApplicationUserByEmail(usernameOrEmail);

            if (applicationUser == null)
                return ApplicationUserLoginResults.ApplicationUserNotExist;
            if (applicationUser.Deleted)
                return ApplicationUserLoginResults.Deleted;
            if (!applicationUser.Active)
                return ApplicationUserLoginResults.NotActive;
            //only registered can login
            if (!applicationUser.IsRegistered())
                return ApplicationUserLoginResults.NotRegistered;
            //check whether a applicationUser is locked out
            if (applicationUser.CannotLoginUntilDateUtc.HasValue && applicationUser.CannotLoginUntilDateUtc.Value > DateTime.UtcNow)
                return ApplicationUserLoginResults.LockedOut;

            if (!PasswordsMatch(_applicationUserService.GetCurrentPassword(applicationUser.Id), password))
            {
                //wrong password
                applicationUser.FailedLoginAttempts++;
                if (_applicationUserSettings.FailedPasswordAllowedAttempts > 0 &&
                    applicationUser.FailedLoginAttempts >= _applicationUserSettings.FailedPasswordAllowedAttempts)
                {
                    //lock out
                    applicationUser.CannotLoginUntilDateUtc = DateTime.UtcNow.AddMinutes(_applicationUserSettings.FailedPasswordLockoutMinutes);
                    //reset the counter
                    applicationUser.FailedLoginAttempts = 0;
                }

                _applicationUserService.UpdateApplicationUser(applicationUser);

                return ApplicationUserLoginResults.WrongPassword;
            }

            //update login details
            applicationUser.FailedLoginAttempts = 0;
            applicationUser.CannotLoginUntilDateUtc = null;
            applicationUser.RequireReLogin = false;
            applicationUser.LastLoginDateUtc = DateTime.UtcNow;
            _applicationUserService.UpdateApplicationUser(applicationUser);

            return ApplicationUserLoginResults.Successful;
        }

        /// <summary>
        /// Register applicationUser.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Result.</returns>
        public virtual ApplicationUserRegistrationResult RegisterApplicationUser(ApplicationUserRegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.ApplicationUser == null)
                throw new ArgumentException("Can't load current applicationUser");

            var result = new ApplicationUserRegistrationResult();
            if (request.ApplicationUser.IsSearchEngineAccount())
            {
                result.AddError("Search engine can't be registered");
                return result;
            }

            if (request.ApplicationUser.IsBackgroundTaskAccount())
            {
                result.AddError("Background task account can't be registered");
                return result;
            }

            if (request.ApplicationUser.IsRegistered())
            {
                result.AddError("Current applicationUser is already registered");
                return result;
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.EmailIsNotProvided"));
                return result;
            }

            if (!CommonHelper.IsValidEmail(request.Email))
            {
                result.AddError(_localizationService.GetResource("Common.WrongEmail"));
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.PasswordIsNotProvided"));
                return result;
            }

            if (_applicationUserSettings.UsernamesEnabled && string.IsNullOrEmpty(request.Username))
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.UsernameIsNotProvided"));
                return result;
            }

            //validate unique user
            if (_applicationUserService.GetApplicationUserByEmail(request.Email) != null)
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.EmailAlreadyExists"));
                return result;
            }

            if (_applicationUserSettings.UsernamesEnabled && _applicationUserService.GetApplicationUserByUsername(request.Username) != null)
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.UsernameAlreadyExists"));
                return result;
            }

            //at this point request is valid
            request.ApplicationUser.Username = request.Username;
            request.ApplicationUser.Email = request.Email;

            var applicationUserPassword = new ApplicationUserPassword
            {
                ApplicationUser = request.ApplicationUser,
                PasswordFormat = request.PasswordFormat,
                CreatedOnUtc = DateTime.UtcNow
            };
            switch (request.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    applicationUserPassword.Password = request.Password;
                    break;
                case PasswordFormat.Encrypted:
                    applicationUserPassword.Password = _encryptionService.EncryptText(request.Password);
                    break;
                case PasswordFormat.Hashed:
                    var saltKey = _encryptionService.CreateSaltKey(HazelApplicationUserServiceDefaults.PasswordSaltKeySize);
                    applicationUserPassword.PasswordSalt = saltKey;
                    applicationUserPassword.Password = _encryptionService.CreatePasswordHash(request.Password, saltKey, _applicationUserSettings.HashedPasswordFormat);
                    break;
            }

            _applicationUserService.InsertApplicationUserPassword(applicationUserPassword);

            request.ApplicationUser.Active = request.IsApproved;

            //add to 'Registered' role
            var registeredRole = _applicationUserService.GetApplicationUserRoleBySystemName(HazelApplicationUserDefaults.RegisteredRoleName);
            if (registeredRole == null)
                throw new HazelException("'Registered' role could not be loaded");
            //request.ApplicationUser.ApplicationUserRoles.Add(registeredRole);
            request.ApplicationUser.AddApplicationUserRoleMapping(new ApplicationUserApplicationUserRoleMapping { ApplicationUserRole = registeredRole });
            //remove from 'Guests' role
            var guestRole = request.ApplicationUser.ApplicationUserRoles.FirstOrDefault(cr => cr.SystemName == HazelApplicationUserDefaults.GuestsRoleName);
            if (guestRole != null)
            {
                //request.ApplicationUser.ApplicationUserRoles.Remove(guestRole);
                request.ApplicationUser.RemoveApplicationUserRoleMapping(
                    request.ApplicationUser.ApplicationUserApplicationUserRoleMappings.FirstOrDefault(mapping => mapping.ApplicationUserRoleId == guestRole.Id));
            }


            _applicationUserService.UpdateApplicationUser(request.ApplicationUser);

            return result;
        }

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Result.</returns>
        public virtual ChangePasswordResult ChangePassword(ChangePasswordRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var result = new ChangePasswordResult();
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.EmailIsNotProvided"));
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.PasswordIsNotProvided"));
                return result;
            }

            var applicationUser = _applicationUserService.GetApplicationUserByEmail(request.Email);
            if (applicationUser == null)
            {
                result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.EmailNotFound"));
                return result;
            }

            //request isn't valid
            if (request.ValidateRequest && !PasswordsMatch(_applicationUserService.GetCurrentPassword(applicationUser.Id), request.OldPassword))
            {
                result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.OldPasswordDoesntMatch"));
                return result;
            }

            //check for duplicates
            if (_applicationUserSettings.UnduplicatedPasswordsNumber > 0)
            {
                //get some of previous passwords
                var previousPasswords = _applicationUserService.GetApplicationUserPasswords(applicationUser.Id, passwordsToReturn: _applicationUserSettings.UnduplicatedPasswordsNumber);

                var newPasswordMatchesWithPrevious = previousPasswords.Any(password => PasswordsMatch(password, request.NewPassword));
                if (newPasswordMatchesWithPrevious)
                {
                    result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.PasswordMatchesWithPrevious"));
                    return result;
                }
            }

            //at this point request is valid
            var applicationUserPassword = new ApplicationUserPassword
            {
                ApplicationUser = applicationUser,
                PasswordFormat = request.NewPasswordFormat,
                CreatedOnUtc = DateTime.UtcNow
            };
            switch (request.NewPasswordFormat)
            {
                case PasswordFormat.Clear:
                    applicationUserPassword.Password = request.NewPassword;
                    break;
                case PasswordFormat.Encrypted:
                    applicationUserPassword.Password = _encryptionService.EncryptText(request.NewPassword);
                    break;
                case PasswordFormat.Hashed:
                    var saltKey = _encryptionService.CreateSaltKey(HazelApplicationUserServiceDefaults.PasswordSaltKeySize);
                    applicationUserPassword.PasswordSalt = saltKey;
                    applicationUserPassword.Password = _encryptionService.CreatePasswordHash(request.NewPassword, saltKey,
                        request.HashedPasswordFormat ?? _applicationUserSettings.HashedPasswordFormat);
                    break;
            }

            _applicationUserService.InsertApplicationUserPassword(applicationUserPassword);

            //publish event
            _eventPublisher.Publish(new ApplicationUserPasswordChangedEvent(applicationUserPassword));

            return result;
        }

        /// <summary>
        /// Sets a user email.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="newEmail">New email.</param>
        /// <param name="requireValidation">Require validation of new email address.</param>
        public virtual void SetEmail(ApplicationUser applicationUser, string newEmail, bool requireValidation)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            if (newEmail == null)
                throw new HazelException("Email cannot be null");

            newEmail = newEmail.Trim();
            var oldEmail = applicationUser.Email;

            if (!CommonHelper.IsValidEmail(newEmail))
                throw new HazelException(_localizationService.GetResource("Account.EmailUsernameErrors.NewEmailIsNotValid"));

            if (newEmail.Length > 100)
                throw new HazelException(_localizationService.GetResource("Account.EmailUsernameErrors.EmailTooLong"));

            var applicationUser2 = _applicationUserService.GetApplicationUserByEmail(newEmail);
            if (applicationUser2 != null && applicationUser.Id != applicationUser2.Id)
                throw new HazelException(_localizationService.GetResource("Account.EmailUsernameErrors.EmailAlreadyExists"));
        }

        /// <summary>
        /// Sets a applicationUser username.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="newUsername">New Username.</param>
        public virtual void SetUsername(ApplicationUser applicationUser, string newUsername)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            if (!_applicationUserSettings.UsernamesEnabled)
                throw new HazelException("Usernames are disabled");

            newUsername = newUsername.Trim();

            if (newUsername.Length > HazelApplicationUserServiceDefaults.ApplicationUserUsernameLength)
                throw new HazelException(_localizationService.GetResource("Account.EmailUsernameErrors.UsernameTooLong"));

            var user2 = _applicationUserService.GetApplicationUserByUsername(newUsername);
            if (user2 != null && applicationUser.Id != user2.Id)
                throw new HazelException(_localizationService.GetResource("Account.EmailUsernameErrors.UsernameAlreadyExists"));

            applicationUser.Username = newUsername;
            _applicationUserService.UpdateApplicationUser(applicationUser);
        }
    }
}
