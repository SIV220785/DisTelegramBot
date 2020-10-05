using DisBotTelegram.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisBotTelegram.DAL.Entities
{
    [Table("Users")]
    internal class User: IBaseEntity, IAuditable
    {
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<DispatherMessage> DispatherMessages { get; set; }
        public ICollection<WorkTimeDispatcher> WorkTimesDispatcher { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
