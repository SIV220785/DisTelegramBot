using System;
using System.Windows;

namespace DisBotTelegram.PL.Desktop.ReleyCommand
{
    public class ShowDialogCommand : OpenWindowCommand
    {
        private Action _PreOpenDialogAction;
        private Action<bool?> _PostOpenDialogAction;
        public ShowDialogCommand(Action<bool?> postDialogAction)
        {
            if (postDialogAction == null)
                throw new ArgumentNullException("postDialogAction");

            _PostOpenDialogAction = postDialogAction;
        }
        public ShowDialogCommand(Action<bool?> postDialogAction, Action preDialogAction)
            : this(postDialogAction)
        {
            if (preDialogAction == null)
                throw new ArgumentNullException("preDialogAction");

            _PreOpenDialogAction = preDialogAction;
        }
        protected override void OpenWindow(Window wnd)
        {
            if (_PreOpenDialogAction != null)
                _PreOpenDialogAction();

            bool? result = wnd.ShowDialog();

            _PostOpenDialogAction(result);
        }
    }
}
