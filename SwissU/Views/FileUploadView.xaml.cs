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
                txtResultsBox.Text = String.Empty;
                lblSuccessCount.Text = String.Empty;
                lblErrorCount.Text = String.Empty;

                List<int> resultsCounter = viewModel.BulkUpload(txtBulk.Text, resultsList);

                foreach (var item in resultsList)
                {
                    txtResultsBox.Text += Environment.NewLine + item;
                }

                successPanel.Visibility = Visibility.Visible;
                errorPanel.Visibility = Visibility.Visible;
                

                lblSuccessCount.Text = resultsCounter.ElementAt(0).ToString();
                lblErrorCount.Text = resultsCounter.ElementAt(1).ToString();
            });
        }

        private async void test()
        {
            await Task.Run(() => Execute());
        }


        private void BtnExecuteExcelUploadAsync_Click(object sender, RoutedEventArgs e)
        // BULK EXECUTE FILE UPLOAD
        {
            txtResultsBox.Clear();

            Thread thread = new Thread(new ThreadStart(test));
            thread.Start();

        }// EOM


        #region Single File OPS
        // SINGLE FILE UPLOAD
        private void BtnUploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == true)
            {
                txtSingleFileUpload.Text = openFile.FileName;
            }
        }// EOM


        // SINGLE FILE EXECUTE
        private void BtnExecuteSingleUpload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(viewModel.SingleUpload(txtSingleFileLocation.Text, txtSingleFileUpload.Text) == "OK")
                {
                    txtResultsBox.Text = "Completed";
                }
                else
                {
                    txtResultsBox.Text = "There was a error.";
                }
            }
            catch (Exception ex)
            {
                txtResultsBox.Text = $"{ex.Message}";
            }

            // Clears the textBox
            txtSingleFileLocation.Text = string.Empty;
            txtSingleFileUpload.Text = string.Empty;

        }// EOM

        #endregion

        //Upload Bulk WS EXCEL
        private void BtnUploadWSExcel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == true)
            {
                txtBulkWSExcel.Text = openFile.FileName;
            }
        }// EOM


        // Execute Bulk WS Excel

    }
}
