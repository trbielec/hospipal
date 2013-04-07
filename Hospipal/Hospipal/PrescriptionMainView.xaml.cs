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
    /// Interaction logic for PrescriptionMainView.xaml
    /// </summary>
    public partial class PrescriptionMainView : UserControl
    {
        /*
        private static readonly DependencyProperty PrescriptionProperty =
                         DependencyProperty.Register("treatment", typeof(Treatment),
                                                     typeof(PatientTreatmentView));
        */

        public PrescriptionMainView()
        {
            InitializeComponent();
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
