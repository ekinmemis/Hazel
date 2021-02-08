using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Hazel.Core.Http.Extensions
{
    /// <summary>
    /// Represents extensions of ISession.
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Set value to Session.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="session">Session.</param>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public static void Set<TEntity>(this ISession session, string key, TEntity value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Get value from session.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="session">Session.</param>
        /// <param name="key">Key.</param>
        /// <returns>Value.</returns>
        public static TEntity Get<TEntity>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value == null)
                return default;

            return JsonConvert.DeserializeObject<TEntity>(value);
        }
    }
}
