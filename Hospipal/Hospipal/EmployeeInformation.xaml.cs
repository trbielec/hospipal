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

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for NurseWindow.xaml
    /// </summary>
    public partial class EmployeeInformation : UserControl
    {

        private bool _isNewEmployee = false;

        public EmployeeInformation()
        {
            InitializeComponent();
        }

        public EmployeeInformation(bool isNewEmployee)
        {
            _isNewEmployee = isNewEmployee;
            InitializeComponent();
        }

        private void EmployeeUC_Loaded(object sender, RoutedEventArgs e)
        {

            if (_isNewEmployee)
            {
                // Populate title

                // Find smallest unused eid in db, populate eid in ui

            }
            else
            {
                // Populate title

                // Populate text fields with db info
            }
           
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
