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

        List<string> doctors;
        List<string> type;
        List<Ward> wards;
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

        private static readonly DependencyProperty WaitlistProperty =
                          DependencyProperty.Register("waitlist", typeof(WaitlistedPatient),
                                                      typeof(AddTreatment));
        private WaitlistedPatient waitlist
        {
            get { return (WaitlistedPatient)GetValue(WaitlistProperty); }
            set { SetValue(WaitlistProperty, value); }
        }
        //Required for databinding -- END
        #endregion

        #region Constructors
        public AddTreatment(int pID)
        {
            InitializeComponent();
            this.patientID = pID;
            treatment = new Treatment();
            waitlist = new WaitlistedPatient();
            treatment.Status = "Upcoming";
            waitlist.Pid = patientID;
            populatePreBoxFields();
        }

        public AddTreatment(Treatment pTreatment)
        {
            InitializeComponent();
            treatment = pTreatment;
            waitlist = new WaitlistedPatient(treatment.TreatmentID);
            this.patientID = treatment.PatientID;
            populatePreBoxFields();
             _isNewTreatment  = false;

        }

        #endregion

        #region Functions and Methods

        public void populatePreBoxFields()
        {
            type = Treatment.GetTreatmentTypes();
            List<Employee> allEmployees = Employee.GetEmployees();
            wards = Ward.GetWards();
            doctors = new List<string>();

            foreach (Employee employee in allEmployees)
            {
                if (employee.Employee_type == "Doctor")
                    doctors.Add(employee.Lname + ", " + employee.Fname);
            }

            boxDoctors.DataContext = doctors;
            boxTreatmentType.DataContext = type;
            boxWard.DataContext = wards;
            boxDoctors.Items.Refresh();
        }
       
        #endregion  

        #region event handlers
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Content = new PatientTreatmentView(patientID);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            buttonSave.Focus(); //This is to lose focus on the last text field as data binding will not grab the last piece of data because textchanged is not fired off until focus is lost
            if ((_isNewTreatment && treatment.Insert()) || treatment.Update())
            {
                //need to give waitlist treatment ID which I do not have.
                Content = new UserControl_PatientsView();
            }
            else
            {
                MessageBox.Show("The HealthCare No entered is already in use.");
            }
        }
        #endregion

    }
}
