using DisBotTelegram.DAL.Entities.Base;
using System;

namespace DisBotTelegram.DAL.UnitOfWork.Base
{
    internal interface IUnitOfWork : IDisposable
    {
        void Commit();
        IRepository<T> GetRepository<T>() where T : class, IBaseEntity;
    }
}
