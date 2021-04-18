using HealthESB.Domain.Entities;
using HealthESB.Framework.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace HealthESB.Domain.IRepository
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
    where TEntity : BaseEntity, new()
    where TContext : DbContext
    {
        protected TContext Context;

        public GenericRepository(TContext context)
        {
            Context = context;

        }

        public virtual   System.Threading.Tasks.ValueTask<TEntity> GetById(int id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }
        public Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
        public Task<TEntity> LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().LastOrDefaultAsync(predicate);
        }
        public TEntity FirstOrDefaultNormal(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }
        public virtual async Task Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }


        public virtual async Task Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        public bool UpdateNormal(TEntity entity)
        {
            // In case AsNoTracking is used
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return true;
        }
        public Task Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {

            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() => Context.Set<TEntity>().CountAsync();

        public Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate)
            => Context.Set<TEntity>().CountAsync(predicate);

        public async Task<List<TEntity>> GetByPaging(int pageNum, int pageSize)
        {
            return await Context.Set<TEntity>().Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();

        }

        public async Task Insert(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }
        public void Insert2(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }
        public void Commit()
        {
            Context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }
        public async Task Edit(TEntity entity)
        {
            this.Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

        }

        public async Task RemoveRange(IList<TEntity> Model)
        {

            Context.RemoveRange(Model);

        }
        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }


        public async Task<String> Max(Expression<Func<TEntity, String>> predicate)
        {
            return await Context.Set<TEntity>().Select(predicate).MaxAsync();
        }
       
    }
}
