using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DisBotTelegram.PL.Desktop.Services.WindowFactory
{
    internal sealed class WindowFactory : IWindowFactory
    {
        public Window CreateWindow(WindowCreationOptions options)
        {
            return new Window()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = options.Title,
                Width = options.WindowSize.Size.Width,
                Height = options.WindowSize.Size.Height,
                Owner = Application.Current.MainWindow,
                Tag = options.Tag,
                SizeToContent = options.WindowSize.SizeToContent
            };
        }
    }
}
