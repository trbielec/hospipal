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
        Database_Class.Notification Notif = new Database_Class.Notification();
        private static readonly DependencyProperty NotificationProperty =
                          DependencyProperty.Register("notification", typeof(string),
                                                      typeof(UserControl_MainTabView));
        private string notification
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

            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10000); //10 seconds
            _timer.Tick += new EventHandler(delegate(object s, EventArgs a)
            {
                Notif.RetrieveNotification();
                notification = Notif.Text;
            });
            _timer.Start();

        }

        #region event handlers
        private void Notifications_Bar_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Notif.SendNotification(notification);
            //Should it retrieve the notification after sending?
            //Notif.RetrieveNotification();
        }
        #endregion
    }
}
