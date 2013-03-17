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
        List<Treatment> treatmentHistory;
        Patient patient;
        #endregion  
        
        #region Constructors
        public PatientTreatmentView()
        {
            InitializeComponent();

        }

        public PatientTreatmentView(int pID)
        {
            InitializeComponent();
            patient = new Patient(pID);
            lblName.Content = patient.LastName + ", " + patient.FirstName;
            treatmentHistory = Treatment.GetTreatments(pID);
            dataGridTreatments.DataContext = treatmentHistory;
        }
        #endregion


        #region Event handlers
        /* Add button click event 
         */
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Content = new AddTreatment(patient.PatientID);
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
                treatmentHistory.RemoveAt(dataGridTreatments.SelectedIndex);
                dataGridTreatments.Items.Refresh();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Content = new UserControl_PatientsView();
        }
        #endregion

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            //Content = new AddTreatment(patient.PatientID);
        }

        /* Stop button click event 
         */
    }
}
