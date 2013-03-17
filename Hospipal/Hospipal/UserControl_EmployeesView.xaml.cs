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
            
            cbEmployeeType.Items.Add(string.Empty);
            cbSpecialty.Items.Add(string.Empty);

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

        private void SearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbEID.Text) && string.IsNullOrWhiteSpace(tbFName.Text) && string.IsNullOrWhiteSpace(tbLName.Text) && string.IsNullOrWhiteSpace(tbSupervisorID.Text) && string.IsNullOrWhiteSpace(cbEmployeeType.Text) && string.IsNullOrWhiteSpace(cbSpecialty.Text))
            {
                Employees = Employee.GetEmployees();
                Employee_DataGrid.DataContext = Employees;
            }
            else
            {
                validateInputs();
            }
            clearBoxes();
        }

        private void clearBoxes()
        {
            tbEID.Clear();
            tbFName.Clear();
            tbLName.Clear();
            tbSupervisorID.Clear();
            cbEmployeeType.Text = string.Empty;
            cbSpecialty.Text = String.Empty;
        }


        private void Employee_DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Employee_DataGrid.SelectedItems.Count > 0)
                Content = new EmployeeInformation(((Employee)Employee_DataGrid.SelectedItem));
        }

        public void validateInputs()
        {
            Search advancedSearch = new Search("Employee");
            List<string> dbSideVariables = new List<string>();
            List<string> cSideVariables = new List<string>();

            #region Box Checking
            if (!string.IsNullOrWhiteSpace(tbEID.Text))
            {

                dbSideVariables.Add("eid");
                cSideVariables.Add(tbEID.Text);
            }

            if (!string.IsNullOrWhiteSpace(tbFName.Text))
            {
                dbSideVariables.Add("fname");
                cSideVariables.Add(tbFName.Text);
            }

            if (!string.IsNullOrWhiteSpace(tbLName.Text))
            {
                dbSideVariables.Add("lname");
                cSideVariables.Add(tbLName.Text);
            }

            if (!string.IsNullOrWhiteSpace(cbSpecialty.Text))
            {
                dbSideVariables.Add("specialty");
                cSideVariables.Add(cbSpecialty.Text);
            }

            if (!string.IsNullOrWhiteSpace(cbEmployeeType.Text))
            {
                dbSideVariables.Add("employee_type");
                cSideVariables.Add(cbEmployeeType.Text);
            }

            if (!string.IsNullOrWhiteSpace(tbSupervisorID.Text))
            {
                dbSideVariables.Add("supervisor_id");
                cSideVariables.Add(tbSupervisorID.Text);
            }
            #endregion

            advancedSearch.UseInputs(dbSideVariables, cSideVariables);

            Employees = Search.SearchEmployees(advancedSearch.GetBuiltQuery());
            Employee_DataGrid.DataContext = Employees;

           
        }
    }
}
