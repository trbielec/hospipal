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
        Patient patient;

        List<string> doctors;
        List<string> type;
        List<Ward> wards;
        List<int> employeeIDs;
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
        public AddTreatment(int healthCareNo)
        {
            InitializeComponent();
            this.patient = new Patient(healthCareNo);
            treatment = new Treatment();
            waitlist = new WaitlistedPatient();
            treatment.Status = "Upcoming";
            treatment.PatientID = patient.PatientID;
            waitlist.Pid = patient.PatientID;
            populatePreBoxFields();
            boxDate.Text = System.DateTime.Today.ToString();
        }

        public AddTreatment(Treatment pTreatment)
        {
            InitializeComponent();
            treatment = pTreatment;
            waitlist = new WaitlistedPatient(treatment.TreatmentID);
            patient = new Patient();
            patient.GetPatient(treatment.PatientID);
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
            employeeIDs = new List<int>();

            foreach (Employee employee in allEmployees)
            {
                if (employee.Employee_type == "Doctor")
                {
                    doctors.Add(employee.Lname + ", " + employee.Fname);
                    employeeIDs.Add(employee.Eid);
                }
            }

            boxDoctors.DataContext = doctors;
            boxDoctors.SelectedIndex = employeeIDs.IndexOf(treatment.Doctor);
            boxTreatmentType.DataContext = type;
            boxWard.DataContext = wards;
            boxWard.DisplayMemberPath = "WardName";
            boxWard.SelectedValuePath = "SlugName";
            boxDoctors.Items.Refresh();
            boxTreatmentType.Items.Refresh();
            patient.Select();
            lblName.Content = patient.LastName + ", " + patient.FirstName;
        }
       
        #endregion  

        #region event handlers
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Content = new PatientTreatmentView(patient.HealthCareNo);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            buttonSave.Focus(); //This is to lose focus on the last text field as data binding will not grab the last piece of data because textchanged is not fired off until focus is lost
            if(boxDoctors.SelectedIndex > 0)
            treatment.Doctor = employeeIDs[boxDoctors.SelectedIndex];
            if (boxPriority.SelectedIndex < 0 || boxTreatmentType.SelectedIndex < 0 || boxWard.SelectedIndex < 0)
            {
                MessageBox.Show("Treatment Type, Ward and Priority are required fields.");
            }
            else if ((_isNewTreatment && treatment.Insert()))
            {
                WaitlistedPatient.AddPatientToWaitlist(treatment.PatientID, waitlist.Ward, waitlist.Priority, Treatment.GenerateNextrtid() - 1);
                Content = new PatientTreatmentView(patient.HealthCareNo);
            }
            else if (treatment.Update())
            {
                WaitlistedPatient.EditPatientInWaitlist(treatment.PatientID, waitlist.Ward, waitlist.Priority, treatment.TreatmentID);
                Content = new PatientTreatmentView(patient.HealthCareNo);
            }
            else
            {
                MessageBox.Show("Error Occured");
            }
        }
        #endregion

    }
}
