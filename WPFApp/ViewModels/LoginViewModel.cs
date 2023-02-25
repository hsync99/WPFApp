using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApp.Views;
using WPFApp.Models.UI;
using WPFApp.Models;
using System.Windows.Navigation;

namespace WPFApp.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        private string _userName;   
        private string _password;
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public MainWindow mw;
        private RelayCommand _loginCommand;
        private RelayCommand _registrationCommand;
        NavigationService _navigationSerice;
      
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                  (_loginCommand = new RelayCommand(obj =>
                  {
                      Login();
                  }));
            }
        }
        public RelayCommand RegistrationCommand
        {
            get
            {
                return _registrationCommand ??
                  (_registrationCommand = new RelayCommand(obj =>
                  {
                      Registration();
                  }));
            }
        }

        public async void Login()
        {
        var user = await   DataStore.Login(UserName, Password);
           
           MenuWindow menuWindow= new MenuWindow(user);
            menuWindow.ShowDialog();
       
            
        }
        public async void Registration()
        {
            User u = new User();
            u.Balance = 15000;
            u.Username= _userName;
            u.Password= _password;  
            u.ID = Guid.NewGuid().ToString();
            u.Role = 0;
            u.Email = "test@gmail.com";
            u.Fname = "Test";
            u.Lname= "Test";
        await    DataStore.Registration(u);
        }
        public LoginViewModel()
        {
            
        }
    }
}
