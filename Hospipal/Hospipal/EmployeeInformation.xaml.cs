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
            // Populate eid
            generatedEID.Content = Employee.GenerateNewEid();
        }

        public EmployeeInformation(int eid)
        {
            _isNewEmployee = false;  //A new employee will not have an employee id to reference
            employee = new Employee(eid);
        }
  

        void Cancel(object sender, RoutedEventArgs e)
        {
            Content = new UserControl_EmployeesView();  //This needs to be cleaned up so that rather than creating a new instance of a control
            //it should find an old instance that called it.
        }

        void Save(object sender, RoutedEventArgs e)
        {
            employeeInfo_SaveButton.Focus();  //This is to lose focus on the last text field as data binding will not grab the last piece of data because textchanged is not fired off until focus is lost
            if (_isNewEmployee)
            {
                employee.Insert();
            }
            else
            {
                employee.Update();
            }

            
            /*if (_isNewEmployee)
            {
                employee.SetEid((int)generatedEID.Content);
                employee.SetName(firstNameTb.Text, lastNameTb.Text);
                //employee.SetSpecialty(specialtyComboBox.Items[specialtyComboBox1.SelectedIndex].ToString());
                employee.Add(employee);
              
            }
            else
            {
                employee.Update(employee);
            }*/ 
        }

        public EmployeeInformation(bool isNewEmployee)
        {
            _isNewEmployee = isNewEmployee;
            InitializeComponent();

            /*if (_isNewEmployee)
            {
                Employee e = new Employee();

                // Populate eid
                this.generatedEID.Content = e.GetEid();
            }
            else
            {
                // Populate title

                // Populate text fields with db info
            }*/ 
        }

  

    }
}
