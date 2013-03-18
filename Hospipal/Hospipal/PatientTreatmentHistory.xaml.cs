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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for PatientTreatmentHistory.xaml
    /// </summary>
    public partial class PatientTreatmentHistory : UserControl
    {

        private static readonly DependencyProperty TreatmentProperty =
                          DependencyProperty.Register("treatment", typeof(Treatment),
                                                      typeof(PatientTreatmentHistory));
        private Treatment treatment
        {
            get { return (Treatment)GetValue(TreatmentProperty); }
            set { SetValue(TreatmentProperty, value); }
        }

        private static readonly DependencyProperty WaitlistProperty =
                          DependencyProperty.Register("waitlist", typeof(WaitlistedPatient),
                                                      typeof(PatientTreatmentHistory));
        private WaitlistedPatient waitlist  
        {
            get { return (WaitlistedPatient)GetValue(WaitlistProperty); }
            set { SetValue(WaitlistProperty, value); }
        }

        List<Treatment> treatments;


        public PatientTreatmentHistory(int Pid)
        {
            InitializeComponent();
            Patient p = new Patient(Pid);
            lblPatientName.Content = p.LastName + ", " + p.FirstName;
            treatments = Treatment.GetTreatments(Pid,"History");
            dgHistory.DataContext = treatments;

        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Content = new PatientTreatmentView(treatment.PatientID);
        }

        private void dgHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            treatment = (Treatment)dgHistory.SelectedItem;
            waitlist = new WaitlistedPatient(treatment.TreatmentID);
            lblDetails.Content = treatment.Type + " " + treatment.DateToShortDateString + " " + treatment.Time;
        }
    }
}
