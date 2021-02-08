namespace Hazel.Services.Events
{
    /// <summary>
    /// Consumer interface.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public interface IConsumer<TEntity>
    {
        /// <summary>
        /// Handle event.
        /// </summary>
        /// <param name="eventMessage">Event.</param>
        void HandleEvent(TEntity eventMessage);
    }
}
