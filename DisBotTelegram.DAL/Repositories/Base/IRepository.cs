using DisBotTelegram.DAL.Entities.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DisBotTelegram.DAL.UnitOfWork
{
    internal interface IRepository<T> where T : class, IBaseEntity
    {
        T Get(int id);
        T GetIncluding(int id, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        T Add(T t);

        T Find(Expression<Func<T, bool>> match);
        T FindIncluding(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindAll(Expression<Func<T, bool>> match);
        IQueryable<T> FindAllIncluding(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeProperties);

        int Delete(T entity);
        int DeleteBy(int id);

        T Update(T t, object key);

        int Count();

        void Save();
        void Dispose();
    }
}
