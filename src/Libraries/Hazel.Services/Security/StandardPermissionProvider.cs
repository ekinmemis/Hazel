using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Core.Domain.Security;
using System.Collections.Generic;

namespace Hazel.Services.Security
{
    /// <summary>
    /// Standard permission provider.
    /// </summary>
    public partial class StandardPermissionProvider : IPermissionProvider
    {
        //admin area permissions
        /// <summary>
        /// Defines the AccessAdminPanel.
        /// </summary>
        public static readonly PermissionRecord AccessAdminPanel = new PermissionRecord { Name = "Access admin area", SystemName = "AccessAdminPanel", Category = "Standard" };

        /// <summary>
        /// Defines the AllowApplicationUserImpersonation.
        /// </summary>
        public static readonly PermissionRecord AllowApplicationUserImpersonation = new PermissionRecord { Name = "Admin area. Allow ApplicationUser Impersonation", SystemName = "AllowApplicationUserImpersonation", Category = "ApplicationUsers" };

        /// <summary>
        /// Defines the ManageProducts.
        /// </summary>
        public static readonly PermissionRecord ManageProducts = new PermissionRecord { Name = "Admin area. Manage Products", SystemName = "ManageProducts", Category = "Catalog" };

        /// <summary>
        /// Defines the ManageCategories.
        /// </summary>
        public static readonly PermissionRecord ManageCategories = new PermissionRecord { Name = "Admin area. Manage Categories", SystemName = "ManageCategories", Category = "Catalog" };

        /// <summary>
        /// Defines the ManageManufacturers.
        /// </summary>
        public static readonly PermissionRecord ManageManufacturers = new PermissionRecord { Name = "Admin area. Manage Manufacturers", SystemName = "ManageManufacturers", Category = "Catalog" };

        /// <summary>
        /// Defines the ManageProductReviews.
        /// </summary>
        public static readonly PermissionRecord ManageProductReviews = new PermissionRecord { Name = "Admin area. Manage Product Reviews", SystemName = "ManageProductReviews", Category = "Catalog" };

        /// <summary>
        /// Defines the ManageProductTags.
        /// </summary>
        public static readonly PermissionRecord ManageProductTags = new PermissionRecord { Name = "Admin area. Manage Product Tags", SystemName = "ManageProductTags", Category = "Catalog" };

        /// <summary>
        /// Defines the ManageAttributes.
        /// </summary>
        public static readonly PermissionRecord ManageAttributes = new PermissionRecord { Name = "Admin area. Manage Attributes", SystemName = "ManageAttributes", Category = "Catalog" };

        /// <summary>
        /// Defines the ManageApplicationUsers.
        /// </summary>
        public static readonly PermissionRecord ManageApplicationUsers = new PermissionRecord { Name = "Admin area. Manage ApplicationUsers", SystemName = "ManageApplicationUsers", Category = "ApplicationUsers" };

        /// <summary>
        /// Defines the ManageVendors.
        /// </summary>
        public static readonly PermissionRecord ManageVendors = new PermissionRecord { Name = "Admin area. Manage Vendors", SystemName = "ManageVendors", Category = "ApplicationUsers" };

        /// <summary>
        /// Defines the ManageCurrentCarts.
        /// </summary>
        public static readonly PermissionRecord ManageCurrentCarts = new PermissionRecord { Name = "Admin area. Manage Current Carts", SystemName = "ManageCurrentCarts", Category = "Orders" };

        /// <summary>
        /// Defines the ManageOrders.
        /// </summary>
        public static readonly PermissionRecord ManageOrders = new PermissionRecord { Name = "Admin area. Manage Orders", SystemName = "ManageOrders", Category = "Orders" };

        /// <summary>
        /// Defines the ManageRecurringPayments.
        /// </summary>
        public static readonly PermissionRecord ManageRecurringPayments = new PermissionRecord { Name = "Admin area. Manage Recurring Payments", SystemName = "ManageRecurringPayments", Category = "Orders" };

        /// <summary>
        /// Defines the ManageGiftCards.
        /// </summary>
        public static readonly PermissionRecord ManageGiftCards = new PermissionRecord { Name = "Admin area. Manage Gift Cards", SystemName = "ManageGiftCards", Category = "Orders" };

        /// <summary>
        /// Defines the ManageReturnRequests.
        /// </summary>
        public static readonly PermissionRecord ManageReturnRequests = new PermissionRecord { Name = "Admin area. Manage Return Requests", SystemName = "ManageReturnRequests", Category = "Orders" };

