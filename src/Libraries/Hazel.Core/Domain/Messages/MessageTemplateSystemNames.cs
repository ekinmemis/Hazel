namespace Hazel.Core.Domain.Messages
{
    /// <summary>
    /// Represents message template system names.
    /// </summary>
    public static partial class MessageTemplateSystemNames
    {
        /// <summary>
        /// Represents system name of notification about new registration.
        /// </summary>
        public const string ApplicationUserRegisteredNotification = "NewApplicationUser.Notification";

        /// <summary>
        /// Represents system name of applicationUser welcome message.
        /// </summary>
        public const string ApplicationUserWelcomeMessage = "ApplicationUser.WelcomeMessage";

        /// <summary>
        /// Represents system name of email validation message.
        /// </summary>
        public const string ApplicationUserEmailValidationMessage = "ApplicationUser.EmailValidationMessage";

        /// <summary>
        /// Represents system name of email revalidation message.
        /// </summary>
        public const string ApplicationUserEmailRevalidationMessage = "ApplicationUser.EmailRevalidationMessage";

        /// <summary>
        /// Represents system name of password recovery message.
        /// </summary>
        public const string ApplicationUserPasswordRecoveryMessage = "ApplicationUser.PasswordRecovery";

        /// <summary>
        /// Represents system name of notification vendor about placed order.
        /// </summary>
        public const string OrderPlacedVendorNotification = "OrderPlaced.VendorNotification";

        /// <summary>
        /// Represents system name of notification store owner about placed order.
        /// </summary>
        public const string OrderPlacedStoreOwnerNotification = "OrderPlaced.StoreOwnerNotification";

        /// <summary>
        /// Represents system name of notification affiliate about placed order.
        /// </summary>
        public const string OrderPlacedAffiliateNotification = "OrderPlaced.AffiliateNotification";

        /// <summary>
        /// Represents system name of notification store owner about paid order.
        /// </summary>
        public const string OrderPaidStoreOwnerNotification = "OrderPaid.StoreOwnerNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about paid order.
        /// </summary>
        public const string OrderPaidApplicationUserNotification = "OrderPaid.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification vendor about paid order.
        /// </summary>
        public const string OrderPaidVendorNotification = "OrderPaid.VendorNotification";

        /// <summary>
        /// Represents system name of notification affiliate about paid order.
        /// </summary>
        public const string OrderPaidAffiliateNotification = "OrderPaid.AffiliateNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about placed order.
        /// </summary>
        public const string OrderPlacedApplicationUserNotification = "OrderPlaced.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about sent shipment.
        /// </summary>
        public const string ShipmentSentApplicationUserNotification = "ShipmentSent.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about delivered shipment.
        /// </summary>
        public const string ShipmentDeliveredApplicationUserNotification = "ShipmentDelivered.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about completed order.
        /// </summary>
        public const string OrderCompletedApplicationUserNotification = "OrderCompleted.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about cancelled order.
        /// </summary>
        public const string OrderCancelledApplicationUserNotification = "OrderCancelled.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification store owner about refunded order.
        /// </summary>
        public const string OrderRefundedStoreOwnerNotification = "OrderRefunded.StoreOwnerNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about refunded order.
        /// </summary>
        public const string OrderRefundedApplicationUserNotification = "OrderRefunded.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about new order note.
        /// </summary>
        public const string NewOrderNoteAddedApplicationUserNotification = "ApplicationUser.NewOrderNote";

        /// <summary>
        /// Represents system name of notification store owner about cancelled recurring order.
        /// </summary>
        public const string RecurringPaymentCancelledStoreOwnerNotification = "RecurringPaymentCancelled.StoreOwnerNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about cancelled recurring order.
        /// </summary>
        public const string RecurringPaymentCancelledApplicationUserNotification = "RecurringPaymentCancelled.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about failed payment for the recurring payments.
        /// </summary>
        public const string RecurringPaymentFailedApplicationUserNotification = "RecurringPaymentFailed.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of subscription activation message.
        /// </summary>
        public const string NewsletterSubscriptionActivationMessage = "NewsLetterSubscription.ActivationMessage";

        /// <summary>
        /// Represents system name of subscription deactivation message.
        /// </summary>
        public const string NewsletterSubscriptionDeactivationMessage = "NewsLetterSubscription.DeactivationMessage";

        /// <summary>
        /// Represents system name of 'Email a friend' message.
        /// </summary>
        public const string EmailAFriendMessage = "Service.EmailAFriend";

        /// <summary>
        /// Represents system name of 'Email a friend' message with wishlist.
        /// </summary>
        public const string WishlistToFriendMessage = "Wishlist.EmailAFriend";

        /// <summary>
        /// Represents system name of notification store owner about new return request.
        /// </summary>
        public const string NewReturnRequestStoreOwnerNotification = "NewReturnRequest.StoreOwnerNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about new return request.
        /// </summary>
        public const string NewReturnRequestApplicationUserNotification = "NewReturnRequest.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification applicationUser about changing return request status.
        /// </summary>
        public const string ReturnRequestStatusChangedApplicationUserNotification = "ReturnRequestStatusChanged.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification about new forum topic.
        /// </summary>
        public const string NewForumTopicMessage = "Forums.NewForumTopic";

        /// <summary>
        /// Represents system name of notification about new forum post.
        /// </summary>
        public const string NewForumPostMessage = "Forums.NewForumPost";

        /// <summary>
        /// Represents system name of notification about new private message.
        /// </summary>
        public const string PrivateMessageNotification = "ApplicationUser.NewPM";

        /// <summary>
        /// Represents system name of notification store owner about applying new vendor account.
        /// </summary>
        public const string NewVendorAccountApplyStoreOwnerNotification = "VendorAccountApply.StoreOwnerNotification";

        /// <summary>
        /// Represents system name of notification vendor about changing information.
        /// </summary>
        public const string VendorInformationChangeNotification = "VendorInformationChange.StoreOwnerNotification";

        /// <summary>
        /// Represents system name of notification about gift card.
        /// </summary>
        public const string GiftCardNotification = "GiftCard.Notification";

        /// <summary>
        /// Represents system name of notification store owner about new product review.
        /// </summary>
        public const string ProductReviewStoreOwnerNotification = "Product.ProductReview";

        /// <summary>
        /// Represents system name of notification applicationUser about product review reply.
        /// </summary>
        public const string ProductReviewReplyApplicationUserNotification = "ProductReview.Reply.ApplicationUserNotification";

        /// <summary>
        /// Represents system name of notification store owner about below quantity of product.
        /// </summary>
        public const string QuantityBelowStoreOwnerNotification = "QuantityBelow.StoreOwnerNotification";

        /// <summary>
        /// Represents system name of notification store owner about below quantity of product attribute combination.
        /// </summary>
        public const string QuantityBelowAttributeCombinationStoreOwnerNotification = "QuantityBelow.AttributeCombination.StoreOwnerNotification";

        /// <summary>
        /// Represents system name of notification store owner about submitting new VAT.
        /// </summary>
        public const string NewVatSubmittedStoreOwnerNotification = "NewVATSubmitted.StoreOwnerNotification";

        /// <summary>
        /// Represents system name of notification store owner about new blog comment.
        /// </summary>
        public const string BlogCommentNotification = "Blog.BlogComment";

        /// <summary>
        /// Represents system name of notification store owner about new news comment.
        /// </summary>
        public const string NewsCommentNotification = "News.NewsComment";

        /// <summary>
        /// Represents system name of notification applicationUser about product receipt.
        /// </summary>
        public const string BackInStockNotification = "ApplicationUser.BackInStock";

        /// <summary>
        /// Represents system name of 'Contact us' message.
        /// </summary>
        public const string ContactUsMessage = "Service.ContactUs";

        /// <summary>
        /// Represents system name of 'Contact vendor' message.
        /// </summary>
        public const string ContactVendorMessage = "Service.ContactVendor";
    }
}
