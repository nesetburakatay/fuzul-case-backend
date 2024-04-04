using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IGenericRepository<T> where T : class, IBaseEntity, new()
    {
        Task<T> xAddAsync(T entity);
        T xUpdate(T entity);
        void xDelete(T entity);
        void xDeleteById(long id);
        Task<int> xSaveChangesAsync();
        int xSaveChanges();


        IQueryable<T> xGetAll(bool tracking = false);
        Task<T> xGetByIdAsync(long id, bool tracking = false);
        Task<T> xGetFirstOrDefaultAsync();
        Task<T> xGetFirstOrDefaultAsync(Expression<Func<T, bool>> expression);


    }
}
