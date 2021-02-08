namespace Hazel.Core.Events
{
    /// <summary>
    /// A container for entities that have been inserted.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public class EntityInsertedEvent<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityInsertedEvent{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public EntityInsertedEvent(TEntity entity)
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
