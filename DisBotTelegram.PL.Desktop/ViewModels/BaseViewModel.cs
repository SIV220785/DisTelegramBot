using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DisBotTelegram.PL.Desktop.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, INotifyCollectionChanged 
    {

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void AddRule(Func<string> p1, Func<bool> p2, string v)
        {
            throw new NotImplementedException();
        }
    }
}
