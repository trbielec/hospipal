using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Telerik.Windows.Controls;


namespace Hospipal
{
    /// <summary>
    /// Interaction logic for UserControl_MainTabView.xaml
    /// </summary>
    public partial class UserControl_MainTabView : UserControl
    {
        private DispatcherTimer _timer;
        Database_Class.Notification Notif = new Database_Class.Notification();
        
        private static DependencyProperty NotificationProperty =
                          DependencyProperty.Register("notification", typeof(string),
                                                      typeof(UserControl_MainTabView));


        public string notification
        {
            get { return (string)GetValue(NotificationProperty); }
            set { SetValue(NotificationProperty, value); }
        }

        public UserControl_MainTabView()
        {
            InitializeComponent();

            PatientsTab.Content = new UserControl_PatientsView();
            EmployeesTab.Content = new UserControl_EmployeesView();
            WardsTab.Content = new UserControl_Wards();
            WaitlsitTab.Content = new WaitlistView();
            SchedulingTab.Content = new UserControl_ScheduleView();

            //Add event handler for notification text box
            Notifications_Bar.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(Notifications_Bar_LostFocus);

            //Retrience Notification when the tab page loads
            Notif.RetrieveNotification();
            notification = Notif.Text;

            //Start a timer to retrieve notifications via polling
            _timer = new DispatcherTimer();
            StartTimer();

        }

        public void StartTimer()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(10000); //10 seconds
            _timer.Tick += new EventHandler(delegate(object s, EventArgs a)
            {
                Notif.RetrieveNotification();
                notification = Notif.Text;
            });
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        #region event handlers
        private void Notifications_Bar_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            StopTimer();
            //Ugliest workaround for a bug in the history of man?
            //A blank window is the only way I could find for the 'notification' text from the UI to be updated and pass into the following functions
            Window window = new Window();
                window.Visibility = Visibility.Hidden;
                window.Show();
                window.Height = 0;
                window.Width = 0;
                window.Top = 5000;
                window.Hide();
                window.Close();
            
            //Send the notification
            Notif.Text = notification;
            Notif.SendNotification();

            StartTimer();

            //Should it retrieve the notification right after sending? I decided not to
            //Notif.RetrieveNotification();
            //notification = Notif.Text;
        }
        #endregion
    }
}
