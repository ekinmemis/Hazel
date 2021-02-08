using System.Collections.Generic;

namespace Hazel.Core.Domain.Messages
{
    /// <summary>
    /// A container for tokens that are added.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="U">.</typeparam>
    public class EntityTokensAddedEvent<TEntity, U> where TEntity : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityTokensAddedEvent{TEntity, U}"/> class.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="tokens">Tokens.</param>
        public EntityTokensAddedEvent(TEntity entity, IList<U> tokens)
        {
            Entity = entity;
            Tokens = tokens;
        }

        /// <summary>
        /// Gets the Entity
        /// Entity.
        /// </summary>
        public TEntity Entity { get; }

        /// <summary>
        /// Gets the Tokens
        /// Tokens.
        /// </summary>
        public IList<U> Tokens { get; }
    }
}
