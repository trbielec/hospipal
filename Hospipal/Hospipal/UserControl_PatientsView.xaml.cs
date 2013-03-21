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
using System.Timers;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using Hospipal.Database_Class;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for UserControl_PatientsView.xaml
    /// </summary>
    public partial class UserControl_PatientsView : UserControl
    {

        List<Patient> Patients; 

        public UserControl_PatientsView()
        {
            InitializeComponent();
            Patients = Patient.GetPatients();

            Patients_DataGrid.DataContext = Patients;
           

            Patients_AddButton.Click += new RoutedEventHandler(AddPatient);
            Patients_EditButton.Click += new RoutedEventHandler(EditPatient);
            Patients_TreatmentHistoryButton.Click += new RoutedEventHandler(TreatmentHistory);
        }

        private void TreatmentHistory(object sender, RoutedEventArgs e)
        {
            Content = new PatientTreatmentHistory((((Patient)Patients_DataGrid.SelectedItem).HealthCareNo));
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            Content = new PatientInformation();
        }

        private void EditPatient(object sender, RoutedEventArgs e)
        {
            if (Patients_DataGrid.SelectedItems.Count > 0)
                Content = new PatientInformation(((Patient)Patients_DataGrid.SelectedItem));
            else
                MessageBox.Show("Please select a patient from the list.");
        }

        private void DeletePatient(object sender, RoutedEventArgs e)
        {
            if (Patients_DataGrid.SelectedItems.Count > 0)
            {
                Patients[Patients_DataGrid.SelectedIndex].Delete();
                Patients.RemoveAt(Patients_DataGrid.SelectedIndex);
                
                Patients_DataGrid.Items.Refresh();
            }
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbHealthCare.Text) && string.IsNullOrWhiteSpace(tbFName.Text) && string.IsNullOrWhiteSpace(tbLName.Text) && (dpDOB.Text == "") && string.IsNullOrWhiteSpace(tbAddress.Text) && string.IsNullOrWhiteSpace(tbCity.Text) && string.IsNullOrWhiteSpace(tbProvince.Text) && string.IsNullOrWhiteSpace(tbPostalCode.Text) && string.IsNullOrWhiteSpace(tbPhoneNumber.Text))
            {
                Patients = Patient.GetPatients();
                Patients_DataGrid.DataContext = Patients;
            }
            else
            {
                validateInputs();
            }

        }

        private void clearBoxes()
        {
            tbHealthCare.Clear();
            tbAddress.Clear();
            tbCity.Clear();
            tbFName.Clear();
            tbLName.Clear();
            tbPhoneNumber.Clear();
            tbPostalCode.Clear();
            tbProvince.Clear();
            dpDOB.Text = string.Empty;
        }

        public void validateInputs()
        {
            Search advancedSearch = new Search("Patient");
            List<string> dbSideVariables = new List<string>();
            List<string> cSideVariables = new List<string>();

            #region Box Checking
            if (!string.IsNullOrWhiteSpace(tbHealthCare.Text))
            {
                cSideVariables.Add(tbHealthCare.Text);
                dbSideVariables.Add("hc_no");
            }
            if (!string.IsNullOrWhiteSpace(tbFName.Text))
            {
                cSideVariables.Add(tbFName.Text);
                dbSideVariables.Add("fname");
            }

            if (!string.IsNullOrWhiteSpace(tbLName.Text))
            {
                cSideVariables.Add(tbLName.Text);
                dbSideVariables.Add("lname");
            }

            if (!(dpDOB.SelectedDate == null))
            {
                cSideVariables.Add(dpDOB.SelectedDate.Value.Day.ToString());
                cSideVariables.Add(dpDOB.SelectedDate.Value.Month.ToString());
                cSideVariables.Add(dpDOB.SelectedDate.Value.Year.ToString());

                dbSideVariables.Add("dob_day");
                dbSideVariables.Add("dob_month");
                dbSideVariables.Add("dob_year");
            }

            if (!string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                cSideVariables.Add(tbAddress.Text);
                dbSideVariables.Add("street_address");
            }

            if (!string.IsNullOrWhiteSpace(tbCity.Text))
            {
                cSideVariables.Add(tbCity.Text);
                dbSideVariables.Add("city");
            }
            if (!string.IsNullOrWhiteSpace(tbProvince.Text))
            {
                cSideVariables.Add(tbProvince.Text);
                dbSideVariables.Add("province");
            }

            if (!string.IsNullOrWhiteSpace(tbPostalCode.Text))
            {
                cSideVariables.Add(tbPostalCode.Text);
                dbSideVariables.Add("postal_code");
            }

            if (!string.IsNullOrWhiteSpace(tbPhoneNumber.Text))
            {
                cSideVariables.Add(tbPhoneNumber.Text);
                dbSideVariables.Add("home_phone_no");
            }
            #endregion

            advancedSearch.UseInputs(dbSideVariables, cSideVariables);
 
            Patients = Search.SearchPatient(advancedSearch.GetBuiltQuery());
            Patients_DataGrid.DataContext = Patients;
        }

        private void Patients_DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Patients_DataGrid.SelectedItems.Count > 0)
                Content = new PatientInformation(((Patient)Patients_DataGrid.SelectedItem));
        }

    }
}
