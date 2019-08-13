using Microsoft.Win32;
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
using SwissU.ViewModels;
using SwissU.Configuration;

namespace SwissU.Views
{
    /// <summary>
    /// Interaction logic for UpdateCategoriesView.xaml
    /// </summary>
    public partial class UpdateCategoriesView : UserControl
    {
        //public static string id { get; set; }

        public UpdateCategoriesView()
        {
            InitializeComponent();
            
        }// EOC


        private void BtnExecute_Click(object sender, RoutedEventArgs e)
        {
            string attributeIDString = string.Format("Attr_{0} : {1}", txtSearchValue.Text, cbxAttributeValue.Text);
            updateCategoriesViewModel viewModel = new updateCategoriesViewModel();
            viewModel.ExecuteUpdate(LoginViewModel.ticket, attributeIDString, txtNewValue.Text, txtSearchValue.Text, Config.endpoint);

            

        }// EOM


        private void TxtSearchValue_LostFocus(object sender, RoutedEventArgs e)
        {
            updateCategoriesViewModel viewModel = new updateCategoriesViewModel();

            List<string> list = viewModel.GetAttributeValues(Int32.Parse(txtSearchValue.Text));

            foreach (var item in list)
            {
                cbxAttributeValue.Items.Add(item);
            }
        }
    }
}
