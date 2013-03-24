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
using System.Windows.Shapes;
using Hospipal.Database_Class;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for NurseWindow.xaml
    /// </summary>
    public partial class EmployeeInformation : UserControl
    {

        private bool _isNewEmployee = true;

        //Required for databinding -- BEGIN
        private static readonly DependencyProperty EmployeeProperty =
                          DependencyProperty.Register("employee", typeof(Employee),
                                                      typeof(EmployeeInformation));
        private Employee employee
        {
            get { return (Employee)GetValue(EmployeeProperty); }
            set { SetValue(EmployeeProperty, value); }
        }
        //Required for databinding -- END

        public EmployeeInformation()
        {
            InitializeComponent();
            employee = new Employee();
            generatedEID.Content = Employee.GenerateNextEid(); //Show the next EID when page loads
        }

        public EmployeeInformation(Employee Employee)
        {
            InitializeComponent();
            _isNewEmployee = false;  //A new employee will not have an employee id to reference
            employee = Employee;
        }
  

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Content = new UserControl_EmployeesView();  //This needs to be cleaned up so that rather than creating a new instance of a control
            //it should find an old instance that called it.
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            employeeInfo_SaveButton.Focus();  //This is to lose focus on the last text field as data binding will not grab the last piece of data because textchanged is not fired off until focus is lost
        
            if (string.IsNullOrEmpty(employeeCb.Text) || string.IsNullOrEmpty(specialtyCb.Text))
            {
                MessageBox.Show("No value is selected for the employee type or specialty type");
            }
            else if (string.IsNullOrEmpty(lastNameTb.Text) || string.IsNullOrEmpty(firstNameTb.Text))
            {
                MessageBox.Show("No value is entered for the first name or the last name");
            } 
            else
            {
                if (_isNewEmployee)
                    employee.Insert();
                else
                    employee.Update();

                Content = new UserControl_EmployeesView();
            }
            }

    }
}
