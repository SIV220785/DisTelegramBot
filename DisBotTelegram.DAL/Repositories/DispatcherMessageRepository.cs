using DisBotTelegram.DAL.Entities;
using DisBotTelegram.DAL.UnitOfWork;
using System.Data.Entity;

namespace DisBotTelegram.DAL.Repositories
{
    internal class DispatcherMessageRepository: GenericRepository<DispatherMessage>
    {
        public DispatcherMessageRepository(DbContext db) : base(db)
        {
        }
    }
}
