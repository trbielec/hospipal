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

using Telerik.Windows.Controls.ScheduleView;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for UserControl_AddSchedule.xaml
    /// </summary>
    public partial class UserControl_AddSchedule : Window
    {

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



        public UserControl_AddSchedule(UserControl_ScheduleView parentUC)
        {
            InitializeComponent();
            schedule = new Schedule();
            SaveButton.Click += SaveButton_Click;
            CancelButton.Click += CancelButton_Click;

            ParentScheduleWindow = parentUC as UserControl_ScheduleView; 
        }

        public UserControl_AddSchedule(Appointment appt, UserControl_ScheduleView parentUC)
        {
            InitializeComponent();
            schedule = new Schedule();
            
            _isSelected = true;



            ParentScheduleWindow = parentUC as UserControl_ScheduleView; 

        }



        void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.Focus();

            if (!_isSelected)
                schedule.Insert();
            else
                schedule.Update();
        }
    }
}
