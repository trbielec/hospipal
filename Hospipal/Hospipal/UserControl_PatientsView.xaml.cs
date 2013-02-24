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
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for UserControl_PatientsView.xaml
    /// </summary>
    public partial class UserControl_PatientsView : UserControl
    {


        public UserControl_PatientsView()
        {
            InitializeComponent();
            /*
            // construct the dataset
            ismacaul_HospiPalDataSet dataset = new ismacaul_HospiPalDataSet();

            // use a table adapter to populate the Customers table
            ismacaul_HospiPalDataSetTableAdapters.PatientTableAdapter adapter = new ismacaul_HospiPalDataSetTableAdapters.PatientTableAdapter();
            adapter.Fill(dataset.Patient);

            // use the Customer table as the DataContext for this Window
            this.DataContext = dataset.Patient.DefaultView;
             */

            Patients_AddButton.Click += new RoutedEventHandler(AddPatient);
        }

        
        private void AddPatient(object sender, RoutedEventArgs e)
        {
        }
        
    }
}
