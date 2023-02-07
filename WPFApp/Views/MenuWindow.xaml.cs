using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFApp.ViewModels;
namespace WPFApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        MenuViewModel _menuViewModel = new MenuViewModel();   
        public MenuWindow()
        {
            
        }
        public MenuWindow(string userid)
        {
            InitializeComponent();
            _menuViewModel.userid = userid;
            DataContext = _menuViewModel;
            
            //TBuserid.Text = userid;
        }
    }
}
