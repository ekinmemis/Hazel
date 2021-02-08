namespace Hazel.Core.Events
{
    /// <summary>
    /// A container for passing entities that have been deleted. This is not used for entities that are deleted logically via a bit column.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public class EntityDeletedEvent<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDeletedEvent{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public EntityDeletedEvent(TEntity entity)
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
