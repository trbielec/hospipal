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
using Hospipal.Database_Class;
using System.Collections.ObjectModel;

using Telerik.Windows.Controls.ScheduleView;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for UserControl_AddSchedule.xaml
    /// </summary>
    public partial class UserControl_AddSchedule : Window
    {

        private List<Ward> wards;
        private List<Employee> employee;

        private bool _isSelected = false;

        private UserControl_ScheduleView ParentScheduleWindow = null;
        
        private static readonly DependencyProperty ScheduleProperty =
                  DependencyProperty.Register("schedule", typeof(Schedule),
                                              typeof(UserControl_AddSchedule));
        private Schedule schedule
        {
            get { return (Schedule)GetValue(ScheduleProperty); }
            set { SetValue(ScheduleProperty, value); }
        }
        
        public UserControl_AddSchedule(Slot selectedSlot, UserControl_ScheduleView parentUC)
        {
            InitializeComponent();
            sidLabel.Content = Schedule.GenerateNextEid();
            schedule = new Schedule();
            
            Console.WriteLine(selectedSlot.ToString());

            startDateTimePicker.SelectedValue = selectedSlot.Start;
            endDateTimePicker.SelectedValue = selectedSlot.End;

            wards = Ward.GetWards();
            WardName.ItemsSource = wards;
            WardName.SelectedIndex = 0;

            employee = Employee.GetEmployees();
            foreach(Employee i in employee){
                EmpID.Items.Add(i.Fname);
            }
            EmpID.SelectedIndex = 0;

            SaveButton.Click += SaveButton_Click;
            CancelButton.Click += CancelButton_Click;

            ParentScheduleWindow = parentUC as UserControl_ScheduleView; 
        }

        public UserControl_AddSchedule(Appointment selectedAppt, UserControl_ScheduleView parentUC)
        {
            InitializeComponent();

            schedule = new Schedule();

            sidLabel.Content = selectedAppt.Url;

            startDateTimePicker.SelectedValue = selectedAppt.Start;
            endDateTimePicker.SelectedValue = selectedAppt.End;

            EmpID.Text = selectedAppt.Subject;

            wards = Ward.GetWards();
            WardName.ItemsSource = wards;

            foreach (Ward i in wards)
            {
                if ((selectedAppt.Location.ToString()).CompareTo(i.WardName.ToString()) == 0)
                {
                    WardName.SelectedItem = i;
                }
            }

            employee = Employee.GetEmployees();

            foreach (Employee i in employee)
            {
                EmpID.Items.Add(i.Fname);
            }

            foreach (Employee i in employee)
            {
                if (selectedAppt.Subject.CompareTo(i.Eid.ToString()) == 0)
                {
                    EmpID.SelectedItem = i.Fname;
                }
            }

            _isSelected = true;
            SaveButton.Click += SaveButton_Click;
            CancelButton.Click += CancelButton_Click;
            ParentScheduleWindow = parentUC as UserControl_ScheduleView; 
        }


        void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.Focus();

            if (!_isSelected)
            {
                schedule.Sid = Convert.ToInt32(sidLabel.Content);
                schedule.Start_time = startDateTimePicker.SelectedValue.Value;
                schedule.End_time = endDateTimePicker.SelectedValue.Value;
                schedule.Ward = WardName.SelectedItem.ToString();

                employee = Employee.GetEmployees();

                foreach (Employee i in employee)
                {
                    if (EmpID.SelectedItem.ToString().CompareTo(i.Fname.ToString()) == 0)
                    {
                        schedule.Employee = Convert.ToInt32(i.Eid);
                    }
                }

                if (schedule.CheckforConflicts())
                    schedule.Insert();
                else
                    MessageBox.Show("Proposed schedule conflicts with an existing schedule.");

                this.Close();
            }
            else
            {
                schedule.Sid = Convert.ToInt32(sidLabel.Content);
                schedule.Start_time = startDateTimePicker.SelectedValue.Value;
                schedule.End_time = endDateTimePicker.SelectedValue.Value;
                schedule.Ward = WardName.SelectedItem.ToString();

                employee = Employee.GetEmployees();

                foreach (Employee i in employee)
                {
                    if (EmpID.SelectedItem.ToString().CompareTo(i.Fname.ToString()) == 0)
                    {
                        schedule.Employee = Convert.ToInt32(i.Eid);
                    }
                }

                if (schedule.CheckforConflicts() )
                    schedule.Update();
                else
                    MessageBox.Show("Proposed schedule conflicts with an existing schedule.");

                this.Close();
            }

            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();

            appointments = Schedule.Select();

            ParentScheduleWindow.scheduleView.AppointmentsSource = appointments;
        }

    }
}
