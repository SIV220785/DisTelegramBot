using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DisBotTelegram.PL.Desktop.Services.WindowFactory
{
    public interface IWindowFactory
    {
        #region  Methods

        Window CreateWindow(WindowCreationOptions options);

        #endregion
    }

    public class WindowCreationOptions
    {
        #region Properties

        public string Title { get; set; } = "";
        public string Tag { get; set; } = "";

        public WindowSize WindowSize { get; set; }
        public SizeToContent SizeToContent { get; set; }


        #endregion
    }

    public class WindowSize
    {
        #region Static Fields and Constants

        public static readonly WindowSize Default = new WindowSize(new Size(double.NaN, double.NaN))
        {
            MinSize = new Size(640, 360),
            MaxSize = new Size(864, 486)
        };

        #endregion

        #region Ctors

        public WindowSize()
        {
            Size = new Size(double.NaN, double.NaN);
            MinSize = new Size(0, 0);
            MaxSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
        }

        public WindowSize(Size size) : this()
        {
            Size = size;
        }

        #endregion

        #region Properties

        public bool IsFixedSize { get; set; }
        public Size MaxSize { get; set; }
        public Size MinSize { get; set; }
        public Size Size { get; set; }

        public SizeToContent SizeToContent { get; set; } = SizeToContent.Manual;

        #endregion
    }
}
