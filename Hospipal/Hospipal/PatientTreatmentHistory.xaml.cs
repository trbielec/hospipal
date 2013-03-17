using Hospipal.Database_Class;
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
    /// Interaction logic for PatientTreatmentHistory.xaml
    /// </summary>
    public partial class PatientTreatmentHistory : UserControl
    {

        private static readonly DependencyProperty TreatmentProperty =
                          DependencyProperty.Register("treatment", typeof(Patient),
                                                      typeof(PatientInformation));
        private Treatment treatment
        {
            get { return (Treatment)GetValue(TreatmentProperty); }
            set { SetValue(TreatmentProperty, value); }
        }

        List<Treatment> treatments;
        public PatientTreatmentHistory(int Pid)
        {
            InitializeComponent();
            treatments = Treatment.GetTreatments(Pid,"History");

        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Content = new PatientTreatmentView();
        }

        private void dgHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            treatment = (Treatment)dgHistory.SelectedItem;
        }
    }
}
