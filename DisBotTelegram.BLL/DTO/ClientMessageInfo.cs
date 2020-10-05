using System;

namespace DisBotTelegram.BLL.DTO
{
    public class ClientMessageInfo
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string MessageClient { get; set; }
        public DateTime TimeMassage { get; set; }
    }
}
