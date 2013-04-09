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
    /// Interaction logic for PrescriptionView.xaml
    /// </summary>
    public partial class PrescriptionView : UserControl
    {
        #region Attributes
        
        private string Caller = "New";
        Patient patient;
        List<string> doctors;
        List<int> employeeIDs;

        #endregion  
        
        #region Data Binding
        private static readonly DependencyProperty PrescriptionProperty =
                                DependencyProperty.Register("prescription", typeof(Prescription),
                                                                typeof(PrescriptionView));
        private Prescription prescription
        {
            get { return (Prescription)GetValue(PrescriptionProperty); }
            set { SetValue(PrescriptionProperty, value); }
        }

        #endregion 

        #region Constructors

        public PrescriptionView()
        {
            InitializeComponent();
        }
        public PrescriptionView(int healthCareNo) //New
        {
            InitializeComponent();
            this.patient = new Patient(healthCareNo);
            this.prescription = new Prescription();
            prescription.PatientID = patient.PatientID;
            populatePreBoxFields();
        }

        public PrescriptionView(Prescription pPrescription, string pCaller)  //History/Upcoming/Current  
        {
            InitializeComponent();
            prescription = pPrescription;
            patient.GetPatient(prescription.PatientID);
            Caller = pCaller;
            populatePreBoxFields();
        }
        #endregion

        #region Functions and Methods

        public void populatePreBoxFields()
        {
            doctors = new List<string>();
            employeeIDs = new List<int>();
            List<Employee> allEmployees = Employee.GetEmployees();
            
            foreach (Employee employee in allEmployees)
            {
                if (employee.Employee_type == "Doctor")
                {
                    doctors.Add(employee.Lname + ", " + employee.Fname);
                    employeeIDs.Add(employee.Eid);
                }
            }

            boxDoctors.DataContext = doctors;
            boxDoctors.SelectedIndex = employeeIDs.IndexOf(prescription.Doctor); 
            boxDoctors.Items.Refresh();
            lblGenerateName.Content = patient.LastName + ", " + patient.FirstName;
            
        }
        #endregion

        #region event handlers

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Content = new PrescriptionMainView(patient.HealthCareNo);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            buttonSave.Focus(); //This is to lose focus on the last text field as data binding will not grab the last piece of data because textchanged is not fired off until focus is lost
            if (boxDoctors.SelectedIndex > 0)
                prescription.Doctor = employeeIDs[boxDoctors.SelectedIndex];
            if (DateTime.Compare(dpStartDate.SelectedDate.Value,dpEndDate.SelectedDate.Value) > 0)
            {
                MessageBox.Show("The start date must occur before the end date");
            }
            else if ((Caller=="New" && prescription.Insert()))
            {
                Content = new PrescriptionMainView(patient.HealthCareNo);
            }
            else if (prescription.Update())
            {
                Content = new PrescriptionMainView(patient.HealthCareNo);
            }
            else
            {
                MessageBox.Show("Error Occured");
            }
        }
        
        
        #endregion

    }
}
