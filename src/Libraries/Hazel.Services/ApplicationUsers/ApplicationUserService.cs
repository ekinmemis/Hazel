using Hazel.Core;
using Hazel.Core.Caching;
using Hazel.Core.Data;
using Hazel.Core.Data.Extensions;
using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Core.Domain.Common;
using Hazel.Core.Infrastructure;
using Hazel.Data;
using Hazel.Services.Common;
using Hazel.Services.Events;
using Hazel.Services.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace Hazel.Services.ApplicationUsers
{
    /// <summary>
    /// ApplicationUser service.
    /// </summary>
    public partial class ApplicationUserService : IApplicationUserService
    {
        /// <summary>
        /// Defines the _applicationUserSettings.
        /// </summary>
        private readonly ApplicationUserSettings _applicationUserSettings;

        /// <summary>
        /// Defines the _cacheManager.
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Defines the _dataProvider.
        /// </summary>
        private readonly IDataProvider _dataProvider;

        /// <summary>
        /// Defines the _dbContext.
        /// </summary>
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Defines the _eventPublisher.
        /// </summary>
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Defines the _genericAttributeService.
        /// </summary>
        private readonly IGenericAttributeService _genericAttributeService;

        /// <summary>
        /// Defines the _applicationUserRepository.
        /// </summary>
        private readonly IRepository<ApplicationUser> _applicationUserRepository;

        /// <summary>
        /// Defines the _applicationUserApplicationUserRoleMappingRepository.
        /// </summary>
        private readonly IRepository<ApplicationUserApplicationUserRoleMapping> _applicationUserApplicationUserRoleMappingRepository;

        /// <summary>
        /// Defines the _applicationUserPasswordRepository.
        /// </summary>
        private readonly IRepository<ApplicationUserPassword> _applicationUserPasswordRepository;

        /// <summary>
        /// Defines the _applicationUserRoleRepository.
        /// </summary>
        private readonly IRepository<ApplicationUserRole> _applicationUserRoleRepository;

        /// <summary>
        /// Defines the _gaRepository.
        /// </summary>
        private readonly IRepository<GenericAttribute> _gaRepository;

        /// <summary>
        /// Defines the _staticCacheManager.
        /// </summary>
        private readonly IStaticCacheManager _staticCacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserService"/> class.
        /// </summary>
        /// <param name="applicationUserSettings">The applicationUserSettings<see cref="ApplicationUserSettings"/>.</param>
        /// <param name="cacheManager">The cacheManager<see cref="ICacheManager"/>.</param>
        /// <param name="dataProvider">The dataProvider<see cref="IDataProvider"/>.</param>
        /// <param name="dbContext">The dbContext<see cref="IDbContext"/>.</param>
        /// <param name="eventPublisher">The eventPublisher<see cref="IEventPublisher"/>.</param>
        /// <param name="genericAttributeService">The genericAttributeService<see cref="IGenericAttributeService"/>.</param>
        /// <param name="applicationUserRepository">The applicationUserRepository<see cref="IRepository{ApplicationUser}"/>.</param>
        /// <param name="applicationUserApplicationUserRoleMappingRepository">The applicationUserApplicationUserRoleMappingRepository<see cref="IRepository{ApplicationUserApplicationUserRoleMapping}"/>.</param>
        /// <param name="applicationUserPasswordRepository">The applicationUserPasswordRepository<see cref="IRepository{ApplicationUserPassword}"/>.</param>
        /// <param name="applicationUserRoleRepository">The applicationUserRoleRepository<see cref="IRepository{ApplicationUserRole}"/>.</param>
        /// <param name="gaRepository">The gaRepository<see cref="IRepository{GenericAttribute}"/>.</param>
        /// <param name="staticCacheManager">The staticCacheManager<see cref="IStaticCacheManager"/>.</param>
        public ApplicationUserService(ApplicationUserSettings applicationUserSettings,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IEventPublisher eventPublisher,
            IGenericAttributeService genericAttributeService,
            IRepository<ApplicationUser> applicationUserRepository,
            IRepository<ApplicationUserApplicationUserRoleMapping> applicationUserApplicationUserRoleMappingRepository,
            IRepository<ApplicationUserPassword> applicationUserPasswordRepository,
            IRepository<ApplicationUserRole> applicationUserRoleRepository,
            IRepository<GenericAttribute> gaRepository,
            IStaticCacheManager staticCacheManager)
        {
            _applicationUserSettings = applicationUserSettings;
            _cacheManager = cacheManager;
            _dataProvider = dataProvider;
            _dbContext = dbContext;
            _eventPublisher = eventPublisher;
            _genericAttributeService = genericAttributeService;
            _applicationUserRepository = applicationUserRepository;
            _applicationUserApplicationUserRoleMappingRepository = applicationUserApplicationUserRoleMappingRepository;
            _applicationUserPasswordRepository = applicationUserPasswordRepository;
            _applicationUserRoleRepository = applicationUserRoleRepository;
            _gaRepository = gaRepository;
            _staticCacheManager = staticCacheManager;
        }

        /// <summary>
        /// Gets all applicationUsers.
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records.</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records.</param>
        /// <param name="affiliateId">Affiliate identifier.</param>
        /// <param name="vendorId">Vendor identifier.</param>
        /// <param name="applicationUserRoleIds">A list of applicationUser role identifiers to filter by (at least one match); pass null or empty list in order to load all applicationUsers; .</param>
        /// <param name="email">Email; null to load all applicationUsers.</param>
        /// <param name="username">Username; null to load all applicationUsers.</param>
        /// <param name="firstName">First name; null to load all applicationUsers.</param>
        /// <param name="lastName">Last name; null to load all applicationUsers.</param>
        /// <param name="dayOfBirth">Day of birth; 0 to load all applicationUsers.</param>
        /// <param name="monthOfBirth">Month of birth; 0 to load all applicationUsers.</param>
        /// <param name="company">Company; null to load all applicationUsers.</param>
        /// <param name="phone">Phone; null to load all applicationUsers.</param>
        /// <param name="zipPostalCode">Phone; null to load all applicationUsers.</param>
        /// <param name="ipAddress">IP address; null to load all applicationUsers.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="getOnlyTotalCount">A value in indicating whether you want to load only total number of records. Set to "true" if you don't want to load data from database.</param>
        /// <returns>ApplicationUsers.</returns>
        public virtual IPagedList<ApplicationUser> GetAllApplicationUsers(DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            int affiliateId = 0, int vendorId = 0, int[] applicationUserRoleIds = null,
            string email = null, string username = null, string firstName = null, string lastName = null,
            int dayOfBirth = 0, int monthOfBirth = 0,
            string company = null, string phone = null, string zipPostalCode = null, string ipAddress = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var query = _applicationUserRepository.Table;
            if (createdFromUtc.HasValue)
                query = query.Where(c => createdFromUtc.Value <= c.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(c => createdToUtc.Value >= c.CreatedOnUtc);
            if (affiliateId > 0)
                query = query.Where(c => affiliateId == c.AffiliateId);
            if (vendorId > 0)
                query = query.Where(c => vendorId == c.VendorId);
            query = query.Where(c => !c.Deleted);

            if (applicationUserRoleIds != null && applicationUserRoleIds.Length > 0)
            {
                query = query.Join(_applicationUserApplicationUserRoleMappingRepository.Table, x => x.Id, y => y.ApplicationUserId,
                        (x, y) => new { ApplicationUser = x, Mapping = y })
                    .Where(z => applicationUserRoleIds.Contains(z.Mapping.ApplicationUserRoleId))
                    .Select(z => z.ApplicationUser)
                    .Distinct();
            }

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(c => c.Email.Contains(email));
            if (!string.IsNullOrWhiteSpace(username))
                query = query.Where(c => c.Username.Contains(username));
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { ApplicationUser = x, Attribute = y })
                    .Where(z => z.Attribute.KeyGroup == nameof(ApplicationUser) &&
                                z.Attribute.Key == HazelApplicationUserDefaults.FirstNameAttribute &&
                                z.Attribute.Value.Contains(firstName))
                    .Select(z => z.ApplicationUser);
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { ApplicationUser = x, Attribute = y })
                    .Where(z => z.Attribute.KeyGroup == nameof(ApplicationUser) &&
                                z.Attribute.Key == HazelApplicationUserDefaults.LastNameAttribute &&
                                z.Attribute.Value.Contains(lastName))
                    .Select(z => z.ApplicationUser);
            }

            //date of birth is stored as a string into database.
            //we also know that date of birth is stored in the following format YYYY-MM-DD (for example, 1983-02-18).
            //so let's search it as a string
            if (dayOfBirth > 0 && monthOfBirth > 0)
            {
                //both are specified
                var dateOfBirthStr = monthOfBirth.ToString("00", CultureInfo.InvariantCulture) + "-" + dayOfBirth.ToString("00", CultureInfo.InvariantCulture);

                //z.Attribute.Value.Length - dateOfBirthStr.Length = 5
                //dateOfBirthStr.Length = 5
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { ApplicationUser = x, Attribute = y })
                    .Where(z => z.Attribute.KeyGroup == nameof(ApplicationUser) &&
                                z.Attribute.Key == HazelApplicationUserDefaults.DateOfBirthAttribute &&
                                z.Attribute.Value.Substring(5, 5) == dateOfBirthStr)
                    .Select(z => z.ApplicationUser);
            }
            else if (dayOfBirth > 0)
            {
                //only day is specified
                var dateOfBirthStr = dayOfBirth.ToString("00", CultureInfo.InvariantCulture);

                //z.Attribute.Value.Length - dateOfBirthStr.Length = 8
                //dateOfBirthStr.Length = 2
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { ApplicationUser = x, Attribute = y })
                    .Where(z => z.Attribute.KeyGroup == nameof(ApplicationUser) &&
                                z.Attribute.Key == HazelApplicationUserDefaults.DateOfBirthAttribute &&
                                z.Attribute.Value.Substring(8, 2) == dateOfBirthStr)
                    .Select(z => z.ApplicationUser);
            }
            else if (monthOfBirth > 0)
            {
                //only month is specified
                var dateOfBirthStr = "-" + monthOfBirth.ToString("00", CultureInfo.InvariantCulture) + "-";
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { ApplicationUser = x, Attribute = y })
                    .Where(z => z.Attribute.KeyGroup == nameof(ApplicationUser) &&
                                z.Attribute.Key == HazelApplicationUserDefaults.DateOfBirthAttribute &&
                                z.Attribute.Value.Contains(dateOfBirthStr))
                    .Select(z => z.ApplicationUser);
            }
            //search by company
            if (!string.IsNullOrWhiteSpace(company))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { ApplicationUser = x, Attribute = y })
                    .Where(z => z.Attribute.KeyGroup == nameof(ApplicationUser) &&
                                z.Attribute.Key == HazelApplicationUserDefaults.CompanyAttribute &&
                                z.Attribute.Value.Contains(company))
                    .Select(z => z.ApplicationUser);
            }
            //search by phone
            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { ApplicationUser = x, Attribute = y })
                    .Where(z => z.Attribute.KeyGroup == nameof(ApplicationUser) &&
                                z.Attribute.Key == HazelApplicationUserDefaults.PhoneAttribute &&
                                z.Attribute.Value.Contains(phone))
                    .Select(z => z.ApplicationUser);
            }
            //search by zip
            if (!string.IsNullOrWhiteSpace(zipPostalCode))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { ApplicationUser = x, Attribute = y })
                    .Where(z => z.Attribute.KeyGroup == nameof(ApplicationUser) &&
                                z.Attribute.Key == HazelApplicationUserDefaults.ZipPostalCodeAttribute &&
                                z.Attribute.Value.Contains(zipPostalCode))
                    .Select(z => z.ApplicationUser);
            }

            //search by IpAddress
            if (!string.IsNullOrWhiteSpace(ipAddress) && CommonHelper.IsValidIpAddress(ipAddress))
            {
                query = query.Where(w => w.LastIpAddress == ipAddress);
            }

            query = query.OrderByDescending(c => c.CreatedOnUtc);

            var applicationUsers = new PagedList<ApplicationUser>(query, pageIndex, pageSize, getOnlyTotalCount);
            return applicationUsers;
        }

        /// <summary>
        /// Gets online applicationUsers.
        /// </summary>
        /// <param name="lastActivityFromUtc">ApplicationUser last activity date (from).</param>
        /// <param name="applicationUserRoleIds">A list of applicationUser role identifiers to filter by (at least one match); pass null or empty list in order to load all applicationUsers; .</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>ApplicationUsers.</returns>
        public virtual IPagedList<ApplicationUser> GetOnlineApplicationUsers(DateTime lastActivityFromUtc,
            int[] applicationUserRoleIds, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _applicationUserRepository.Table;
            query = query.Where(c => lastActivityFromUtc <= c.LastActivityDateUtc);
            query = query.Where(c => !c.Deleted);
            if (applicationUserRoleIds != null && applicationUserRoleIds.Length > 0)
                query = query.Where(c => c.ApplicationUserApplicationUserRoleMappings.Select(mapping => mapping.ApplicationUserRoleId).Intersect(applicationUserRoleIds).Any());

            query = query.OrderByDescending(c => c.LastActivityDateUtc);
            var applicationUsers = new PagedList<ApplicationUser>(query, pageIndex, pageSize);
            return applicationUsers;
        }

        /// <summary>
        /// Delete a applicationUser.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        public virtual void DeleteApplicationUser(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            if (applicationUser.IsSystemAccount)
                throw new HazelException($"System applicationUser account ({applicationUser.SystemName}) could not be deleted");

            applicationUser.Deleted = true;

            if (_applicationUserSettings.SuffixDeletedApplicationUsers)
            {
                if (!string.IsNullOrEmpty(applicationUser.Email))
                    applicationUser.Email += "-DELETED";
                if (!string.IsNullOrEmpty(applicationUser.Username))
                    applicationUser.Username += "-DELETED";
            }

            UpdateApplicationUser(applicationUser);

            //event notification
            _eventPublisher.EntityDeleted(applicationUser);
        }

        /// <summary>
        /// Gets a applicationUser.
        /// </summary>
        /// <param name="applicationUserId">ApplicationUser identifier.</param>
        /// <returns>A applicationUser.</returns>
        public virtual ApplicationUser GetApplicationUserById(int applicationUserId)
        {
            if (applicationUserId == 0)
                return null;

            return _applicationUserRepository.GetById(applicationUserId);
        }

        /// <summary>
        /// Get applicationUsers by identifiers.
        /// </summary>
        /// <param name="applicationUserIds">ApplicationUser identifiers.</param>
        /// <returns>ApplicationUsers.</returns>
        public virtual IList<ApplicationUser> GetApplicationUsersByIds(int[] applicationUserIds)
        {
            if (applicationUserIds == null || applicationUserIds.Length == 0)
                return new List<ApplicationUser>();

            var query = from c in _applicationUserRepository.Table
                        where applicationUserIds.Contains(c.Id) && !c.Deleted
                        select c;
            var applicationUsers = query.ToList();
            //sort by passed identifiers
            var sortedApplicationUsers = new List<ApplicationUser>();
            foreach (var id in applicationUserIds)
            {
                var applicationUser = applicationUsers.Find(x => x.Id == id);
                if (applicationUser != null)
                    sortedApplicationUsers.Add(applicationUser);
            }

            return sortedApplicationUsers;
        }

        /// <summary>
        /// Gets a applicationUser by GUID.
        /// </summary>
        /// <param name="applicationUserGuid">ApplicationUser GUID.</param>
        /// <returns>A applicationUser.</returns>
        public virtual ApplicationUser GetApplicationUserByGuid(Guid applicationUserGuid)
        {
            if (applicationUserGuid == Guid.Empty)
                return null;

            var query = from c in _applicationUserRepository.Table
                        where c.ApplicationUserGuid == applicationUserGuid
                        orderby c.Id
                        select c;
            var applicationUser = query.FirstOrDefault();
            return applicationUser;
        }

        /// <summary>
        /// Get applicationUser by email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>ApplicationUser.</returns>
        public virtual ApplicationUser GetApplicationUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = from c in _applicationUserRepository.Table
                        orderby c.Id
                        where c.Email == email
                        select c;
            var applicationUser = query.FirstOrDefault();
            return applicationUser;
        }

        /// <summary>
        /// Get applicationUser by system name.
        /// </summary>
        /// <param name="systemName">System name.</param>
        /// <returns>ApplicationUser.</returns>
        public virtual ApplicationUser GetApplicationUserBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from c in _applicationUserRepository.Table
                        orderby c.Id
                        where c.SystemName == systemName
                        select c;
            var applicationUser = query.FirstOrDefault();
            return applicationUser;
        }

        /// <summary>
        /// Get applicationUser by username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>ApplicationUser.</returns>
        public virtual ApplicationUser GetApplicationUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in _applicationUserRepository.Table
                        orderby c.Id
                        where c.Username == username
                        select c;
            var applicationUser = query.FirstOrDefault();
            return applicationUser;
        }

        /// <summary>
        /// Insert a guest applicationUser.
        /// </summary>
        /// <returns>ApplicationUser.</returns>
        public virtual ApplicationUser InsertGuestApplicationUser()
        {
            var applicationUser = new ApplicationUser
            {
                ApplicationUserGuid = Guid.NewGuid(),
                Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow
            };

            //add to 'Guests' role
            var guestRole = GetApplicationUserRoleBySystemName(HazelApplicationUserDefaults.GuestsRoleName);
            if (guestRole == null)
                throw new HazelException("'Guests' role could not be loaded");
            //applicationUser.ApplicationUserRoles.Add(guestRole);
            applicationUser.AddApplicationUserRoleMapping(new ApplicationUserApplicationUserRoleMapping { ApplicationUserRole = guestRole });

            _applicationUserRepository.Insert(applicationUser);

            return applicationUser;
        }

        /// <summary>
        /// Insert a applicationUser.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        public virtual void InsertApplicationUser(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            _applicationUserRepository.Insert(applicationUser);

            //event notification
            _eventPublisher.EntityInserted(applicationUser);
        }

        /// <summary>
        /// Updates the applicationUser.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        public virtual void UpdateApplicationUser(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            _applicationUserRepository.Update(applicationUser);

            //event notification
            _eventPublisher.EntityUpdated(applicationUser);
        }

        /// <summary>
        /// Reset data required for checkout.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="storeId">Store identifier.</param>
        /// <param name="clearCouponCodes">A value indicating whether to clear coupon code.</param>
        /// <param name="clearCheckoutAttributes">A value indicating whether to clear selected checkout attributes.</param>
        /// <param name="clearRewardPoints">A value indicating whether to clear "Use reward points" flag.</param>
        /// <param name="clearShippingMethod">A value indicating whether to clear selected shipping method.</param>
        /// <param name="clearPaymentMethod">A value indicating whether to clear selected payment method.</param>
        public virtual void ResetCheckoutData(ApplicationUser applicationUser, int storeId,
            bool clearCouponCodes = false, bool clearCheckoutAttributes = false,
            bool clearRewardPoints = true, bool clearShippingMethod = true,
            bool clearPaymentMethod = true)
        {
            if (applicationUser == null)
                throw new ArgumentNullException();

            //clear entered coupon codes
            if (clearCouponCodes)
            {
                _genericAttributeService.SaveAttribute<string>(applicationUser, HazelApplicationUserDefaults.DiscountCouponCodeAttribute, null);
                _genericAttributeService.SaveAttribute<string>(applicationUser, HazelApplicationUserDefaults.GiftCardCouponCodesAttribute, null);
            }

            //clear checkout attributes
            if (clearCheckoutAttributes)
            {
                _genericAttributeService.SaveAttribute<string>(applicationUser, HazelApplicationUserDefaults.CheckoutAttributes, null, storeId);
            }

            //clear reward points flag
            if (clearRewardPoints)
            {
                _genericAttributeService.SaveAttribute(applicationUser, HazelApplicationUserDefaults.UseRewardPointsDuringCheckoutAttribute, false, storeId);
            }

            //clear selected payment method
            if (clearPaymentMethod)
            {
                _genericAttributeService.SaveAttribute<string>(applicationUser, HazelApplicationUserDefaults.SelectedPaymentMethodAttribute, null, storeId);
            }

            UpdateApplicationUser(applicationUser);
        }

        /// <summary>
        /// Delete guest applicationUser records.
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records.</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records.</param>
        /// <param name="onlyWithoutShoppingCart">A value indicating whether to delete applicationUsers only without shopping cart.</param>
        /// <returns>Number of deleted applicationUsers.</returns>
        public virtual int DeleteGuestApplicationUsers(DateTime? createdFromUtc, DateTime? createdToUtc, bool onlyWithoutShoppingCart)
        {
            //prepare parameters
            var pOnlyWithoutShoppingCart = _dataProvider.GetBooleanParameter("OnlyWithoutShoppingCart", onlyWithoutShoppingCart);
            var pCreatedFromUtc = _dataProvider.GetDateTimeParameter("CreatedFromUtc", createdFromUtc);
            var pCreatedToUtc = _dataProvider.GetDateTimeParameter("CreatedToUtc", createdToUtc);
            var pTotalRecordsDeleted = _dataProvider.GetOutputInt32Parameter("TotalRecordsDeleted");

            //invoke stored procedure
            _dbContext.ExecuteSqlCommand(
                "EXEC [DeleteGuests] @OnlyWithoutShoppingCart, @CreatedFromUtc, @CreatedToUtc, @TotalRecordsDeleted OUTPUT",
                false, null,
                pOnlyWithoutShoppingCart,
                pCreatedFromUtc,
                pCreatedToUtc,
                pTotalRecordsDeleted);

            var totalRecordsDeleted = pTotalRecordsDeleted.Value != DBNull.Value ? Convert.ToInt32(pTotalRecordsDeleted.Value) : 0;
            return totalRecordsDeleted;
        }

        /// <summary>
        /// Get full name.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>ApplicationUser full name.</returns>
        public virtual string GetApplicationUserFullName(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            var firstName = _genericAttributeService.GetAttribute<string>(applicationUser, HazelApplicationUserDefaults.FirstNameAttribute);
            var lastName = _genericAttributeService.GetAttribute<string>(applicationUser, HazelApplicationUserDefaults.LastNameAttribute);

            var fullName = string.Empty;
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                fullName = $"{firstName} {lastName}";
            else
            {
                if (!string.IsNullOrWhiteSpace(firstName))
                    fullName = firstName;

                if (!string.IsNullOrWhiteSpace(lastName))
                    fullName = lastName;
            }

            return fullName;
        }

        /// <summary>
        /// Formats the applicationUser name.
        /// </summary>
        /// <param name="applicationUser">Source.</param>
        /// <param name="stripTooLong">Strip too long applicationUser name.</param>
        /// <param name="maxLength">Maximum applicationUser name length.</param>
        /// <returns>Formatted text.</returns>
        public virtual string FormatUsername(ApplicationUser applicationUser, bool stripTooLong = false, int maxLength = 0)
        {
            if (applicationUser == null)
                return string.Empty;

            if (applicationUser.IsGuest())
                return EngineContext.Current.Resolve<ILocalizationService>().GetResource("ApplicationUser.Guest");

            var result = string.Empty;
            switch (_applicationUserSettings.ApplicationUserNameFormat)
            {
                case ApplicationUserNameFormat.ShowEmails:
                    result = applicationUser.Email;
                    break;
                case ApplicationUserNameFormat.ShowUsernames:
                    result = applicationUser.Username;
                    break;
                case ApplicationUserNameFormat.ShowFullNames:
                    result = GetApplicationUserFullName(applicationUser);
                    break;
                case ApplicationUserNameFormat.ShowFirstName:
                    result = _genericAttributeService.GetAttribute<string>(applicationUser, HazelApplicationUserDefaults.FirstNameAttribute);
                    break;
                default:
                    break;
            }

            if (stripTooLong && maxLength > 0)
                result = CommonHelper.EnsureMaximumLength(result, maxLength);

            return result;
        }

        /// <summary>
        /// Gets coupon codes.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>Coupon codes.</returns>
        public virtual string[] ParseAppliedDiscountCouponCodes(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            var existingCouponCodes = _genericAttributeService.GetAttribute<string>(applicationUser, HazelApplicationUserDefaults.DiscountCouponCodeAttribute);

            var couponCodes = new List<string>();
            if (string.IsNullOrEmpty(existingCouponCodes))
                return couponCodes.ToArray();

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(existingCouponCodes);

                var nodeList1 = xmlDoc.SelectNodes(@"//DiscountCouponCodes/CouponCode");
                foreach (XmlNode node1 in nodeList1)
                {
                    if (node1.Attributes?["Code"] == null)
                        continue;
                    var code = node1.Attributes["Code"].InnerText.Trim();
                    couponCodes.Add(code);
                }
            }
            catch
            {
                // ignored
            }

            return couponCodes.ToArray();
        }

        /// <summary>
        /// Adds a coupon code.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="couponCode">Coupon code.</param>
        public virtual void ApplyDiscountCouponCode(ApplicationUser applicationUser, string couponCode)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            var result = string.Empty;
            try
            {
                var existingCouponCodes = _genericAttributeService.GetAttribute<string>(applicationUser, HazelApplicationUserDefaults.DiscountCouponCodeAttribute);

                couponCode = couponCode.Trim().ToLower();

                var xmlDoc = new XmlDocument();
                if (string.IsNullOrEmpty(existingCouponCodes))
                {
                    var element1 = xmlDoc.CreateElement("DiscountCouponCodes");
                    xmlDoc.AppendChild(element1);
                }
                else
                {
                    xmlDoc.LoadXml(existingCouponCodes);
                }

                var rootElement = (XmlElement)xmlDoc.SelectSingleNode(@"//DiscountCouponCodes");

                XmlElement gcElement = null;
                //find existing
                var nodeList1 = xmlDoc.SelectNodes(@"//DiscountCouponCodes/CouponCode");
                foreach (XmlNode node1 in nodeList1)
                {
                    if (node1.Attributes?["Code"] == null)
                        continue;

                    var couponCodeAttribute = node1.Attributes["Code"].InnerText.Trim();

                    if (couponCodeAttribute.ToLower() != couponCode.ToLower())
                        continue;

                    gcElement = (XmlElement)node1;
                    break;
                }

                //create new one if not found
                if (gcElement == null)
                {
                    gcElement = xmlDoc.CreateElement("CouponCode");
                    gcElement.SetAttribute("Code", couponCode);
                    rootElement.AppendChild(gcElement);
                }

                result = xmlDoc.OuterXml;
            }
            catch
            {
                // ignored
            }

            //apply new value
            _genericAttributeService.SaveAttribute(applicationUser, HazelApplicationUserDefaults.DiscountCouponCodeAttribute, result);
        }

        /// <summary>
        /// Removes a coupon code.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="couponCode">Coupon code to remove.</param>
        public virtual void RemoveDiscountCouponCode(ApplicationUser applicationUser, string couponCode)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            //get applied coupon codes
            var existingCouponCodes = ParseAppliedDiscountCouponCodes(applicationUser);

            //clear them
            _genericAttributeService.SaveAttribute<string>(applicationUser, HazelApplicationUserDefaults.DiscountCouponCodeAttribute, null);

            //save again except removed one
            foreach (var existingCouponCode in existingCouponCodes)
                if (!existingCouponCode.Equals(couponCode, StringComparison.InvariantCultureIgnoreCase))
                    ApplyDiscountCouponCode(applicationUser, existingCouponCode);
        }

        /// <summary>
        /// Gets coupon codes.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>Coupon codes.</returns>
        public virtual string[] ParseAppliedGiftCardCouponCodes(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            var existingCouponCodes = _genericAttributeService.GetAttribute<string>(applicationUser, HazelApplicationUserDefaults.GiftCardCouponCodesAttribute);

            var couponCodes = new List<string>();
            if (string.IsNullOrEmpty(existingCouponCodes))
                return couponCodes.ToArray();

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(existingCouponCodes);

                var nodeList1 = xmlDoc.SelectNodes(@"//GiftCardCouponCodes/CouponCode");
                foreach (XmlNode node1 in nodeList1)
                {
                    if (node1.Attributes?["Code"] == null)
                        continue;

                    var code = node1.Attributes["Code"].InnerText.Trim();
                    couponCodes.Add(code);
                }
            }
            catch
            {
                // ignored
            }

            return couponCodes.ToArray();
        }

        /// <summary>
        /// Adds a coupon code.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="couponCode">Coupon code.</param>
        public virtual void ApplyGiftCardCouponCode(ApplicationUser applicationUser, string couponCode)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            var result = string.Empty;
            try
            {
                var existingCouponCodes = _genericAttributeService.GetAttribute<string>(applicationUser, HazelApplicationUserDefaults.GiftCardCouponCodesAttribute);

                couponCode = couponCode.Trim().ToLower();

                var xmlDoc = new XmlDocument();
                if (string.IsNullOrEmpty(existingCouponCodes))
                {
                    var element1 = xmlDoc.CreateElement("GiftCardCouponCodes");
                    xmlDoc.AppendChild(element1);
                }
                else
                {
                    xmlDoc.LoadXml(existingCouponCodes);
                }

                var rootElement = (XmlElement)xmlDoc.SelectSingleNode(@"//GiftCardCouponCodes");

                XmlElement gcElement = null;
                //find existing
                var nodeList1 = xmlDoc.SelectNodes(@"//GiftCardCouponCodes/CouponCode");
                foreach (XmlNode node1 in nodeList1)
                {
                    if (node1.Attributes?["Code"] == null)
                        continue;

                    var couponCodeAttribute = node1.Attributes["Code"].InnerText.Trim();
                    if (couponCodeAttribute.ToLower() != couponCode.ToLower())
                        continue;

                    gcElement = (XmlElement)node1;
                    break;
                }

                //create new one if not found
                if (gcElement == null)
                {
                    gcElement = xmlDoc.CreateElement("CouponCode");
                    gcElement.SetAttribute("Code", couponCode);
                    rootElement.AppendChild(gcElement);
                }

                result = xmlDoc.OuterXml;
            }
            catch
            {
                // ignored
            }

            //apply new value
            _genericAttributeService.SaveAttribute(applicationUser, HazelApplicationUserDefaults.GiftCardCouponCodesAttribute, result);
        }

        /// <summary>
        /// Removes a coupon code.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="couponCode">Coupon code to remove.</param>
        public virtual void RemoveGiftCardCouponCode(ApplicationUser applicationUser, string couponCode)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            //get applied coupon codes
            var existingCouponCodes = ParseAppliedGiftCardCouponCodes(applicationUser);

            //clear them
            _genericAttributeService.SaveAttribute<string>(applicationUser, HazelApplicationUserDefaults.GiftCardCouponCodesAttribute, null);

            //save again except removed one
            foreach (var existingCouponCode in existingCouponCodes)
                if (!existingCouponCode.Equals(couponCode, StringComparison.InvariantCultureIgnoreCase))
                    ApplyGiftCardCouponCode(applicationUser, existingCouponCode);
        }

        /// <summary>
        /// Delete a applicationUser role.
        /// </summary>
        /// <param name="applicationUserRole">ApplicationUser role.</param>
        public virtual void DeleteApplicationUserRole(ApplicationUserRole applicationUserRole)
        {
            if (applicationUserRole == null)
                throw new ArgumentNullException(nameof(applicationUserRole));

            if (applicationUserRole.IsSystemRole)
                throw new HazelException("System role could not be deleted");

            _applicationUserRoleRepository.Delete(applicationUserRole);

            _cacheManager.RemoveByPrefix(HazelApplicationUserServiceDefaults.ApplicationUserRolesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityDeleted(applicationUserRole);
        }

        /// <summary>
        /// Gets a applicationUser role.
        /// </summary>
        /// <param name="applicationUserRoleId">ApplicationUser role identifier.</param>
        /// <returns>ApplicationUser role.</returns>
        public virtual ApplicationUserRole GetApplicationUserRoleById(int applicationUserRoleId)
        {
            if (applicationUserRoleId == 0)
                return null;

            return _applicationUserRoleRepository.GetById(applicationUserRoleId);
        }

        /// <summary>
        /// Gets a applicationUser role.
        /// </summary>
        /// <param name="systemName">ApplicationUser role system name.</param>
        /// <returns>ApplicationUser role.</returns>
        public virtual ApplicationUserRole GetApplicationUserRoleBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var key = string.Format(HazelApplicationUserServiceDefaults.ApplicationUserRolesBySystemNameCacheKey, systemName);
            return _cacheManager.Get(key, () =>
            {
                var query = from cr in _applicationUserRoleRepository.Table
                            orderby cr.Id
                            where cr.SystemName == systemName
                            select cr;
                var applicationUserRole = query.FirstOrDefault();
                return applicationUserRole;
            });
        }

        /// <summary>
        /// Gets all applicationUser roles.
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records.</param>
        /// <returns>ApplicationUser roles.</returns>
        public virtual IList<ApplicationUserRole> GetAllApplicationUserRoles(bool showHidden = false)
        {
            var key = string.Format(HazelApplicationUserServiceDefaults.ApplicationUserRolesAllCacheKey, showHidden);
            return _cacheManager.Get(key, () =>
            {
                var query = from cr in _applicationUserRoleRepository.Table
                            orderby cr.Name
                            where showHidden || cr.Active
                            select cr;
                var applicationUserRoles = query.ToList();
                return applicationUserRoles;
            });
        }

        /// <summary>
        /// Inserts a applicationUser role.
        /// </summary>
        /// <param name="applicationUserRole">ApplicationUser role.</param>
        public virtual void InsertApplicationUserRole(ApplicationUserRole applicationUserRole)
        {
            if (applicationUserRole == null)
                throw new ArgumentNullException(nameof(applicationUserRole));

            _applicationUserRoleRepository.Insert(applicationUserRole);

            _cacheManager.RemoveByPrefix(HazelApplicationUserServiceDefaults.ApplicationUserRolesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(applicationUserRole);
        }

        /// <summary>
        /// Updates the applicationUser role.
        /// </summary>
        /// <param name="applicationUserRole">ApplicationUser role.</param>
        public virtual void UpdateApplicationUserRole(ApplicationUserRole applicationUserRole)
        {
            if (applicationUserRole == null)
                throw new ArgumentNullException(nameof(applicationUserRole));

            _applicationUserRoleRepository.Update(applicationUserRole);

            _cacheManager.RemoveByPrefix(HazelApplicationUserServiceDefaults.ApplicationUserRolesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(applicationUserRole);
        }

        /// <summary>
        /// Gets applicationUser passwords.
        /// </summary>
        /// <param name="applicationUserId">ApplicationUser identifier; pass null to load all records.</param>
        /// <param name="passwordFormat">Password format; pass null to load all records.</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records.</param>
        /// <returns>List of applicationUser passwords.</returns>
        public virtual IList<ApplicationUserPassword> GetApplicationUserPasswords(int? applicationUserId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null)
        {
            var query = _applicationUserPasswordRepository.Table;

            //filter by applicationUser
            if (applicationUserId.HasValue)
                query = query.Where(password => password.ApplicationUserId == applicationUserId.Value);

            //filter by password format
            if (passwordFormat.HasValue)
                query = query.Where(password => password.PasswordFormatId == (int)passwordFormat.Value);

            //get the latest passwords
            if (passwordsToReturn.HasValue)
                query = query.OrderByDescending(password => password.CreatedOnUtc).Take(passwordsToReturn.Value);

            return query.ToList();
        }

        /// <summary>
        /// Get current applicationUser password.
        /// </summary>
        /// <param name="applicationUserId">ApplicationUser identifier.</param>
        /// <returns>ApplicationUser password.</returns>
        public virtual ApplicationUserPassword GetCurrentPassword(int applicationUserId)
        {
            if (applicationUserId == 0)
                return null;

            //return the latest password
            return GetApplicationUserPasswords(applicationUserId, passwordsToReturn: 1).FirstOrDefault();
        }

        /// <summary>
        /// Insert a applicationUser password.
        /// </summary>
        /// <param name="applicationUserPassword">ApplicationUser password.</param>
        public virtual void InsertApplicationUserPassword(ApplicationUserPassword applicationUserPassword)
        {
            if (applicationUserPassword == null)
                throw new ArgumentNullException(nameof(applicationUserPassword));

            _applicationUserPasswordRepository.Insert(applicationUserPassword);

            //event notification
            _eventPublisher.EntityInserted(applicationUserPassword);
        }

        /// <summary>
        /// Update a applicationUser password.
        /// </summary>
        /// <param name="applicationUserPassword">ApplicationUser password.</param>
        public virtual void UpdateApplicationUserPassword(ApplicationUserPassword applicationUserPassword)
        {
            if (applicationUserPassword == null)
                throw new ArgumentNullException(nameof(applicationUserPassword));

            _applicationUserPasswordRepository.Update(applicationUserPassword);

            //event notification
            _eventPublisher.EntityUpdated(applicationUserPassword);
        }

        /// <summary>
        /// Check whether password recovery token is valid.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="token">Token to validate.</param>
        /// <returns>Result.</returns>
        public virtual bool IsPasswordRecoveryTokenValid(ApplicationUser applicationUser, string token)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            var cPrt = _genericAttributeService.GetAttribute<string>(applicationUser, HazelApplicationUserDefaults.PasswordRecoveryTokenAttribute);
            if (string.IsNullOrEmpty(cPrt))
                return false;

            if (!cPrt.Equals(token, StringComparison.InvariantCultureIgnoreCase))
                return false;

            return true;
        }

        /// <summary>
        /// Check whether password recovery link is expired.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>Result.</returns>
        public virtual bool IsPasswordRecoveryLinkExpired(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            if (_applicationUserSettings.PasswordRecoveryLinkDaysValid == 0)
                return false;

            var geneatedDate = _genericAttributeService.GetAttribute<DateTime?>(applicationUser, HazelApplicationUserDefaults.PasswordRecoveryTokenDateGeneratedAttribute);
            if (!geneatedDate.HasValue)
                return false;

            var daysPassed = (DateTime.UtcNow - geneatedDate.Value).TotalDays;
            if (daysPassed > _applicationUserSettings.PasswordRecoveryLinkDaysValid)
                return true;

            return false;
        }

        /// <summary>
        /// Check whether applicationUser password is expired.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>True if password is expired; otherwise false.</returns>
        public virtual bool PasswordIsExpired(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            //the guests don't have a password
            if (applicationUser.IsGuest())
                return false;

            //password lifetime is disabled for user
            if (!applicationUser.ApplicationUserRoles.Any(role => role.Active && role.EnablePasswordLifetime))
                return false;

            //setting disabled for all
            if (_applicationUserSettings.PasswordLifetime == 0)
                return false;

            var cacheKey = string.Format(HazelApplicationUserServiceDefaults.ApplicationUserPasswordLifetimeCacheKey, applicationUser.Id);

            //get current password usage time
            var currentLifetime = _staticCacheManager.Get(cacheKey, () =>
            {
                var applicationUserPassword = GetCurrentPassword(applicationUser.Id);
                //password is not found, so return max value to force applicationUser to change password
                if (applicationUserPassword == null)
                    return int.MaxValue;

                return (DateTime.UtcNow - applicationUserPassword.CreatedOnUtc).Days;
            });

            return currentLifetime >= _applicationUserSettings.PasswordLifetime;
        }
    }
}
