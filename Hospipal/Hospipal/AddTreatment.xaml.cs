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
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

using Hospipal.Database_Class;

namespace Hospipal
{
    /// <summary>
    /// Interaction logic for AddTreatment.xaml
    /// </summary>
    public partial class AddTreatment : UserControl
    {
        #region Attributes
        private bool _isNewTreatment = true;
        int patientID;
        #endregion  

        #region Data Binding
        //Required for databinding -- BEGIN
        private static readonly DependencyProperty TreatmentProperty =
                          DependencyProperty.Register("treatment", typeof(Treatment),
                                                      typeof(AddTreatment));
        private Treatment treatment
        {
            get { return (Treatment)GetValue(TreatmentProperty); }
            set { SetValue(TreatmentProperty, value); }
        }
        //Required for databinding -- END
        #endregion

        #region Constructors
        public AddTreatment(int pID)
        {
            InitializeComponent();
            this.patientID = pID;
      
        }

        public AddTreatment(Treatment pTreatment)
        {
            InitializeComponent();
            treatment = pTreatment;
            this.patientID = treatment.PatientID;

             _isNewTreatment  = false;

        }

        #endregion

        #region Functions and Methods
        public void setDateFormat()
        {
            //Sets a default date format to formate date because this will display differently on different computers and just standardizes it
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }

        public void populatePreBoxFields()
        {
            setDateFormat();

            List<string> type = Treatment.GetTreatmentTypes();
            List<Employee> allEmployees = Employee.GetEmployees();
            List<string> doctors = new List<string>();

            foreach (Employee employee in allEmployees)
            {
                if (employee.Employee_type == "Doctor")
                    doctors.Add(employee.Lname + ", " + employee.Fname);
            }


        }

       
        /* This method parses a string to a integer
        * and catched exceptions that might occur during conversion
        */
       
        #endregion  

        #region event handlers
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Content = new PatientTreatmentView(patientID);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

    }
}
