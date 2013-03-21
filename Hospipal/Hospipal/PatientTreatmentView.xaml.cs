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
    /// Interaction logic for PatientTreatmentView.xaml
    /// </summary>
    public partial class PatientTreatmentView : UserControl
    {
        #region Attributes 
        List<Treatment> upTreatments;

        Patient patient;
       
        #endregion  

        private static readonly DependencyProperty TreatmentProperty =
                         DependencyProperty.Register("treatment", typeof(Treatment),
                                                     typeof(PatientTreatmentView));
        #region Getters/Setters
        private Treatment treatment
        {
            get { return (Treatment)GetValue(TreatmentProperty); }
            set { SetValue(TreatmentProperty, value); }
        }

        #endregion

        #region Constructors
       

        public PatientTreatmentView(int HCNO)
        {
            InitializeComponent();
            patient = new Patient(HCNO);
            lblName.Content = patient.LastName + ", " + patient.FirstName;
            upTreatments = Treatment.GetTreatments(patient.PatientID, "Upcoming");
            List<Treatment> currentTreatment = Treatment.GetTreatments(patient.PatientID, "Current");
            if (currentTreatment.Capacity > 0)
            {
                treatment = currentTreatment[0];
                //WaitlistedPatient wPatient = new WaitlistedPatient(treatment.TreatmentID);

                currentWardtb.Text = Database.Select("SELECT Ward.ward_name FROM Ward WHERE ward_slug IN (Select Bed.ward FROM Bed WHERE Bed.pid =" + patient.PatientID + ")")[0].ElementAt(0).ToString();
                
            }
            
            
            dataGridTreatments.DataContext = upTreatments;
        }
        #endregion
        

        #region Event handlers
        /* Add button click event 
         */
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Content = new AddTreatment(patient.HealthCareNo);
        }

        /* Modify button click event 
         */
        private void buttonModify_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTreatments.SelectedItems.Count > 0)
            {
                Content = new AddTreatment(((Treatment)dataGridTreatments.SelectedItem));
            }
        }

        /* Remove button click event
         */
        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTreatments.SelectedItems.Count > 0)
            {
                upTreatments.RemoveAt(dataGridTreatments.SelectedIndex);
                dataGridTreatments.Items.Refresh();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Content = new UserControl_PatientsView();
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            //Remove patient from bed
            WaitlistedPatient.RemovePatientFromBed(int bedId, int rtID)
            //Convert treatment to history

            //Refresh screen with no treatment present


           
        }
        #endregion

    }
}
