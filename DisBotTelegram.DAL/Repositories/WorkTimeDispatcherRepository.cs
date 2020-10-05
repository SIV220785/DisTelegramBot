using DisBotTelegram.DAL.Entities;
using DisBotTelegram.DAL.UnitOfWork;
using System.Data.Entity;

namespace DisBotTelegram.DAL.Repositories
{
    internal class WorkTimeDispatcherRepository: GenericRepository<WorkTimeDispatcher>
    {
        public WorkTimeDispatcherRepository(DbContext db) : base(db)
        {
        }
    }
}
