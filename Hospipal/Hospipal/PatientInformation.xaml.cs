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
    /// Interaction logic for PatientInformation.xaml
    /// </summary>
    public partial class PatientInformation : UserControl
    {
        private bool _isNewPatient = false;

        public PatientInformation()
        {
            InitializeComponent();
        }
        public PatientInformation(bool isNewPatient)
        {
            _isNewPatient = isNewPatient;
            InitializeComponent();
        }

        private void Save(object sender, MouseButtonEventArgs e)
        {
            if (_isNewPatient)
            {
                //Add a new Patient based off of textFields
            }
            else
            {
                //Update Patient created
            }
        }

        private void Cancel(object sender, MouseButtonEventArgs e)
        {
            //Hide the UI
        }
    }
}
