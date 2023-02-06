using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Services;
namespace WPFApp.ViewModels
{
    public  class BaseViewModel: INotifyPropertyChanged
    {
        public DataStore DataStore = new DataStore();
        public BaseViewModel()
        {

        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
