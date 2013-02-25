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
        private bool _isNewPatient = false;  //To use the same window for edits and adds.


        //Required for databinding -- BEGIN
        /* Also if you look at the Text property of the Textboxes, you can see that the binding is done there.
         * Also it references the name of the control in the binding
         */
        private static readonly DependencyProperty PatientProperty =
                          DependencyProperty.Register("patient", typeof(Patient),
                                                      typeof(PatientInformation));
        private Patient patient
        {
            get { return (Patient)GetValue(PatientProperty); }
            set { SetValue(PatientProperty, value); }
        }
        //Required for databinding -- END

        public PatientInformation()
        {
            InitializeComponent(); //New patient can use default constructor
        }

        public PatientInformation(int HealthCareNo)
        {
            _isNewPatient = false;  //A new patient will not have a health care no to reference
            patient = new Patient(HealthCareNo);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Content = new UserControl_PatientsView();  //This needs to be cleaned up so that rather than creating a new instance of a control
                                                       //it should find an old instance that called it.
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Savebtn.Focus();  //This is to lose focus on the last text field as data binding will not grab the last piece of data because textchanged is not fired off until focus is lost
            if (_isNewPatient)
            {
                patient.Insert();
            }
            else
            {
                patient.Update();
            }
        }
    }
}
