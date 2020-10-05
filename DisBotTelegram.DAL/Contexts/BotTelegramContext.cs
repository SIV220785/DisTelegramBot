using DisBotTelegram.DAL.Entities;
using DisBotTelegram.DAL.Entities.Base;
using DisBotTelegram.DAL.Initializers;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace DisBotTelegram.DAL.Contexts
{
    internal class BotTelegramContext : DbContext
    {
        static BotTelegramContext()
        {
            Database.SetInitializer<BotTelegramContext>(new BotTelegramInitializater());
        }
        public BotTelegramContext() : base("BotTelegramConnectionString")
        {
        }

        public virtual void Save()
        {
            base.SaveChanges();
        }
        public override int SaveChanges()
        {
            TrackChanges();
            try
            {
                return base.SaveChanges();
            }

            catch (DbUpdateException dbu)
            {
                var exception = HandleDbUpdateException(dbu);
                throw exception;
            }
        }

        private Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, dbu);
        }

        #region Auditing
        public Func<DateTime> TimestampProvider { get; set; } = () => DateTime.UtcNow;
        public string UserProvider
        {
            get
            {
                if (!string.IsNullOrEmpty(WindowsIdentity.GetCurrent().Name))
                    return WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                return string.Empty;
            }
        }
        private void TrackChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is IAuditable)
                {
                    var auditable = entry.Entity as IAuditable;
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedBy = UserProvider;  //  
                        auditable.CreatedOn = TimestampProvider();
                        auditable.UpdatedOn = TimestampProvider();
                    }
                    else
                    {
                        auditable.UpdatedBy = UserProvider;
                        auditable.UpdatedOn = TimestampProvider();
                    }
                }
            }
        }
        #endregion

        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClientMessage> ClientMessages { get; set; }
        public DbSet<DispatherMessage> DispatherMessages { get; set; }
        public DbSet<WorkTimeDispatcher> WorkDispatcherTimes { get; set; }
    }
}
