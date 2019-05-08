using Microsoft.EntityFrameworkCore;
using Sansara.Database;
using Sansara.Database.Base;
using Sansara.Database.Helpers;
using Sansara.RepositoriesFacade.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sansara.Repositories.Base
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey>, new() where TKey : struct
    {
        protected DbSet<T> Set
        {
            get { return Context.Set<T>(); }
        }

        protected SansaraContext Context { get; set; }

        public Repository(SansaraContext context)
        {
            Context = context;
        }

        #region Sync (Delete, DeleteAll, Add, AddAll, Update, UpdateAll)

        public T Get(TKey id)
        {
            return Set.AsNoTracking().SingleOrDefault(Equals(x => x.Id, id));
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return Set.AsNoTracking().FirstOrDefault(predicate);
        }

        public virtual IList<T> GetAll()
        {
            return Set.AsNoTracking().ToList();
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return Set.AsNoTracking().Where(predicate).ToList();
        }

        public virtual void Delete(TKey id)
        {
            var entity = new T { Id = id };
            Set.Attach(entity);
            Set.Remove(entity);
        }

        public virtual void DeleteAll(IEnumerable<TKey> ids)
        {
            var entities = ids.Select(id => new T { Id = id }).Select(entity => Set.Attach(entity).Entity).ToList();
            var t = entities.Select(e => (T)e);
            Set.RemoveRange(entities);
        }

        public virtual void Add(T entity)
        {
            Set.Add((T)entity);
        }

        public virtual void AddAll(IEnumerable<T> entities)
        {
            Set.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            var local = Set.Local.FirstOrDefault(Equals(x => x.Id, entity.Id).Compile());
            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            Set.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateAll(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public virtual void UpdateRelationships(T entity)
        {
            Set.Add(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateAllRelationships(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                UpdateRelationships(entity);
            }
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return Set.AsQueryable<T>();
        }
        #endregion

        #region Async (Count, Exist, Get, GetAll, GetPage, GetFirst, GetLast)

        public virtual async Task<int> CountAsync()
        {
            return await Set.AsNoTracking().CountAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.AsNoTracking().CountAsync(predicate).ConfigureAwait(false);
        }

        public virtual async Task<bool> ExistAsync(TKey id)
        {
            return await Set.AsNoTracking().AnyAsync(Equals(x => x.Id, id)).ConfigureAwait(false);
        }

        public virtual async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.AsNoTracking().AnyAsync(predicate).ConfigureAwait(false);
        }

        public virtual bool Exist(TKey id)
        {
            return Set.AsNoTracking().Any(Equals(x => x.Id, id));
        }

        public virtual bool Exist(Expression<Func<T, bool>> predicate)
        {
            return Set.AsNoTracking().Any(predicate);
        }

        public virtual async Task<T> GetAsync(TKey id)
        {
            return await Set.AsNoTracking().SingleOrDefaultAsync(Equals(x => x.Id, id)).ConfigureAwait(false);
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.AsNoTracking().SingleOrDefaultAsync(predicate).ConfigureAwait(false);
        }

        public virtual async Task<T> GetFirstAsync()
        {
            return await Set.AsNoTracking().FirstAsync().ConfigureAwait(false);
        }

        public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.AsNoTracking().FirstAsync(predicate).ConfigureAwait(false);
        }

        public virtual async Task<T> GetLastAsync()
        {
            return await Set.AsNoTracking().OrderByDescending(e => e.Id).FirstAsync().ConfigureAwait(false);
        }

        public virtual async Task<T> GetLastAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.AsNoTracking().OrderByDescending(e => e.Id).FirstAsync(predicate).ConfigureAwait(false);
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await Set.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await Set.AsNoTracking().Where(predicate).ToListAsync().ConfigureAwait(false);
            }
            catch
            {
                return await Set.AsNoTracking().Where(predicate).ToListAsync().ConfigureAwait(false);
            }
        }

        public virtual async Task<IList<T>> GetPageAsync<TOrder>(int skip, int take, Expression<Func<T, TOrder>> orderBy,
            bool desc, Expression<Func<T, bool>> predicate = null)
        {
            var query = from e in Set select e;
            if (predicate != null)
            {
                query = Set.AsNoTracking().Where(predicate);
            }

            query = desc ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            return await query.Skip(skip).Take(take).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.AsNoTracking().AnyAsync(predicate).ConfigureAwait(false);
        }

        #endregion

        #region Save changes

        public virtual async Task<OperationResult<T>> SaveChangesAsync(T updatedEntity)
        {
            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
                return new OperationResult<T>(true, updatedEntity);
            }
            catch (Exception ex)
            {
                return new OperationResult<T>(ex, updatedEntity);
            }
        }

        public virtual OperationResult SaveChanges()
        {
            try
            {
                Context.SaveChanges();
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                return new OperationResult(ex);
            }
        }
        #endregion

        #region Private methods
        // Equals for generic keys in EF, see http://stackoverflow.com/questions/10402029/ef-object-comparison-with-generic-types 
        private static Expression<Func<T, bool>> Equals(Expression<Func<T, TKey>> property, TKey value)
        {
            var left = property.Body;
            var right = Expression.Constant(value, typeof(TKey));
            return Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), new[] { property.Parameters.Single() });
        }
        #endregion
    }
}
