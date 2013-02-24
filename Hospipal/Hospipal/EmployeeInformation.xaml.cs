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
            if (_isNewEmployee)
            {
                Employee e = new Employee();

                // Populate eid
                generatedEID.Content = e.GetEid();
            }
            else
            {
                // Populate title

                // Populate text fields with db info
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

        private void Save(object sender, MouseButtonEventArgs e)
        {
            //TODO

            if (_isNewEmployee)
            {
                //Add a new employee based on entered data
            }
            else
            {
                //Update current employee
            }
        }

        private void Cancel(object sender, MouseButtonEventArgs e)
        {
            //TODO - on button click, return to parent screen? 
        }

    }
}
