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
using Hospipal.Database_Class;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for UserControl_ScheduleView.xaml
    /// </summary>
    public partial class UserControl_ScheduleView : UserControl
    {

        private Schedule schedule = new Schedule();

        public UserControl_ScheduleView()
        {
            InitializeComponent();
            
            scheduleView.AllowDrop = false;

            this.Loaded += UserControl_ScheduleView_Loaded;

            Schedule_AddButton.Click += Schedule_AddButton_Click;
            Schedule_EditButton.Click += Schedule_EditButton_Click;
            Schedule_DeleteButton.Click += Schedule_DeleteButton_Click;
        }

        void Schedule_AddButton_Click(object sender, RoutedEventArgs e)
        {
            Slot selectedSlot = scheduleView.SelectedSlot;

            UserControl_AddSchedule addSchd = new UserControl_AddSchedule(selectedSlot, this);

            addSchd.ShowDialog();
        }

        void Schedule_EditButton_Click(object sender, RoutedEventArgs e)
        {
            IOccurrence selectedAppt = scheduleView.SelectedAppointment;

            UserControl_AddSchedule addSchd = new UserControl_AddSchedule(selectedAppt, this);

            addSchd.ShowDialog();
        }

        void Schedule_DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IOccurrence selectedAppt = scheduleView.SelectedAppointment;
            Appointment sel = selectedAppt as Appointment;
            Schedule sch = new Schedule();
            sch.Sid = Convert.ToInt32(sel.UniqueId);
            sch.Delete();
            // Refresh view
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            appointments = Schedule.Select();
            scheduleView.AppointmentsSource = appointments;
            
        }

        private void RadScheduleView_ShowDialog(object sender, ShowDialogEventArgs e)
        {
            if (e.DialogViewModel is AppointmentDialogViewModel)
            {
                e.Cancel = true;

                if (scheduleView.SelectedSlot != null)
                {
                    Slot selectedSlot = scheduleView.SelectedSlot;
                    UserControl_AddSchedule addSchd = new UserControl_AddSchedule(selectedSlot, this);
                    addSchd.ShowDialog();
                }
            }

            if (e.DialogViewModel is ConfirmDialogViewModel)
            {
                e.DefaultDialogResult = true;
                e.Cancel = true;

                IOccurrence selectedAppt = scheduleView.SelectedAppointment;
                Appointment sel = selectedAppt as Appointment;
                Schedule sch = new Schedule();
                sch.Sid = Convert.ToInt32(sel.UniqueId);
                sch.Delete();
                // Refresh view
                ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
                appointments = Schedule.Select();
                scheduleView.AppointmentsSource = appointments;
            }

            var dialogViewModel = e.DialogViewModel as RecurrenceChoiceDialogViewModel;
            if (dialogViewModel != null)
            {
                dialogViewModel.IsSeriesModeSelected = true;
            }
        }

        void UserControl_ScheduleView_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            
            appointments = Schedule.Select();

            scheduleView.AppointmentsSource = appointments;
        }
    }
}