        /// <summary>
        /// Defines the OrderCountryReport.
        /// </summary>
        public static readonly PermissionRecord OrderCountryReport = new PermissionRecord { Name = "Admin area. Access order country report", SystemName = "OrderCountryReport", Category = "Orders" };

        /// <summary>
        /// Defines the ManageAffiliates.
        /// </summary>
        public static readonly PermissionRecord ManageAffiliates = new PermissionRecord { Name = "Admin area. Manage Affiliates", SystemName = "ManageAffiliates", Category = "Promo" };

        /// <summary>
        /// Defines the ManageCampaigns.
        /// </summary>
        public static readonly PermissionRecord ManageCampaigns = new PermissionRecord { Name = "Admin area. Manage Campaigns", SystemName = "ManageCampaigns", Category = "Promo" };

        /// <summary>
        /// Defines the ManageDiscounts.
        /// </summary>
        public static readonly PermissionRecord ManageDiscounts = new PermissionRecord { Name = "Admin area. Manage Discounts", SystemName = "ManageDiscounts", Category = "Promo" };

        /// <summary>
        /// Defines the ManageNewsletterSubscribers.
        /// </summary>
        public static readonly PermissionRecord ManageNewsletterSubscribers = new PermissionRecord { Name = "Admin area. Manage Newsletter Subscribers", SystemName = "ManageNewsletterSubscribers", Category = "Promo" };

        /// <summary>
        /// Defines the ManagePolls.
        /// </summary>
        public static readonly PermissionRecord ManagePolls = new PermissionRecord { Name = "Admin area. Manage Polls", SystemName = "ManagePolls", Category = "Content Management" };

        /// <summary>
        /// Defines the ManageNews.
        /// </summary>
        public static readonly PermissionRecord ManageNews = new PermissionRecord { Name = "Admin area. Manage News", SystemName = "ManageNews", Category = "Content Management" };

        /// <summary>
        /// Defines the ManageBlog.
        /// </summary>
        public static readonly PermissionRecord ManageBlog = new PermissionRecord { Name = "Admin area. Manage Blog", SystemName = "ManageBlog", Category = "Content Management" };

        /// <summary>
        /// Defines the ManageWidgets.
        /// </summary>
        public static readonly PermissionRecord ManageWidgets = new PermissionRecord { Name = "Admin area. Manage Widgets", SystemName = "ManageWidgets", Category = "Content Management" };

        /// <summary>
        /// Defines the ManageTopics.
        /// </summary>
        public static readonly PermissionRecord ManageTopics = new PermissionRecord { Name = "Admin area. Manage Topics", SystemName = "ManageTopics", Category = "Content Management" };

        /// <summary>
        /// Defines the ManageForums.
        /// </summary>
        public static readonly PermissionRecord ManageForums = new PermissionRecord { Name = "Admin area. Manage Forums", SystemName = "ManageForums", Category = "Content Management" };

        /// <summary>
        /// Defines the ManageMessageTemplates.
        /// </summary>
        public static readonly PermissionRecord ManageMessageTemplates = new PermissionRecord { Name = "Admin area. Manage Message Templates", SystemName = "ManageMessageTemplates", Category = "Content Management" };

