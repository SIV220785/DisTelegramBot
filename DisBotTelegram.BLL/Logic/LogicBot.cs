using DisBotTelegram.BLL.DTO;
using System;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using DisBotTelegram.BLL.Logic.EventsArgs;

namespace DisBotTelegram.BLL.Logic
{
    public class LogicBot
    {
        private readonly TelegramBotClient _botclient;
        private DisBotMessage _messages;
        public event EventHandler<BotEventArgs> LogStart;

        public DisBotMessage Messages
        {
            get { return _messages ?? (_messages = new DisBotMessage()); }
            set { _messages = value; }
        }

        public LogicBot()
        {
            _botclient = new TelegramBotClient("744399662:AAFJafKh3iNO_h7upw4sfGN27p9YXbDeKbc");
            _botclient.OnMessage += OnMessage;
            My_checkinternet();
        }
      
        protected virtual void OnLogStart(DisBotMessage message)
        {
            LogStart?.Invoke(this, new BotEventArgs(message));
        }

        public bool My_checkinternet()
        {
            try
            {
                var me = _botclient.GetMeAsync().Result;
                return true;
            }
            catch
            {
                MessageBox.Show("No internet connection !!", "Notification");
                return false;
            }
        }
        public void ReciveMessage()
        {

            _botclient.StartReceiving(new UpdateType[] { UpdateType.Message });

        }
        public void StopReceiving()
        {
            _botclient.StopReceiving();
        }

        public async void Bot_Send_Message(string id, string message)
        {

            if (message != string.Empty && id != string.Empty)
            {
                await _botclient.SendTextMessageAsync(chatId: id, text: message);
            }
            if (id == string.Empty)
            {
                MessageBox.Show("No internet connection !!", "Notification");
            }
        }

        private async void OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message == null || e.Message.Type != MessageType.Text)
            {
                return;
            }

            Messages.Id = e.Message.Chat.Id.ToString();
            Messages.UserName = e.Message.Chat.Username;
            Messages.FirstName = e.Message.Chat.FirstName;
            Messages.LastName = e.Message.Chat.LastName;
            Messages.Date = DateTime.Now;
            Messages.Content = e.Message.Text;
            Messages.Type = DisBotMessage.MessageType.OutMessage;

            if (!String.IsNullOrEmpty(e.Message.Chat.Username))
            {
                Messages.FullName = e.Message.Chat.Username; 
            }
            else if (!String.IsNullOrEmpty(e.Message.Chat.FirstName) && !String.IsNullOrEmpty(e.Message.Chat.LastName))
            {
                Messages.FullName = e.Message.Chat.FirstName + " " + e.Message.Chat.LastName;
            }
            else if (!String.IsNullOrEmpty(e.Message.Chat.FirstName) && String.IsNullOrEmpty(e.Message.Chat.LastName))
            {
                Messages.FullName = e.Message.Chat.FirstName;
            }
            else if (String.IsNullOrEmpty(e.Message.Chat.FirstName) && !String.IsNullOrEmpty(e.Message.Chat.LastName))
            {
                Messages.FullName = e.Message.Chat.LastName;
            }


            OnLogStart(Messages);

            await _botclient.SendTextMessageAsync(chatId: e.Message.Chat, text: "Message accepted");
           
        }
    }
}
