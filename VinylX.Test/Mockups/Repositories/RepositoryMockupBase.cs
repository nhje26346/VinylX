using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VinylX.Repositories;

namespace VinylX.Test.Mockups.Repositories
{
    internal abstract class RepositoryMockupBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private static List<TEntity> savedList = new List<TEntity>();
        private static List<TEntity> workingList = new List<TEntity>();

        private readonly RepositoryFoundationMockup repositoryFoundation;

        public RepositoryMockupBase(IRepositoryFoundation repositoryFoundation)
        {
            this.repositoryFoundation = repositoryFoundation as RepositoryFoundationMockup ?? throw new Exception($"{nameof(RepositoryFoundationMockup)} mas not the registered implementation for service: {nameof(IRepositoryFoundation)}");

            this.repositoryFoundation.AddOnSaveAction(() => 
            {
                // When we perform a save operation we copy all entities from the working list to the saved list
                savedList = Clone(workingList).ToList();
            });
        }

        #region IRepository<TEntity>

        public IQueryable<TEntity> Queryable => Clone(savedList).AsQueryable();

        public TEntity Add(TEntity entity)
        {
            ValidateEntityHasNoId(entity);
            var saved = Clone(entity);
            var result = Clone(entity);
            SetId(saved, NewId());
            workingList.Add(saved);
            repositoryFoundation.AddOnSaveAction(() => 
            {
                // Assign id to returned entity when a save operation is performed
                SetId(result, GetId(saved));
            });
            return result;
        }

        public ValueTask<TEntity?> FindAsync(params object?[]? keyValues)
        {
            throw new NotImplementedException();
        }

        public TEntity Remove(TEntity entity)
        {
            ValidateEntityHasId(entity);
            var result = RemoveFromList(workingList, entity) ?? entity;
            return Clone(result);
        }

        public TEntity Update(TEntity entity)
        {
            ValidateEntityHasId(entity);
            var match = RemoveFromList(workingList, entity);
            if (match == null)
            {
                throw new Exception($"Could not update entity {nameof(TEntity)}: Entity could not be found");
            }
            workingList.Add(entity);
            return Clone(entity);
        }

        #endregion

        protected abstract int GetId(TEntity entity);

        protected abstract void SetId(TEntity entity, int id);

        private static TEntity Clone(TEntity entity) 
        {
            var serialized = JsonSerializer.Serialize(entity);
            return JsonSerializer.Deserialize<TEntity>(serialized) ?? throw new Exception($"{nameof(JsonSerializer)}.{nameof(JsonSerializer.Deserialize)} returned null!"); ;
        }

        private static IEnumerable<TEntity> Clone(IEnumerable<TEntity> entities) => entities.Select(Clone).ToArray();

        private static int NewId() => Random.Shared.Next(1, int.MaxValue);

        private void ValidateEntityHasId(TEntity entity)
        {
            if (GetId(entity) <= 0)
            {
                throw new Exception($"Entity {nameof(TEntity)} has no ID!");
            }
        }

        private void ValidateEntityHasNoId(TEntity entity)
        {
            if (GetId(entity) > 0)
            {
                throw new Exception($"Entity {nameof(TEntity)} has an ID which was not expected!");
            }
        }

        private TEntity? RemoveFromList(List<TEntity> entities, TEntity entity)
        {
            var toRemoveId = GetId(entity);
            var toRemove = entities.SingleOrDefault(e => GetId(e) == toRemoveId);
            if (toRemove != null)
            {
                entities.Remove(toRemove);
            }
            return toRemove;
        }
    }
}
