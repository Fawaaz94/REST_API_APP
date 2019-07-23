using Microsoft.Win32;
using SwissU.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SwissU.Views
{
    /// <summary>
    /// Interaction logic for FileUploadView.xaml
    /// </summary>
    public partial class FileUploadView : UserControl
    {
        fileUploadViewModel viewModel = new fileUploadViewModel();
        List<string> resultsList = new List<string>();
       

        public FileUploadView()
        {
            InitializeComponent();
            // Hiding the result labels and Retry button before execution
            successPanel.Visibility = Visibility.Hidden;
            errorPanel.Visibility = Visibility.Hidden;
            btnRetry.Visibility = Visibility.Hidden;
        }// EOC


        // BULK FILE UPLOAD
        private void BtnUploadExcel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            
            if(openFile.ShowDialog() == true)
            {
                txtBulk.Text = openFile.FileName;
            }
        }//EOM


        private void Execute()
        {
            this.Dispatcher.Invoke( () => 
            {
                List<int> resultsCounter = viewModel.BulkUpload(txtBulk.Text, resultsList);


                foreach (var item in resultsList)
                {
                    txtResultsBox.Text += Environment.NewLine + item;
                }

                successPanel.Visibility = Visibility.Visible;
                errorPanel.Visibility = Visibility.Visible;
                btnRetry.Visibility = Visibility.Visible;

                lblSuccessCount.Text = resultsCounter.ElementAt(0).ToString();
                lblErrorCount.Text = resultsCounter.ElementAt(1).ToString();
            });
        }

        private async void test()
        {
            await Task.Run(() => Execute());
        }


        // BULK EXECUTE FILE UPLOAD
        private void BtnExecuteExcelUploadAsync_Click(object sender, RoutedEventArgs e)
        {
            txtResultsBox.Clear();

            Thread thread = new Thread(new ThreadStart(test));
            thread.Start();

        }// EOM


        // SINGLE FILE UPLOAD
        private void BtnUploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == true)
            {
                txtSingleFile.Text = openFile.FileName;
            }
        }// EOM


        // SINGLE FILE EXECUTE
        private void BtnExecuteSingleUpload_Click(object sender, RoutedEventArgs e)
        {
            txtResultsBox.Text = viewModel.SingleUpload(txtFileLocation.Text, txtSingleFile.Text);

            txtFileLocation.Text = string.Empty;
            txtSingleFile.Text = string.Empty;
        }

        private void BtnRetry_Click_1(object sender, RoutedEventArgs e)
        {
            txtResultsBox.Clear();
            Thread thread = new Thread(new ThreadStart(test));
            thread.Start();
        }
    }
}
