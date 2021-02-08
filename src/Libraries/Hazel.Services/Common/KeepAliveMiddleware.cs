using Hazel.Core;
using Hazel.Core.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Hazel.Services.Common
{
    /// <summary>
    /// Represents middleware that checks whether request is for keep alive.
    /// </summary>
    public class KeepAliveMiddleware
    {
        /// <summary>
        /// Defines the _next.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeepAliveMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next<see cref="RequestDelegate"/>.</param>
        public KeepAliveMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke middleware actions.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        /// <param name="webHelper">Web helper.</param>
        /// <returns>Task.</returns>
        public async Task Invoke(HttpContext context, IWebHelper webHelper)
        {
            //whether database is installed
            if (DataSettingsManager.DatabaseIsInstalled)
            {
                //keep alive page requested (we ignore it to prevent creating a guest ApplicationUser records)
                var keepAliveUrl = $"{webHelper.GetStoreLocation()}{HazelCommonDefaults.KeepAlivePath}";
                if (webHelper.GetThisPageUrl(false).StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                    return;
            }

            //or call the next middleware in the request pipeline
            await _next(context);
        }
    }
}
