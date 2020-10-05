using DisBotTelegram.DAL.Entities;
using DisBotTelegram.DAL.UnitOfWork;
using System.Data.Entity;

namespace DisBotTelegram.DAL.Repositories
{
    internal class ClientRepository: GenericRepository<Client>
    {
        public ClientRepository(DbContext db) : base(db)
        {
        }
    }
}
