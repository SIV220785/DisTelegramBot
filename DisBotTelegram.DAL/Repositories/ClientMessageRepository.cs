using DisBotTelegram.DAL.Entities;
using DisBotTelegram.DAL.UnitOfWork;
using System.Data.Entity;

namespace DisBotTelegram.DAL.Repositories
{
    internal class ClientMessageRepository : GenericRepository<ClientMessage>
    {
        public ClientMessageRepository(DbContext db) : base(db)
        {

        }
    }
}
