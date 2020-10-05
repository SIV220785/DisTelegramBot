using DisBotTelegram.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisBotTelegram.DAL.Entities
{
    [Table("Clients")]
    internal class Client : IBaseEntity, IAuditable
    {
        public int Id { get; set; }
        public string TelegramId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<ClientMessage> ClientMessages { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
