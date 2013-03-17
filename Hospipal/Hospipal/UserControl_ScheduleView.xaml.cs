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

       /* private ObservableCollection<Appointment> appointments;

        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                return this.appointments;
            }

            set
            {
                this.appointments = value;
            }
        }*/


        /*
        private Resource selectedResource;
        public Resource SelectedResource
        {
            get
            {
                return selectedResource;
            }

            set
            {
                if (selectedResource != value)
                {
                    selectedResource = value;
                    OnPropertyChanged(() => this.SelectedResource);
                }
            }

        }
        */

        public UserControl_ScheduleView()
        {
            InitializeComponent();
            this.Loaded += UserControl_ScheduleView_Loaded;

        }

        private void RadScheduleView_ShowDialog(object sender, ShowDialogEventArgs e)
        {
            if (e.DialogViewModel is AppointmentDialogViewModel)
                e.Cancel = true;

            if (e.DialogViewModel is ConfirmDialogViewModel)
            {
                e.DefaultDialogResult = true;
                e.Cancel = true;
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


            /*
            Appointment appt = new Appointment();
            appt.Subject = "New appointment";
            appt.Start = new DateTime(2013, 3, 15, 00, 00, 00);
            appt.End = new DateTime(2013, 3, 15, 05, 00, 00);
            
            appointments.Add(appt);
            Console.WriteLine(appt.Start.ToString());
             * 
            */

            appointments = Schedule.Select();

            scheduleView.AppointmentsSource = appointments;
            
        }
    }
}
