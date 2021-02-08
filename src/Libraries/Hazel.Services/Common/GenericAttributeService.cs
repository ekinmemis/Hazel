using Hazel.Core;
using Hazel.Core.Caching;
using Hazel.Core.Domain.Common;
using Hazel.Data;
using Hazel.Data.Extensions;
using Hazel.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hazel.Services.Common
{
    /// <summary>
    /// Generic attribute service.
    /// </summary>
    public partial class GenericAttributeService : IGenericAttributeService
    {
        /// <summary>
        /// Defines the _cacheManager.
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Defines the _eventPublisher.
        /// </summary>
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Defines the _genericAttributeRepository.
        /// </summary>
        private readonly IRepository<GenericAttribute> _genericAttributeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericAttributeService"/> class.
        /// </summary>
        /// <param name="cacheManager">The cacheManager<see cref="ICacheManager"/>.</param>
        /// <param name="eventPublisher">The eventPublisher<see cref="IEventPublisher"/>.</param>
        /// <param name="genericAttributeRepository">The genericAttributeRepository<see cref="IRepository{GenericAttribute}"/>.</param>
        public GenericAttributeService(ICacheManager cacheManager,
            IEventPublisher eventPublisher,
            IRepository<GenericAttribute> genericAttributeRepository)
        {
            _cacheManager = cacheManager;
            _eventPublisher = eventPublisher;
            _genericAttributeRepository = genericAttributeRepository;
        }

        /// <summary>
        /// Deletes an attribute.
        /// </summary>
        /// <param name="attribute">Attribute.</param>
        public virtual void DeleteAttribute(GenericAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));

            _genericAttributeRepository.Delete(attribute);

            //cache
            _cacheManager.RemoveByPrefix(HazelCommonDefaults.GenericAttributePrefixCacheKey);

            //event notification
            _eventPublisher.EntityDeleted(attribute);
        }

        /// <summary>
        /// Deletes an attributes.
        /// </summary>
        /// <param name="attributes">Attributes.</param>
        public virtual void DeleteAttributes(IList<GenericAttribute> attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            _genericAttributeRepository.Delete(attributes);

            //cache
            _cacheManager.RemoveByPrefix(HazelCommonDefaults.GenericAttributePrefixCacheKey);

            //event notification
            foreach (var attribute in attributes)
            {
                _eventPublisher.EntityDeleted(attribute);
            }
        }

        /// <summary>
        /// Gets an attribute.
        /// </summary>
        /// <param name="attributeId">Attribute identifier.</param>
        /// <returns>An attribute.</returns>
        public virtual GenericAttribute GetAttributeById(int attributeId)
        {
            if (attributeId == 0)
                return null;

            return _genericAttributeRepository.GetById(attributeId);
        }

        /// <summary>
        /// Inserts an attribute.
        /// </summary>
        /// <param name="attribute">attribute.</param>
        public virtual void InsertAttribute(GenericAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));

            _genericAttributeRepository.Insert(attribute);

            //cache
            _cacheManager.RemoveByPrefix(HazelCommonDefaults.GenericAttributePrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(attribute);
        }

        /// <summary>
        /// Updates the attribute.
        /// </summary>
        /// <param name="attribute">Attribute.</param>
        public virtual void UpdateAttribute(GenericAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));

            _genericAttributeRepository.Update(attribute);

            //cache
            _cacheManager.RemoveByPrefix(HazelCommonDefaults.GenericAttributePrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(attribute);
        }

        /// <summary>
        /// Get attributes.
        /// </summary>
        /// <param name="entityId">Entity identifier.</param>
        /// <param name="keyGroup">Key group.</param>
        /// <returns>Get attributes.</returns>
        public virtual IList<GenericAttribute> GetAttributesForEntity(int entityId, string keyGroup)
        {
            var key = string.Format(HazelCommonDefaults.GenericAttributeCacheKey, entityId, keyGroup);
            return _cacheManager.Get(key, () =>
            {
                var query = from ga in _genericAttributeRepository.Table
                            where ga.EntityId == entityId &&
                            ga.KeyGroup == keyGroup
                            select ga;
                var attributes = query.ToList();
                return attributes;
            });
        }

        /// <summary>
        /// Save attribute value.
        /// </summary>
        /// <typeparam name="TPropType">Property type.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <param name="storeId">Store identifier; pass 0 if this attribute will be available for all stores.</param>
        public virtual void SaveAttribute<TPropType>(BaseEntity entity, string key, TPropType value, int storeId = 0)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var keyGroup = entity.GetUnproxiedEntityType().Name;

            var props = GetAttributesForEntity(entity.Id, keyGroup)
                .Where(x => x.StoreId == storeId)
                .ToList();
            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            var valueStr = CommonHelper.To<string>(value);

            if (prop != null)
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                {
                    //delete
                    DeleteAttribute(prop);
                }
                else
                {
                    //update
                    prop.Value = valueStr;
                    UpdateAttribute(prop);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                    return;

                //insert
                prop = new GenericAttribute
                {
                    EntityId = entity.Id,
                    Key = key,
                    KeyGroup = keyGroup,
                    Value = valueStr,
                    StoreId = storeId
                };

                InsertAttribute(prop);
            }
        }

        /// <summary>
        /// Get an attribute of an entity.
        /// </summary>
        /// <typeparam name="TPropType">Property type.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <param name="key">Key.</param>
        /// <param name="storeId">Load a value specific for a certain store; pass 0 to load a value shared for all stores.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Attribute.</returns>
        public virtual TPropType GetAttribute<TPropType>(BaseEntity entity, string key, int storeId = 0, TPropType defaultValue = default(TPropType))
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var keyGroup = entity.GetUnproxiedEntityType().Name;

            var props = GetAttributesForEntity(entity.Id, keyGroup);

            //little hack here (only for unit testing). we should write expect-return rules in unit tests for such cases
            if (props == null)
                return defaultValue;

            props = props.Where(x => x.StoreId == storeId).ToList();
            if (!props.Any())
                return defaultValue;

            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            if (prop == null || string.IsNullOrEmpty(prop.Value))
                return defaultValue;

            return CommonHelper.To<TPropType>(prop.Value);
        }
    }
}
