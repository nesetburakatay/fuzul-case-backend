using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> xAddAsync(T entity)
        {
           entity.isDeleted = false;
            await _dbSet.AddAsync(entity);
            return entity;
        }
        public void xDeleteById(long id)
        {
            T tempEntity = _dbSet.FirstOrDefault(x => x.Id == id);
            if (tempEntity != null)
                _dbSet.Remove(tempEntity);
        }
        public void xDelete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public T xUpdate(T entity)
        {
            if (entity.Id == 0)
                entity.Id = -1;
            _dbSet.Update(entity);
            return entity;
        }
        public async Task<int> xSaveChangesAsync()
        {
            int efectedRows = await _context.SaveChangesAsync();
            return efectedRows;
        }
        public int xSaveChanges()
        {
            int efectedRows = _context.SaveChanges();
            return efectedRows;
        }



        public IQueryable<T> xGetAll(bool tracking = false)
        {
            IQueryable<T> query = _dbSet.AsQueryable<T>();

            if (tracking)
                query = _dbSet.AsNoTracking();

            return query;
        }

        public async Task<T> xGetByIdAsync(long id, bool tracking = false)
        {
            IQueryable<T> query = _dbSet.AsQueryable<T>();

            if (tracking)
                query = _dbSet.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
            //return await _dbSet.FindAsync(id);
            //return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<T> xGetFirstOrDefaultAsync()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }
        public async Task<T> xGetFirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

       


    }
}
