using System;

namespace DisBotTelegram.BLL.DTO
{
    public class DispatcherMessageInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public string MessageDispather { get; set; }
        public DateTime TimeMassage { get; set; }
    }
}
