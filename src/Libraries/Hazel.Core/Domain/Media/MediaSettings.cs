using Hazel.Core.Configuration;

namespace Hazel.Core.Domain.Media
{
    /// <summary>
    /// Media settings.
    /// </summary>
    public class MediaSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the AvatarPictureSize
        /// Picture size of applicationUser avatars (if enabled).
        /// </summary>
        public int AvatarPictureSize { get; set; }

        /// <summary>
        /// Gets or sets the ProductThumbPictureSize
        /// Picture size of product picture thumbs displayed on catalog pages (e.g. category details page).
        /// </summary>
        public int ProductThumbPictureSize { get; set; }

        /// <summary>
        /// Gets or sets the ProductDetailsPictureSize
        /// Picture size of the main product picture displayed on the product details page.
        /// </summary>
        public int ProductDetailsPictureSize { get; set; }

        /// <summary>
        /// Gets or sets the ProductThumbPictureSizeOnProductDetailsPage
        /// Picture size of the product picture thumbs displayed on the product details page.
        /// </summary>
        public int ProductThumbPictureSizeOnProductDetailsPage { get; set; }

        /// <summary>
        /// Gets or sets the AssociatedProductPictureSize
        /// Picture size of the associated product picture.
        /// </summary>
        public int AssociatedProductPictureSize { get; set; }

        /// <summary>
        /// Gets or sets the CategoryThumbPictureSize
        /// Picture size of category pictures.
        /// </summary>
        public int CategoryThumbPictureSize { get; set; }

        /// <summary>
        /// Gets or sets the ManufacturerThumbPictureSize
        /// Picture size of manufacturer pictures.
        /// </summary>
        public int ManufacturerThumbPictureSize { get; set; }

        /// <summary>
        /// Gets or sets the VendorThumbPictureSize
        /// Picture size of vendor pictures.
        /// </summary>
        public int VendorThumbPictureSize { get; set; }

        /// <summary>
        /// Gets or sets the CartThumbPictureSize
        /// Picture size of product pictures on the shopping cart page.
        /// </summary>
        public int CartThumbPictureSize { get; set; }

        /// <summary>
        /// Gets or sets the MiniCartThumbPictureSize
        /// Picture size of product pictures for minishipping cart box.
        /// </summary>
        public int MiniCartThumbPictureSize { get; set; }

        /// <summary>
        /// Gets or sets the AutoCompleteSearchThumbPictureSize
        /// Picture size of product pictures for autocomplete search box.
        /// </summary>
        public int AutoCompleteSearchThumbPictureSize { get; set; }

        /// <summary>
        /// Gets or sets the ImageSquarePictureSize
        /// Picture size of image squares on a product details page (used with "image squares" attribute type.
        /// </summary>
        public int ImageSquarePictureSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether DefaultPictureZoomEnabled
        /// A value indicating whether picture zoom is enabled.
        /// </summary>
        public bool DefaultPictureZoomEnabled { get; set; }

        /// <summary>
        /// Gets or sets the MaximumImageSize
        /// Maximum allowed picture size. If a larger picture is uploaded, then it'll be resized.
        /// </summary>
        public int MaximumImageSize { get; set; }

        /// <summary>
        /// Gets or sets the DefaultImageQuality
        /// Gets or sets a default quality used for image generation.
        /// </summary>
        public int DefaultImageQuality { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether single (/content/images/thumbs/) or multiple (/content/images/thumbs/001/ and /content/images/thumbs/002/) directories will used for picture thumbs.
        /// </summary>
        public bool MultipleThumbDirectories { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should use fast HASHBYTES (hash sum) database function to compare pictures when importing products.
        /// </summary>
        public bool ImportProductImagesUsingHash { get; set; }

        /// <summary>
        /// Gets or sets the AzureCacheControlHeader
        /// Gets or sets Azure CacheControl header (e.g. "max-age=3600, public").
        /// </summary>
        public string AzureCacheControlHeader { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether need to use absolute pictures path.
        /// </summary>
        public bool UseAbsoluteImagePath { get; set; }
    }
}
