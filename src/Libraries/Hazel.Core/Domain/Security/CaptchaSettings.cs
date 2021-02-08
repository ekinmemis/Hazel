using Hazel.Core.Configuration;

namespace Hazel.Core.Domain.Security
{
    /// <summary>
    /// CAPTCHA settings.
    /// </summary>
    public class CaptchaSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether Enabled
        /// Is CAPTCHA enabled?.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOnLoginPage
        /// A value indicating whether CAPTCHA should be displayed on the login page.
        /// </summary>
        public bool ShowOnLoginPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOnRegistrationPage
        /// A value indicating whether CAPTCHA should be displayed on the registration page.
        /// </summary>
        public bool ShowOnRegistrationPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOnContactUsPage
        /// A value indicating whether CAPTCHA should be displayed on the contacts page.
        /// </summary>
        public bool ShowOnContactUsPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOnEmailWishlistToFriendPage
        /// A value indicating whether CAPTCHA should be displayed on the wishlist page.
        /// </summary>
        public bool ShowOnEmailWishlistToFriendPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOnEmailProductToFriendPage
        /// A value indicating whether CAPTCHA should be displayed on the "email a friend" page.
        /// </summary>
        public bool ShowOnEmailProductToFriendPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOnBlogCommentPage
        /// A value indicating whether CAPTCHA should be displayed on the "comment blog" page.
        /// </summary>
        public bool ShowOnBlogCommentPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOnNewsCommentPage
        /// A value indicating whether CAPTCHA should be displayed on the "comment news" page.
        /// </summary>
        public bool ShowOnNewsCommentPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOnProductReviewPage
        /// A value indicating whether CAPTCHA should be displayed on the product reviews page.
        /// </summary>
        public bool ShowOnProductReviewPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOnApplyVendorPage
        /// A value indicating whether CAPTCHA should be displayed on the "Apply for vendor account" page.
        /// </summary>
        public bool ShowOnApplyVendorPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ShowOnForgotPasswordPage
        /// A value indicating whether CAPTCHA should be displayed on the "forgot password" page.
        /// </summary>
        public bool ShowOnForgotPasswordPage { get; set; }

        /// <summary>
        /// Gets or sets the ReCaptchaPublicKey
        /// reCAPTCHA public key.
        /// </summary>
        public string ReCaptchaPublicKey { get; set; }

        /// <summary>
        /// Gets or sets the ReCaptchaPrivateKey
        /// reCAPTCHA private key.
        /// </summary>
        public string ReCaptchaPrivateKey { get; set; }

        /// <summary>
        /// Gets or sets the ReCaptchaTheme
        /// reCAPTCHA theme.
        /// </summary>
        public string ReCaptchaTheme { get; set; }

        /// <summary>
        /// Gets or sets the ReCaptchaDefaultLanguage
        /// reCAPTCHA default language.
        /// </summary>
        public string ReCaptchaDefaultLanguage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether AutomaticallyChooseLanguage
        /// A value indicating whether reCAPTCHA language should be set automatically.
        /// </summary>
        public bool AutomaticallyChooseLanguage { get; set; }
    }
}
