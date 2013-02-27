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

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for UserControl_EmployeesView.xaml
    /// </summary>
    public partial class UserControl_EmployeesView : UserControl
    {
        List<Employee> Employees; 

        public UserControl_EmployeesView()
        {
            InitializeComponent();

            Employees = Employee.GetEmployees();
            Employee_DataGrid.DataContext = Employees;

            Employees_AddButton.Click += new RoutedEventHandler(AddEmployee);
            Employees_EditButton.Click += new RoutedEventHandler(EditEmployee);
            Employees_DeleteButton.Click += new RoutedEventHandler(DeleteEmployee);

        }


        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            Content = new EmployeeInformation();
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            if (Employee_DataGrid.SelectedItems.Count > 0)
                Content = new EmployeeInformation(((Employee)Employee_DataGrid.SelectedItem));
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            if (Employee_DataGrid.SelectedItems.Count > 0)
            {
                Employees[Employee_DataGrid.SelectedIndex].Delete();
                Employees.RemoveAt(Employee_DataGrid.SelectedIndex);

                Employee_DataGrid.Items.Refresh();
            }
            
        }
    }
}
