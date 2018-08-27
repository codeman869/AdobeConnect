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

namespace Adobe_Connect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Models.Meeting> meetings;
        

        public MainWindow()
        {
            InitializeComponent();

        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            Login loginwindow = new Login();

            loginwindow.Show();
        }

        private async void GetMeetingsButton_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource meetingViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("meetingViewSource")));
            meetings = await Services.AdobeConnectService.GetMeetings();

            meetingViewSource.Source = meetings;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Data.CollectionViewSource meetingViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("meetingViewSource")));
            //System.Windows.Data.CollectionViewSource meetingViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("meetingViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            //meetingViewSource.Source = meetings;
        }
    }
}
