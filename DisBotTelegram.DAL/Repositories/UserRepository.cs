using DisBotTelegram.DAL.Entities;
using DisBotTelegram.DAL.UnitOfWork;
using System.Data.Entity;

namespace DisBotTelegram.DAL.Repositories
{
    internal class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext db):base(db)
        {
        }
    }
}
