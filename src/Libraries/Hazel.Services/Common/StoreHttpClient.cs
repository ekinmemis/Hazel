using Hazel.Core;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hazel.Services.Common
{
    /// <summary>
    /// Represents the HTTP client to request current store.
    /// </summary>
    public partial class StoreHttpClient
    {
        /// <summary>
        /// Defines the _httpClient.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreHttpClient"/> class.
        /// </summary>
        /// <param name="client">The client<see cref="HttpClient"/>.</param>
        /// <param name="webHelper">The webHelper<see cref="IWebHelper"/>.</param>
        public StoreHttpClient(HttpClient client,
            IWebHelper webHelper)
        {
            //configure client
            client.BaseAddress = new Uri(webHelper.GetStoreLocation());

            _httpClient = client;
        }

        /// <summary>
        /// Keep the current store site alive.
        /// </summary>
        /// <returns>The asynchronous task whose result determines that request completed.</returns>
        public virtual async Task KeepAliveAsync()
        {
            await _httpClient.GetStringAsync(HazelCommonDefaults.KeepAlivePath);
        }
    }
}
