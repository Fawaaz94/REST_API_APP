using SwissU.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SwissU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string _ticket { get; set; }
        
        public MainWindow(string ticket)
        {
            InitializeComponent();
            _ticket = ticket;
            DataContext = new fileUploadViewModel(_ticket);
        }

        private void BtnFileUpload_Click_1(object sender, RoutedEventArgs e)
        {
            DataContext = new fileUploadViewModel(_ticket);
        }

        private void BtnCategoriesUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new updateCategoriesViewModel();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new SettingsViewModel();
        }

      
    }
}
