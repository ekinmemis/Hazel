using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Services.ApplicationUsers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Hazel.Services.Authentication
{
    /// <summary>
    /// Represents service using cookie middleware for the authentication.
    /// </summary>
    public partial class CookieAuthenticationService : IAuthenticationService
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
        /// Defines the _httpContextAccessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Defines the _cachedApplicationUser.
        /// </summary>
        private ApplicationUser _cachedApplicationUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="CookieAuthenticationService"/> class.
        /// </summary>
        /// <param name="applicationUserSettings">The applicationUserSettings<see cref="ApplicationUserSettings"/>.</param>
        /// <param name="applicationUserService">The applicationUserService<see cref="IApplicationUserService"/>.</param>
        /// <param name="httpContextAccessor">The httpContextAccessor<see cref="IHttpContextAccessor"/>.</param>
        public CookieAuthenticationService(ApplicationUserSettings applicationUserSettings,
            IApplicationUserService applicationUserService,
            IHttpContextAccessor httpContextAccessor)
        {
            _applicationUserSettings = applicationUserSettings;
            _applicationUserService = applicationUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Sign in.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests.</param>
        public virtual async void SignIn(ApplicationUser applicationUser, bool isPersistent)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            //create claims for applicationUser's username and email
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(applicationUser.Username))
                claims.Add(new Claim(ClaimTypes.Name, applicationUser.Username, ClaimValueTypes.String, HazelAuthenticationDefaults.ClaimsIssuer));

            if (!string.IsNullOrEmpty(applicationUser.Email))
                claims.Add(new Claim(ClaimTypes.Email, applicationUser.Email, ClaimValueTypes.Email, HazelAuthenticationDefaults.ClaimsIssuer));

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, HazelAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(HazelAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);

            _cachedApplicationUser = applicationUser;
        }

        /// <summary>
        /// Sign out.
        /// </summary>
        public virtual async void SignOut()
        {
            //reset cached applicationUser
            _cachedApplicationUser = null;

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(HazelAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Get authenticated applicationUser.
        /// </summary>
        /// <returns>ApplicationUser.</returns>
        public virtual ApplicationUser GetAuthenticatedApplicationUser()
        {
            //whether there is a cached applicationUser
            if (_cachedApplicationUser != null)
                return _cachedApplicationUser;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(HazelAuthenticationDefaults.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            ApplicationUser applicationUser = null;
            if (_applicationUserSettings.UsernamesEnabled)
            {
                //try to get applicationUser by username
                var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name
                    && claim.Issuer.Equals(HazelAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
                if (usernameClaim != null)
                    applicationUser = _applicationUserService.GetApplicationUserByUsername(usernameClaim.Value);
            }
            else
            {
                //try to get applicationUser by email
                var emailClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email
                    && claim.Issuer.Equals(HazelAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
                if (emailClaim != null)
                    applicationUser = _applicationUserService.GetApplicationUserByEmail(emailClaim.Value);
            }

            //whether the found applicationUser is available
            if (applicationUser == null || !applicationUser.Active || applicationUser.RequireReLogin || applicationUser.Deleted || !applicationUser.IsRegistered())
                return null;

            _cachedApplicationUser = applicationUser;

            return _cachedApplicationUser;
        }
    }
}
