using SwissU.Configuration;
using SwissU.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;


namespace SwissU.Views
{
    public partial class Login : Window
    {
        MainWindow mainWindow;


        public Login()
        {
            InitializeComponent();
        }// EOC


        private void  BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // Calls the static Authentication method and passes the username and password textboxes 
                // The endpoint comes from the Config class that pulls the data from a text file on your local computer
                LoginViewModel.Authentication("fdassie", "Data2019!", Config.endpoint);
                //LoginViewModel.Authentication(txtUsername.Text, txtPassword.Password, Config.endpoint);

                // Once logged in it calls the MainWindow window 
                // And passes the ticket generated from the Authentication method
                mainWindow = new MainWindow(LoginViewModel.ticket);

                if (LoginViewModel.statusCode == "OK")
                {
                    // Opens the MainWindow
                    mainWindow.Show();

                    // Closes the Login Screen
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                // If log in details are incorrect this error message will display
                lblAuthError.Visibility = Visibility.Visible;
            }
            
        }// EOM


    }// EOCLASS
}
