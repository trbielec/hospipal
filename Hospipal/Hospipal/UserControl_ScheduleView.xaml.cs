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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using System.Collections.ObjectModel;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for UserControl_ScheduleView.xaml
    /// </summary>
    public partial class UserControl_ScheduleView : UserControl
    {
        public UserControl_ScheduleView()
        {
            InitializeComponent();

            this.Loaded += UserControl_ScheduleView_Loaded;
        }

        void UserControl_ScheduleView_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableAppointmentCollection appointments = new ObservableAppointmentCollection();

            /*appointments.Add(new Appointment()
            {
                Subject = "New appointment",
                Start = new DateTime(2013, 3, 10, 12, 30, 00),
                End = new DateTime(2013, 3, 10, 13, 00, 00),
            });*/ 

            // Get appointment data from database


            scheduleView.AppointmentsSource = appointments;

        }
    }
}
