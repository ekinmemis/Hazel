using Hazel.Core;
using Hazel.Core.Caching;
using System;

namespace Hazel.Data.Extensions
{
    /// <summary>
    /// Represents extensions.
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Check whether an entity is proxy.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns>Result.</returns>
        private static bool IsProxy(this BaseEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //in EF 6 we could use ObjectContext.GetObjectType. Now it's not available. Here is a workaround:

            var type = entity.GetType();
            //e.g. "ApplicationUserProxy" will be derived from "ApplicationUser". And "ApplicationUser" is derived from BaseEntity
            return type.BaseType != null && type.BaseType.BaseType != null && type.BaseType.BaseType == typeof(BaseEntity);
        }

        /// <summary>
        /// Get unproxied entity type.
        /// </summary>
        /// <param name="entity">.</param>
        /// <returns>.</returns>
        public static Type GetUnproxiedEntityType(this BaseEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Type type = null;
            //cachable entity (get the base entity type)
            if (entity is IEntityForCaching)
                type = ((IEntityForCaching)entity).GetType().BaseType;
            //EF proxy
            else if (entity.IsProxy())
                type = entity.GetType().BaseType;
            //not proxied entity
            else
                type = entity.GetType();

            if (type == null)
                throw new Exception("Original entity type cannot be loaded");

            return type;
        }
    }
}