        /// <summary>
        /// Defines the ManageCountries.
        /// </summary>
        public static readonly PermissionRecord ManageCountries = new PermissionRecord { Name = "Admin area. Manage Countries", SystemName = "ManageCountries", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageLanguages.
        /// </summary>
        public static readonly PermissionRecord ManageLanguages = new PermissionRecord { Name = "Admin area. Manage Languages", SystemName = "ManageLanguages", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageSettings.
        /// </summary>
        public static readonly PermissionRecord ManageSettings = new PermissionRecord { Name = "Admin area. Manage Settings", SystemName = "ManageSettings", Category = "Configuration" };

        /// <summary>
        /// Defines the ManagePaymentMethods.
        /// </summary>
        public static readonly PermissionRecord ManagePaymentMethods = new PermissionRecord { Name = "Admin area. Manage Payment Methods", SystemName = "ManagePaymentMethods", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageExternalAuthenticationMethods.
        /// </summary>
        public static readonly PermissionRecord ManageExternalAuthenticationMethods = new PermissionRecord { Name = "Admin area. Manage External Authentication Methods", SystemName = "ManageExternalAuthenticationMethods", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageTaxSettings.
        /// </summary>
        public static readonly PermissionRecord ManageTaxSettings = new PermissionRecord { Name = "Admin area. Manage Tax Settings", SystemName = "ManageTaxSettings", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageShippingSettings.
        /// </summary>
        public static readonly PermissionRecord ManageShippingSettings = new PermissionRecord { Name = "Admin area. Manage Shipping Settings", SystemName = "ManageShippingSettings", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageCurrencies.
        /// </summary>
        public static readonly PermissionRecord ManageCurrencies = new PermissionRecord { Name = "Admin area. Manage Currencies", SystemName = "ManageCurrencies", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageActivityLog.
        /// </summary>
        public static readonly PermissionRecord ManageActivityLog = new PermissionRecord { Name = "Admin area. Manage Activity Log", SystemName = "ManageActivityLog", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageAcl.
        /// </summary>
        public static readonly PermissionRecord ManageAcl = new PermissionRecord { Name = "Admin area. Manage ACL", SystemName = "ManageACL", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageEmailAccounts.
        /// </summary>
        public static readonly PermissionRecord ManageEmailAccounts = new PermissionRecord { Name = "Admin area. Manage Email Accounts", SystemName = "ManageEmailAccounts", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageStores.
        /// </summary>
        public static readonly PermissionRecord ManageStores = new PermissionRecord { Name = "Admin area. Manage Stores", SystemName = "ManageStores", Category = "Configuration" };

        /// <summary>
        /// Defines the ManagePlugins.
        /// </summary>
        public static readonly PermissionRecord ManagePlugins = new PermissionRecord { Name = "Admin area. Manage Plugins", SystemName = "ManagePlugins", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageSystemLog.
        /// </summary>
        public static readonly PermissionRecord ManageSystemLog = new PermissionRecord { Name = "Admin area. Manage System Log", SystemName = "ManageSystemLog", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageMessageQueue.
        /// </summary>
        public static readonly PermissionRecord ManageMessageQueue = new PermissionRecord { Name = "Admin area. Manage Message Queue", SystemName = "ManageMessageQueue", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageMaintenance.
        /// </summary>
        public static readonly PermissionRecord ManageMaintenance = new PermissionRecord { Name = "Admin area. Manage Maintenance", SystemName = "ManageMaintenance", Category = "Configuration" };

        /// <summary>
        /// Defines the HtmlEditorManagePictures.
        /// </summary>
        public static readonly PermissionRecord HtmlEditorManagePictures = new PermissionRecord { Name = "Admin area. HTML Editor. Manage pictures", SystemName = "HtmlEditor.ManagePictures", Category = "Configuration" };

        /// <summary>
        /// Defines the ManageScheduleTasks.
        /// </summary>
        public static readonly PermissionRecord ManageScheduleTasks = new PermissionRecord { Name = "Admin area. Manage Schedule Tasks", SystemName = "ManageScheduleTasks", Category = "Configuration" };

        //public store permissions
        /// <summary>
        /// Defines the DisplayPrices.
        /// </summary>
        public static readonly PermissionRecord DisplayPrices = new PermissionRecord { Name = "Public store. Display Prices", SystemName = "DisplayPrices", Category = "PublicStore" };

        /// <summary>
        /// Defines the EnableShoppingCart.
        /// </summary>
        public static readonly PermissionRecord EnableShoppingCart = new PermissionRecord { Name = "Public store. Enable shopping cart", SystemName = "EnableShoppingCart", Category = "PublicStore" };

        /// <summary>
        /// Defines the EnableWishlist.
        /// </summary>
        public static readonly PermissionRecord EnableWishlist = new PermissionRecord { Name = "Public store. Enable wishlist", SystemName = "EnableWishlist", Category = "PublicStore" };

        /// <summary>
        /// Defines the PublicStoreAllowNavigation.
        /// </summary>
        public static readonly PermissionRecord PublicStoreAllowNavigation = new PermissionRecord { Name = "Public store. Allow navigation", SystemName = "PublicStoreAllowNavigation", Category = "PublicStore" };

        /// <summary>
        /// Defines the AccessClosedStore.
        /// </summary>
        public static readonly PermissionRecord AccessClosedStore = new PermissionRecord { Name = "Public store. Access a closed store", SystemName = "AccessClosedStore", Category = "PublicStore" };

        /// <summary>
        /// Get permissions.
        /// </summary>
        /// <returns>Permissions.</returns>
        public virtual IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[]
            {
                AccessAdminPanel,
                AllowApplicationUserImpersonation,
                ManageProducts,
                ManageCategories,
                ManageManufacturers,
                ManageProductReviews,
                ManageProductTags,
                ManageAttributes,
                ManageApplicationUsers,
                ManageVendors,
                ManageCurrentCarts,
                ManageOrders,
                ManageRecurringPayments,
                ManageGiftCards,
                ManageReturnRequests,
                OrderCountryReport,
                ManageAffiliates,
                ManageCampaigns,
                ManageDiscounts,
                ManageNewsletterSubscribers,
                ManagePolls,
                ManageNews,
                ManageBlog,
                ManageWidgets,
                ManageTopics,
                ManageForums,
                ManageMessageTemplates,
                ManageCountries,
                ManageLanguages,
                ManageSettings,
                ManagePaymentMethods,
                ManageExternalAuthenticationMethods,
                ManageTaxSettings,
                ManageShippingSettings,
                ManageCurrencies,
                ManageActivityLog,
                ManageAcl,
                ManageEmailAccounts,
                ManageStores,
                ManagePlugins,
                ManageSystemLog,
                ManageMessageQueue,
                ManageMaintenance,
                HtmlEditorManagePictures,
                ManageScheduleTasks,
                DisplayPrices,
                EnableShoppingCart,
                EnableWishlist,
                PublicStoreAllowNavigation,
                AccessClosedStore
            };
        }

        /// <summary>
        /// Get default permissions.
        /// </summary>
        /// <returns>Permissions.</returns>
        public virtual IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return new[]
            {
                new DefaultPermissionRecord
                {
                    ApplicationUserRoleSystemName = HazelApplicationUserDefaults.AdministratorsRoleName,
                    PermissionRecords = new[]
                    {
                        AccessAdminPanel,
                        AllowApplicationUserImpersonation,
                        ManageProducts,
                        ManageCategories,
                        ManageManufacturers,
                        ManageProductReviews,
                        ManageProductTags,
                        ManageAttributes,
                        ManageApplicationUsers,
                        ManageVendors,
                        ManageCurrentCarts,
                        ManageOrders,
                        ManageRecurringPayments,
                        ManageGiftCards,
                        ManageReturnRequests,
                        OrderCountryReport,
                        ManageAffiliates,
                        ManageCampaigns,
                        ManageDiscounts,
                        ManageNewsletterSubscribers,
                        ManagePolls,
                        ManageNews,
                        ManageBlog,
                        ManageWidgets,
                        ManageTopics,
                        ManageForums,
                        ManageMessageTemplates,
                        ManageCountries,
                        ManageLanguages,
                        ManageSettings,
                        ManagePaymentMethods,
                        ManageExternalAuthenticationMethods,
                        ManageTaxSettings,
                        ManageShippingSettings,
                        ManageCurrencies,
                        ManageActivityLog,
                        ManageAcl,
                        ManageEmailAccounts,
                        ManageStores,
                        ManagePlugins,
                        ManageSystemLog,
                        ManageMessageQueue,
                        ManageMaintenance,
                        HtmlEditorManagePictures,
                        ManageScheduleTasks,
                        DisplayPrices,
                        EnableShoppingCart,
                        EnableWishlist,
                        PublicStoreAllowNavigation,
                        AccessClosedStore
                    }
                },
                new DefaultPermissionRecord
                {
                    ApplicationUserRoleSystemName = HazelApplicationUserDefaults.ForumModeratorsRoleName,
                    PermissionRecords = new[]
                    {
                        DisplayPrices,
                        EnableShoppingCart,
                        EnableWishlist,
                        PublicStoreAllowNavigation
                    }
                },
                new DefaultPermissionRecord
                {
                    ApplicationUserRoleSystemName = HazelApplicationUserDefaults.GuestsRoleName,
                    PermissionRecords = new[]
                    {
                        DisplayPrices,
                        EnableShoppingCart,
                        EnableWishlist,
                        PublicStoreAllowNavigation
                    }
                },
                new DefaultPermissionRecord
                {
                    ApplicationUserRoleSystemName = HazelApplicationUserDefaults.RegisteredRoleName,
                    PermissionRecords = new[]
                    {
                        DisplayPrices,
                        EnableShoppingCart,
                        EnableWishlist,
                        PublicStoreAllowNavigation
                    }
                },
                new DefaultPermissionRecord
                {
                    ApplicationUserRoleSystemName = HazelApplicationUserDefaults.VendorsRoleName,
                    PermissionRecords = new[]
                    {
                        AccessAdminPanel,
                        ManageProducts,
                        ManageProductReviews,
                        ManageOrders
                    }
                }
            };
        }
    }
}
