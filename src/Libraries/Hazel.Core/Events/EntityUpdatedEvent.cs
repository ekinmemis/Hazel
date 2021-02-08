namespace Hazel.Core.Events
{
    /// <summary>
    /// A container for entities that are updated.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public class EntityUpdatedEvent<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityUpdatedEvent{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public EntityUpdatedEvent(TEntity entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Gets the Entity
        /// Entity.
        /// </summary>
        public TEntity Entity { get; }
    }
}
