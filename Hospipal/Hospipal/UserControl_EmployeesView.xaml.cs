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
        public UserControl_EmployeesView()
        {
            InitializeComponent();
            /*
            // construct the dataset
            ismacaul_HospiPalDataSet dataset = new ismacaul_HospiPalDataSet();

            // use a table adapter to populate the Customers table
            ismacaul_HospiPalDataSetTableAdapters.EmployeeTableAdapter adapter = new ismacaul_HospiPalDataSetTableAdapters.EmployeeTableAdapter();
            adapter.Fill(dataset.Employee);

            // use the Customer table as the DataContext for this Window
            this.DataContext = dataset.Employee.DefaultView;
             */

            List<Employee> Employees = Employee.GetEmployees();
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
            Content = new EmployeeInformation();
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
        }
    }
}
