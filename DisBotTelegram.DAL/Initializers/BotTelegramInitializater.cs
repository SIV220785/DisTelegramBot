using System.Data.Entity;
using DisBotTelegram.DAL.Contexts;
using DisBotTelegram.DAL.Entities;

namespace DisBotTelegram.DAL.Initializers
{
    internal class BotTelegramInitializater : CreateDatabaseIfNotExists<BotTelegramContext>
    {
        protected override void Seed(BotTelegramContext db)
        {
            User admin = new User()
            {
                UserLogin = "Admin",
                Password = "123"
            };
            User dis1 = new User()
            {
                UserLogin = "dis1",
                Password = "123"

            };
            User dis2 = new User()
            {
                UserLogin = "dis2",
                Password = "123"
            };
            db.Users.Add(admin);
            db.Users.Add(dis1);
            db.Users.Add(dis2);
            db.SaveChanges();
        }
    }
}
