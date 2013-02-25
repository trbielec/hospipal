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

        public EmployeeInformation()
        {
            InitializeComponent();

            employeeInfo_SaveButton.Click += employeeInfo_SaveButton_Click;
            employeeInfo_CancelButton.Click += employeeInfo_CancelButton_Click;

            if (_isNewEmployee)
            {
                // Populate eid
                generatedEID.Content = Employee.GenerateNewEid();
            }
            else
            {
                // Populate title

                // Populate text fields with db info
            }

        }

        void employeeInfo_CancelButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void employeeInfo_SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            if (_isNewEmployee)
            {
              employee.SetEid((int)generatedEID.Content);
              employee.SetName(firstNameTb.Text, lastNameTb.Text);
              //employee.SetSpecialty(specialtyComboBox.Items[specialtyComboBox.SelectedIndex].ToString());
            }
            else
            {
                employee.UpdateEmployee(employee);
            }
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
