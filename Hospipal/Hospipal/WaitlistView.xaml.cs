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
    /// Interaction logic for WaitlistView.xaml
    /// </summary>
    public partial class WaitlistView : UserControl
    {
        private List<WaitlistedPatient> patients;
        private List<Ward> wards;
        private List<Bed> beds;

        public WaitlistView()
        {
            InitializeComponent();

            wards = Ward.GetWards();
            WardSelectionBox.ItemsSource = wards;
            WardSelectionBox.DisplayMemberPath = "WardName";
            WardSelectionBox.SelectedValuePath = "WardName";
        }

        private void WardSelectionBoxEvent(object sender, SelectionChangedEventArgs e)
        {
            if (WardSelectionBox.SelectedValue != null)
            {
                string selectedWard = WardSelectionBox.SelectedValue.ToString();

                patients = WaitlistedPatient.GetWaitlistedPatientsForWard(selectedWard);
                PatientWaitlistDG.DataContext = patients;

                beds = WaitlistedPatient.GetOpenBedsForWard(selectedWard);
                OpenBedsDG.DataContext = beds;
            }   
        }

        private void AssignPatientToBed(object sender, RoutedEventArgs e)
        {
            // Add Patient to bed here!!!
            if (PatientWaitlistDG.SelectedValue != null)
            {
                Bed bed;
                WaitlistedPatient patient = ((WaitlistedPatient)PatientWaitlistDG.SelectedValue);
                if (OpenBedsDG.SelectedValue != null)
                {
                    bed = ((Bed)OpenBedsDG.SelectedValue);
                }
                else
                {
                    if (beds.Count != 0)
                        bed = beds[0];
                    else
                    {
                        string messagetext = "There are no beds to assign '" + patient.Fname + " " + patient.Lname + "' too";
                        string cap = "Error: No Beds Available";
                        MessageBoxButton but = MessageBoxButton.OK;
                        MessageBoxImage img = MessageBoxImage.Error;
                        MessageBox.Show(messagetext, cap, but, img);
                        return;
                    }
                }

                string messageboxtext = "Assign '" + patient.Fname + " " + patient.Lname + "' to " + bed.bed + "?";
                string caption = "Assign Patient";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageboxtext, caption, buttons, icon);

                if (result == MessageBoxResult.Yes)
                {
                    patient.AssignPatientToBed(bed.bedNo);
                    patient.RemovedPatientFromWaitlist();
                    RefreshViewEvent(null, null);
                }
            }
            else
            {
                string messageboxtext = "Please select a patient to assign them to a bed.";
                string caption = "Error: No Patient Selected";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;

                MessageBox.Show(messageboxtext, caption, buttons, icon); 
            }
        }

        private void RefreshViewEvent(object sender, RoutedEventArgs e)
        {
            WardSelectionBoxEvent(null, null);
        }
    }
}
