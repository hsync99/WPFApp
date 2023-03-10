using System;
using System.CodeDom;
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
        private string _userid;
        public string Userid { get => _userid; set => _userid = value; }    
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
