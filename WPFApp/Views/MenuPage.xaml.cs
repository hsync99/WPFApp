﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        MenuViewModel _menuViewModel = new MenuViewModel();
        public MenuPage()
        {
            InitializeComponent();
            DataContext = _menuViewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ListView view = (ListView)sender;
            var index = view.SelectedIndex;
            var selectedItem = view.SelectedItem;
            _menuViewModel.SelectItemByIndexInList(selectedItem);
        }
    }
}
