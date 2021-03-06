using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace HealthESB.Domain.IRepository
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        ValueTask<TEntity> GetById(int id);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetByPaging(int pageNum, int pageSize);
        Task Insert(TEntity model);
        void Insert2(TEntity entity);
        Task Edit(TEntity model);
        Task RemoveRange(IList<TEntity> Model);
        void Commit();
        Task CommitAsync();
        Task<String> Max(Expression<Func<TEntity, String>> predicate);
        TEntity FirstOrDefaultNormal(Expression<Func<TEntity, bool>> predicate);
        bool UpdateNormal(TEntity entity);
        Task<TEntity> LastOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}
