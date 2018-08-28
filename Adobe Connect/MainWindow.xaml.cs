using System.Collections.Generic;
using System.Windows;
using Adobe_Connect.Models;
using Adobe_Connect.Services;
using Adobe_Connect.Collections;
using MahApps.Metro.Controls;

namespace Adobe_Connect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        
        private MeetingList meetings;

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

            List<Meeting> tempList = await AdobeConnectService.GetMeetings();

            meetings.Clear();

            foreach(Meeting meeting in tempList)
            {
                meetings.Add(meeting);
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Data.CollectionViewSource meetingViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("meetingViewSource")));
            
            meetings = new MeetingList();

            meetingViewSource.Source = meetings;
        }
    }
}
