using Hospipal.Database_Class;
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
        private static readonly DependencyProperty PatientProperty =
                          DependencyProperty.Register("patient", typeof(Patient),
                                                      typeof(PatientInformation));
        private bool _isNewPatient = false;
        private Patient patient
   {
      get { return (Patient)GetValue(PatientProperty); }
      set { SetValue(PatientProperty, value); }
   }
        public PatientInformation()
        {
            InitializeComponent();
            //patient = new Patient(Convert.ToInt32(HealthCaretb.Text), FirstNametb.Text, LastNametb.Text, Convert.ToDateTime(DOBdp.SelectedDate), Addresstb.Text, Citytb.Text, Provincetb.Text, PostalCodetb.Text, Hometb.Text, Worktb.Text, Mobiletb.Text);
            patient = new Patient(12345);
        }
        public PatientInformation(int HealthCareNo)
        {
            _isNewPatient = false;
            patient = new Patient(HealthCareNo);
        }
        private void Save(object sender, MouseButtonEventArgs e)
        {
            this.Focus();
            if (_isNewPatient)
            {
                patient.Insert();
            }
            else
            {
                patient.Update();
            }
        }

        private void Cancel(object sender, MouseButtonEventArgs e)
        {
            Content = new UserControl_PatientsView();

        }
    }
}
