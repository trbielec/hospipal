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
            Patients_DeleteButton.Click += new RoutedEventHandler(DeletePatient);
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

        /*private void PatientsListLostFocus(object sender, RoutedEventArgs e)
        {
            Patients_DeleteButton.Visibility = System.Windows.Visibility.Hidden;
            Patients_EditButton.Visibility = System.Windows.Visibility.Hidden;

        }

        private void PatientsListHaveFocus(object sender, RoutedEventArgs e)
        {
            Patients_DeleteButton.Visibility = System.Windows.Visibility.Visible;
            Patients_EditButton.Visibility = System.Windows.Visibility.Visible;
        }*/

    }
}
