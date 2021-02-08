using Hazel.Core.Caching;
using Hazel.Core.Domain.Directory;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hazel.Services.Directory
{
    /// <summary>
    /// Current (for caching).
    /// </summary>
    [Serializable]
    //Entity Framework will assume that any class that inherits from a POCO class that is mapped to a table on the database requires a Discriminator column
    //That's why we have to add [NotMapped] as an attribute of the derived class.
    [NotMapped]
    public class CurrencyForCaching : Currency, IEntityForCaching
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyForCaching"/> class.
        /// </summary>
        public CurrencyForCaching()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyForCaching"/> class.
        /// </summary>
        /// <param name="c">Currency to copy.</param>
        public CurrencyForCaching(Currency c)
        {
            Id = c.Id;
            Name = c.Name;
            CurrencyCode = c.CurrencyCode;
            Rate = c.Rate;
            DisplayLocale = c.DisplayLocale;
            CustomFormatting = c.CustomFormatting;
            LimitedToStores = c.LimitedToStores;
            Published = c.Published;
            DisplayOrder = c.DisplayOrder;
            CreatedOnUtc = c.CreatedOnUtc;
            UpdatedOnUtc = c.UpdatedOnUtc;
            RoundingTypeId = c.RoundingTypeId;
        }
    }
}
