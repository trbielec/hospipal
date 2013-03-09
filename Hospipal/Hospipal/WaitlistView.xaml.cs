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
            string selectedWard = WardSelectionBox.SelectedValue.ToString();

            patients = WaitlistedPatient.GetWaitlistedPatientsForWard(selectedWard);
            PatientWaitlistDG.DataContext = patients;

            beds = WaitlistedPatient.GetOpenBedsForWard(selectedWard);
            OpenBedsDG.DataContext = beds;
            
        }

        private void AssignPatientToBed(object sender, RoutedEventArgs e)
        {

        }
    }
}
