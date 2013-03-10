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
        PatientTreatments patientTreatment;
        List<SingleTreatment> treatmentHistory;
        int patientID = 0;
        #endregion  
        
        #region Constructors
        public PatientTreatmentView()
        {
            InitializeComponent();

        }

        public PatientTreatmentView(int pID)
        {
            InitializeComponent();
            patientID = pID;

            patientTreatment = new PatientTreatments(pID);
            displayPatientName(pID);
            populateHistory(pID);
        }
        #endregion

        #region Function call and methods
        public void displayPatientName(int pID)
        {
            lblName.Content = PatientTreatments.GetPatientName(pID);
        }

        public void populateHistory(int pID)
        {
            treatmentHistory = PatientTreatments.GetTreatmentHistory(pID);
            dataGridTreatments.DataContext = treatmentHistory;
        }

        #endregion


        #region Event handlers
        /* Add button click event 
         */
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Content = new AddTreatment(patientID);
        }

        /* Modify button click event 
         */
        private void buttonModify_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTreatments.SelectedItems.Count > 0)
            {
                Content = new AddTreatment(((SingleTreatment)dataGridTreatments.SelectedItem));
            }
        }

        /* Remove button click event
         */
        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTreatments.SelectedItems.Count > 0)
            {
                patientTreatment.RemoveTreatment(PatientTreatments.treatmentIDList[dataGridTreatments.SelectedIndex]);
                treatmentHistory.RemoveAt(dataGridTreatments.SelectedIndex);

                dataGridTreatments.Items.Refresh();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Content = new UserControl_PatientsView();
        }
        #endregion

        private void dataGridTreatments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonModify.Visibility = Visibility.Visible;
            buttonRemove.Visibility = Visibility.Visible;
        }
    }
}
