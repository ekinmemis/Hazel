//This product includes GeoLite2 data created by MaxMind, available from http://www.maxmind.com

using Hazel.Core.Infrastructure;
using Hazel.Services.Logging;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;
using MaxMind.GeoIP2.Responses;
using System;

namespace Hazel.Services.Directory
{
    /// <summary>
    /// GEO lookup service.
    /// </summary>
    public partial class GeoLookupService : IGeoLookupService
    {
        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Defines the _fileProvider.
        /// </summary>
        private readonly IHazelFileProvider _fileProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLookupService"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="fileProvider">The fileProvider<see cref="IHazelFileProvider"/>.</param>
        public GeoLookupService(ILogger logger,
            IHazelFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
        }

        /// <summary>
        /// Get information.
        /// </summary>
        /// <param name="ipAddress">IP address.</param>
        /// <returns>Information.</returns>
        protected virtual CountryResponse GetInformation(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return null;

            try
            {
                //This product includes GeoLite2 data created by MaxMind, available from http://www.maxmind.com
                var databasePath = _fileProvider.MapPath("~/App_Data/GeoLite2-Country.mmdb");
                var reader = new DatabaseReader(databasePath);
                var omni = reader.Country(ipAddress);
                return omni;
                //more info: http://maxmind.github.io/GeoIP2-dotnet/
                //more info: https://github.com/maxmind/GeoIP2-dotnet
                //more info: http://dev.maxmind.com/geoip/geoip2/geolite2/
                //Console.WriteLine(omni.Country.IsoCode); // 'US'
                //Console.WriteLine(omni.Country.Name); // 'United States'
                //Console.WriteLine(omni.Country.Names["zh-CN"]); // '美国'
                //Console.WriteLine(omni.MostSpecificSubdivision.Name); // 'Minnesota'
                //Console.WriteLine(omni.MostSpecificSubdivision.IsoCode); // 'MN'
                //Console.WriteLine(omni.City.Name); // 'Minneapolis'
                //Console.WriteLine(omni.Postal.Code); // '55455'
                //Console.WriteLine(omni.Location.Latitude); // 44.9733
                //Console.WriteLine(omni.Location.Longitude); // -93.2323
            }
            //catch (AddressNotFoundException exc)
            catch (GeoIP2Exception)
            {
                //address is not found
                //do not throw exceptions
                return null;
            }
            catch (Exception exc)
            {
                //do not throw exceptions
                _logger.Warning("Cannot load MaxMind record", exc);
                return null;
            }
        }

        /// <summary>
        /// Get country ISO code.
        /// </summary>
        /// <param name="ipAddress">IP address.</param>
        /// <returns>Country name.</returns>
        public virtual string LookupCountryIsoCode(string ipAddress)
        {
            var response = GetInformation(ipAddress);
            if (response?.Country != null)
                return response.Country.IsoCode;

            return string.Empty;
        }

        /// <summary>
        /// Get country name.
        /// </summary>
        /// <param name="ipAddress">IP address.</param>
        /// <returns>Country name.</returns>
        public virtual string LookupCountryName(string ipAddress)
        {
            var response = GetInformation(ipAddress);
            if (response?.Country != null)
                return response.Country.Name;

            return string.Empty;
        }
    }
}
