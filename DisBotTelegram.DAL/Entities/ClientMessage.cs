﻿using DisBotTelegram.DAL.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisBotTelegram.DAL.Entities
{
    [Table("ClientMessages")]
    internal class ClientMessage : IBaseEntity, IAuditable
    {

        public int Id { get; set; }
        public string MessageClient { get; set; }
        public int UserId { get; set; }
        public string TelegramId { get; set; }
        public DateTime TimeMassage { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
