using Hazel.Core;
using Hazel.Core.Events;

namespace Hazel.Services.Events
{
    /// <summary>
    /// Event publisher extensions.
    /// </summary>
    public static class EventPublisherExtensions
    {
        /// <summary>
        /// Entity inserted.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="eventPublisher">Event publisher.</param>
        /// <param name="entity">Entity.</param>
        public static void EntityInserted<TEntity>(this IEventPublisher eventPublisher, TEntity entity) where TEntity : BaseEntity
        {
            eventPublisher.Publish(new EntityInsertedEvent<TEntity>(entity));
        }

        /// <summary>
        /// Entity updated.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="eventPublisher">Event publisher.</param>
        /// <param name="entity">Entity.</param>
        public static void EntityUpdated<TEntity>(this IEventPublisher eventPublisher, TEntity entity) where TEntity : BaseEntity
        {
            eventPublisher.Publish(new EntityUpdatedEvent<TEntity>(entity));
        }

        /// <summary>
        /// Entity deleted.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="eventPublisher">Event publisher.</param>
        /// <param name="entity">Entity.</param>
        public static void EntityDeleted<TEntity>(this IEventPublisher eventPublisher, TEntity entity) where TEntity : BaseEntity
        {
            eventPublisher.Publish(new EntityDeletedEvent<TEntity>(entity));
        }
    }
}
