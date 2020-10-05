using DisBotTelegram.BLL.DTO;
using System.Windows;
using System.Windows.Controls;

namespace DisBotTelegram.PL.Desktop.Helper
{
    public class MEssageViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate InMessage { get; set; }
        public DataTemplate OutMessage { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DisBotMessage message = (DisBotMessage)item;

            switch (message.Type)
            {
                case DisBotMessage.MessageType.InMessage:
                    return InMessage;
                default:
                    return OutMessage;
            }
        }
    }
}
