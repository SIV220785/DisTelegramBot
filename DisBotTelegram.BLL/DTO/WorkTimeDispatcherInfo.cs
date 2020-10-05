using System;

namespace DisBotTelegram.BLL.DTO
{
    public class WorkTimeDispatcherInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; }
        public DateTime EnterDispatchar { get; set; }
        public DateTime OutDispatcher { get; set; }
    }
}
