using DisBotTelegram.DAL.Entities.Base;
using System;

namespace DisBotTelegram.DAL.Entities
{
    internal class WorkTimeDispatcher : IBaseEntity, IAuditable
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public DateTime? EnterDispatchar { get; set; }
        public DateTime? OutDispatcher { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
