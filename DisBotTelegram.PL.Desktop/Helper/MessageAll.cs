using System;

namespace DisBotTelegram.PL.Desktop.Helper
{
    public class MessageAll
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return $"{DateTime} [{UserName}] : {Message} ";
        }
    }
}
