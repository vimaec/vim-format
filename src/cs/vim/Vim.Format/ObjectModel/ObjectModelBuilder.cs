using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.Format.Utils;

namespace Vim.Format.ObjectModel
{
    public readonly struct GetOrAddResult<TEntity> where TEntity : Entity
    {
        public readonly TEntity Entity;
        public readonly bool Added;

        public GetOrAddResult(TEntity entity, bool added)
            => (Entity, Added) = (entity, added);

        public static GetOrAddResult<TEntity> Empty
            => new GetOrAddResult<TEntity>(null, false);

        public bool IsEmpty
            => Entity == null;

        public void Deconstruct(out TEntity entity, out bool added)
            => (entity, added) = (Entity, Added);
    }

    /// <summary>
    /// This class makes it easy to fill out the entity tables of a VIM, by allowing the user to specify a mapping
    /// between objects in the source domain (e.g. Revit.Element) and entities. This allows a programmer to not
    /// have to worry about adding the same element twice, and allows them to add entity objects to the document
    /// builder in a single pass without worrying about lookup tables.
    /// </summary>
    public partial class ObjectModelBuilder
    {
        public class EntityTableBuilder
        {
            public readonly List<Entity> Entities = new List<Entity>();
            public readonly IndexedSet<object> KeyToEntityIndex = new IndexedSet<object>();

            /// <summary>
            /// Adds the provided entity and assigns it to the given key.
            /// NOTE: this method mutates the entity's Index.
            /// </summary>
            public GetOrAddResult<TEntity> Add<TEntity>(object key, TEntity entity) where TEntity : Entity
            {
                Debug.Assert(KeyToEntityIndex.Count == Entities.Count);

                // Update the entity's index.
                var index = Entities.Count;
                entity.Index = index;

                // Add the entity to the local collections.
                Entities.Add(entity);
                KeyToEntityIndex.Add(key, index);

                return new GetOrAddResult<TEntity>(entity, true);
            }

            /// <summary>
            /// Adds the provided entity.
            /// NOTE: this method mutates the entity's Index.
            /// </summary>
            public GetOrAddResult<TEntity> Add<TEntity>(TEntity entity) where TEntity : Entity
                => Add(new object(), entity);

            /// <summary>
            /// Gets or adds the provided entity based on the given key.
            /// NOTE: this method mutates the entity's Index.
            /// </summary>
            public GetOrAddResult<TEntity> GetOrAdd<TEntity>(object key, Func<object, TEntity> getEntity) where TEntity : Entity
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
        }

        public DocumentBuilder AddEntityTablesToDocumentBuilder(DocumentBuilder db)
        {
            foreach (var kv in EntityTableBuilders)
            {
                var tableName = kv.Key.GetEntityTableName();
                var tb = kv.Key.GetTableBuilderFunc()(kv.Value.Entities);
                db.Tables.Add(tableName, tb);
            }
            return db;
        }

        public EntityTableBuilder GetOrAddEntityTableBuilder<TEntity>() where TEntity : Entity
            => EntityTableBuilders.GetOrCompute(typeof(TEntity), _ => new EntityTableBuilder());

        public GetOrAddResult<TEntity> GetOrAdd<TEntity>(object key, Func<object, TEntity> getEntity) where TEntity : Entity
            => GetOrAddEntityTableBuilder<TEntity>().GetOrAdd(key, getEntity);

        public TEntity Add<TEntity>(TEntity e) where TEntity : Entity
            => GetOrAddEntityTableBuilder<TEntity>().Add(e).Entity;

        public bool TryGet<TEntity>(object key, out TEntity value) where TEntity : Entity
        {
            value = default;
            
            if (!EntityTableBuilders.TryGetValue(typeof(TEntity), out var entityTableBuilder))
                return false;

            if (!entityTableBuilder.KeyToEntityIndex.TryGetValue(key, out var index))
                return false;

            value = entityTableBuilder.Entities[index] as TEntity;
            return value != null;
        }

        public TEntity GetOrDefault<TEntity>(object key) where TEntity : Entity
            => TryGet<TEntity>(key, out var e) ? e : default;

        public DocumentBuilder ToDocumentBuilder(string generator, string versionString)
            => AddEntityTablesToDocumentBuilder(new DocumentBuilder(generator, SchemaVersion.Current, versionString));

        public bool HasBuilder(Type t)
            => EntityTableBuilders.ContainsKey(t);

        public bool HasKey(Type t, object key)
            => HasBuilder(t) && EntityTableBuilders[t].KeyToEntityIndex.ContainsKey(key);

        public IndexedSet<object> GetLookup<TEntity>()
            => GetLookup(typeof(TEntity));

        public IndexedSet<object> GetLookup(Type t)
            => EntityTableBuilders[t].KeyToEntityIndex;

        public int IndexOf<TEntity>(object key)
            => GetLookup<TEntity>().GetOrDefault(key, -1);

        public EntityTableBuilder GetEntityTableBuilder<TEntity>()
            => GetEntityTableBuilder(typeof(TEntity));

        public EntityTableBuilder GetEntityTableBuilder(Type t)
            => EntityTableBuilders.GetOrDefault(t);

        public IEnumerable<TEntity> GetEntities<TEntity>() where TEntity : Entity
            => GetEntityTableBuilder<TEntity>()?.Entities?.Cast<TEntity>();

        public TEntity GetEntityByIndex<TEntity>(int index) where TEntity : Entity
            => GetEntityTableBuilder<TEntity>()?.Entities[index] as TEntity;
    }
}
