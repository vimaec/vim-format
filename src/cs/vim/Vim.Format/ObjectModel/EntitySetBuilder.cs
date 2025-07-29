using System;
using System.Collections.Generic;
using System.Diagnostics;
using Vim.Util;

namespace Vim.Format.ObjectModel
{
    public class EntitySetBuilder<TEntity> where TEntity : Entity
    {
        public readonly string EntityTableName;
        public readonly List<TEntity> Entities = new List<TEntity>();
        public readonly IndexedSet<object> KeyToEntityIndex = new IndexedSet<object>();

        public EntitySetBuilder(string entityTableName)
        {
            EntityTableName = entityTableName;
        }

        /// <summary>
        /// Adds the entity but does not update the KeyToEntityIndex member.
        /// NOTE: Revit Parameters take up a *LOT* of memory and have previously caused System.OutOfMemoryExceptions
        /// when KeyToEntityIndex was trying to resize itself. To work around this issue, AddUntracked should be used
        /// when objects can be blindly added into the entity table.
        /// </summary>
        public TEntity AddUntracked(TEntity entity)
        {
            // Update the entity's index.
            var index = Entities.Count;
            entity.Index = index;

            Entities.Add(entity);

            return entity;
        }

        /// <summary>
        /// Adds the provided entity and assigns it to the given key. Tracks the entity in the 
        /// NOTE: this method mutates the entity's Index.
        /// </summary>
        public GetOrAddResult<TEntity> Add(object key, TEntity entity)
        {
            Debug.Assert(KeyToEntityIndex.Count == Entities.Count);

            var addedEntity = AddUntracked(entity); // mutates entity.Index; addedEntity == entity.

            KeyToEntityIndex.Add(key, addedEntity.Index);

            return new GetOrAddResult<TEntity>(addedEntity, true);
        }

        /// <summary>
        /// Adds the provided entity.
        /// NOTE: this method mutates the entity's Index.
        /// </summary>
        public GetOrAddResult<TEntity> Add(TEntity entity)
            => Add(new object(), entity);

        /// <summary>
        /// Gets or adds the provided entity based on the given key.
        /// NOTE: this method mutates the entity's Index.
        /// </summary>
        public GetOrAddResult<TEntity> GetOrAdd(object key, Func<object, TEntity> getEntity)
        {
            if (KeyToEntityIndex.TryGetValue(key, out var storedEntityIndex))
                return new GetOrAddResult<TEntity>(Entities[storedEntityIndex] as TEntity, false);

            Debug.Assert(KeyToEntityIndex.Count == Entities.Count);

            // Obtain the entity
            var entity = getEntity(key);
            if (entity == null)
                return new GetOrAddResult<TEntity>(null, false);

            // Add the entity and sets its index.
            return Add(key, entity);
        }

        /// <summary>
        /// Gets or adds the provided entity based on the given key.
        /// </summary>
        public GetOrAddResult<TEntity> GetOrAdd(object key, Func<TEntity> createEntity)
        {
            if (KeyToEntityIndex.TryGetValue(key, out var storedEntityIndex))
                return new GetOrAddResult<TEntity>(Entities[storedEntityIndex] as TEntity, false);

            Debug.Assert(KeyToEntityIndex.Count == Entities.Count);

            // Create the entity
            var entity = createEntity();
            if (entity == null)
                return new GetOrAddResult<TEntity>(null, false);

            // Add the entity and sets its index.
            return Add(key, entity);
        }

        /// <summary>
        /// Gets or adds the provided entity based on the given key.
        /// </summary>
        public GetOrAddResult<TEntity> GetOrAdd(object key, TEntity newEntity)
        {
            if (KeyToEntityIndex.TryGetValue(key, out var storedEntityIndex))
                return new GetOrAddResult<TEntity>(Entities[storedEntityIndex] as TEntity, false);

            Debug.Assert(KeyToEntityIndex.Count == Entities.Count);

            if (newEntity == null)
                return new GetOrAddResult<TEntity>(null, false);

            // Add the entity and sets its index.
            return Add(key, newEntity);
        }

        /// <summary>
        /// Returns true along with the entity if the key corresponds to an entity in the stored set.
        /// </summary>
        public bool TryGet(object key, out TEntity value)
        {
            value = null;

            if (!KeyToEntityIndex.TryGetValue(key, out var index))
                return false;

            value = Entities[index];

            return value != null;
        }
    }
}
