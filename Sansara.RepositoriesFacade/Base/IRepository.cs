using Sansara.Database.Base;
using Sansara.Database.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sansara.RepositoriesFacade.Base
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        #region Sync (Delete, DeleteAll, Add, AddAll, Update, UpdateAll)
        T Get(string id);

        T Get(Expression<Func<T, bool>> predicate);

        IList<T> GetAll();

        IList<T> GetAll(Expression<Func<T, bool>> predicate);

        void Delete(string id);

        void DeleteAll(IEnumerable<string> ids);

        void Add(T entity);

        void AddAll(IEnumerable<T> entities);

        void Update(T entity);

        void UpdateAll(IEnumerable<T> entities);

        void UpdateRelationships(T entity);

        void UpdateAllRelationships(IEnumerable<T> entities);

        IQueryable<T> GetQueryable();
        #endregion

        #region Async (Count, Exist, Get, GetAll, GetPage, GetFirst, GetLast)
        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        bool Exist(string id);

        bool Exist(Expression<Func<T, bool>> predicate);

        Task<bool> ExistAsync(string id);

        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetAsync(string id);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetFirstAsync();

        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetLastAsync();

        Task<T> GetLastAsync(Expression<Func<T, bool>> predicate);

        Task<IList<T>> GetAllAsync();

        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        Task<IList<T>> GetPageAsync<TOrder>(int skip, int take, Expression<Func<T, TOrder>> orderBy, bool desc, Expression<Func<T, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        #endregion

        #region Save changes

        Task<OperationResult<T>> SaveChangesAsync(T updatedEntity);

        OperationResult SaveChanges();
        #endregion
    }
}
