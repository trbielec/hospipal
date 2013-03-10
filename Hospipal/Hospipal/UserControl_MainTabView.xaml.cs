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


namespace Hospipal
{
    /// <summary>
    /// Interaction logic for UserControl_MainTabView.xaml
    /// </summary>
    public partial class UserControl_MainTabView : UserControl
    {
        public UserControl_MainTabView()
        {
            InitializeComponent();

            PatientsTab.Content = new UserControl_PatientsView();
            EmployeesTab.Content = new UserControl_EmployeesView();
            WardsTab.Content = new UserControl_Wards();
            SchedulingTab.Content = new UserControl_ScheduleView();
        }
    }
}
