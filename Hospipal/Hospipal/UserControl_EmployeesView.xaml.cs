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
    /// Interaction logic for UserControl_EmployeesView.xaml
    /// </summary>
    public partial class UserControl_EmployeesView : UserControl
    {
        public UserControl_EmployeesView()
        {
            InitializeComponent();
            /*
            // construct the dataset
            ismacaul_HospiPalDataSet dataset = new ismacaul_HospiPalDataSet();

            // use a table adapter to populate the Customers table
            ismacaul_HospiPalDataSetTableAdapters.EmployeeTableAdapter adapter = new ismacaul_HospiPalDataSetTableAdapters.EmployeeTableAdapter();
            adapter.Fill(dataset.Employee);

            // use the Customer table as the DataContext for this Window
            this.DataContext = dataset.Employee.DefaultView;
             */

        }
    }
}
