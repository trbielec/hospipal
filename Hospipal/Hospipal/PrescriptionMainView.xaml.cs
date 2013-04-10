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
        #region Attributes
        List<Prescription> prescripHist;
        List<Prescription> prescripUp;
        List<Prescription> prescripCurrent;

        Patient patient;
        
        //Needed for data bindings
        private static readonly DependencyProperty PrescriptionProperty =
                         DependencyProperty.Register("prescription", typeof(Prescription),
                                                     typeof(PrescriptionMainView));
        #endregion

        #region Constructor
        public PrescriptionMainView(int HCNO)
        {
            InitializeComponent();
            //grab current patient to display name
            patient = new Patient(HCNO);
            lblPName.Content = patient.LastName + ", " + patient.FirstName;

            //set to upcioming or history based on date
            Prescription.CheckDatesForStatusChanges();

            //populate the three Datagrid lists
            prescripHist = Prescription.GetPrescriptions(patient.PatientID, "History");
            prescripUp = Prescription.GetPrescriptions(patient.PatientID, "Upcoming");
            prescripCurrent = Prescription.GetPrescriptions(patient.PatientID, "Current");

            //set data to Datagrids
            dataGridPrescriptionHist.DataContext = prescripHist;
            dataGridPrescriptionUp.DataContext = prescripUp;
            dataGridPrescriptionCurrent.DataContext = prescripCurrent;
        }
        #endregion

        #region Getters/Setters
        private Prescription prescription
        {
            get { return (Prescription)GetValue(PrescriptionProperty); }
            set { SetValue(PrescriptionProperty, value); }
        }

        #endregion

        #region Event handlers
        /* Add button click event 
         */
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
           Content = new PrescriptionView(patient.HealthCareNo);
        }

     
        /* Remove button click event
         */
        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            
            if (dataGridPrescriptionUp.SelectedItems.Count > 0)
            {
                prescripUp.RemoveAt(dataGridPrescriptionUp.SelectedIndex);
                dataGridPrescriptionUp.Items.Refresh();
            }
             
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
           // Content = new PrescriptionView();
        }
        #endregion
    }
}
