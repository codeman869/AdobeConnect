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

namespace Adobe_Connect
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public bool LoggedIn { get;

            private set;
        }

        public Login()
        {
            InitializeComponent();
            LoggedIn = false;
        }

        private async void LogIn_Click(object sender, RoutedEventArgs e)
        {
            

            string username = Username.Text;
            string password = Password.Password;

            LoginStatus.Content = "Logging in...";

            LoggedIn = await Services.AdobeConnectService.Login(username, password);

            if (LoggedIn)
            {
                LoginStatus.Content = "Logged In!";
            } else
            {
                LoginStatus.Content = "Error Logging in!";
            }

        }

        
    }
}
