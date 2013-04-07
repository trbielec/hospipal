using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Hospipal.Database_Class;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for PrescriptionMainView.xaml
    /// </summary>
    public partial class PrescriptionMainView : UserControl
    {
        List<Prescription> prescripHist;
        List<Prescription> prescripUp;
        List<Prescription> prescripCurrent;

        Patient patient;
        
        
        private static readonly DependencyProperty PrescriptionProperty =
                         DependencyProperty.Register("prescription", typeof(Prescription),
                                                     typeof(PrescriptionMainView));
        

        public PrescriptionMainView(int HCNO)
        {
            InitializeComponent();
            patient = new Patient(HCNO);
            lblPName.Content = patient.LastName + ", " + patient.FirstName;
            prescripHist = Prescription.GetPrescriptions(patient.PatientID, "History");
            prescripUp = Prescription.GetPrescriptions(patient.PatientID, "Upcoming");
            prescripCurrent = Prescription.GetPrescriptions(patient.PatientID, "Current");

            //Data binding settings
            dataGridPrescriptionHist.DataContext = prescripHist;
            dataGridPrescriptionUp.DataContext = prescripUp;
            dataGridPrescriptionCurrent.DataContext = prescripCurrent;
        }


        #region Event handlers
        /* Add button click event 
         */
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
           // Content = new Prescription(prescription.HealthCareNo);
        }

     
        /* Remove button click event
         */
        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (dataGridTreatments.SelectedItems.Count > 0)
            {
                upTreatments.RemoveAt(dataGridPrescriptionUp.SelectedIndex);
                dataGridTreatments.Items.Refresh();
            }
             */
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
           // Content = new PrescriptionView();
        }
        #endregion
    }
}
