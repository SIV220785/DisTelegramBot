using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisBotTelegram.BLL.DTO
{
    public class DisBotMessage
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Telegramid { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public bool IsIncoming { get; set; }
        public MessageType Type { get; set; }

        public enum MessageType
        {
            InMessage = 0,
            OutMessage = 1
        }
    }
}
