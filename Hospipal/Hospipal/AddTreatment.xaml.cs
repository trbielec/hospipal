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
                          DependencyProperty.Register("treatment", typeof(SingleTreatment),
                                                      typeof(AddTreatment));
        private SingleTreatment treatment
        {
            get { return (SingleTreatment)GetValue(TreatmentProperty); }
            set { SetValue(TreatmentProperty, value); }
        }
        //Required for databinding -- END
        #endregion

        #region Constructors
        public AddTreatment(int pID)
        {
            InitializeComponent();
            this.patientID = pID;

            populatePreBoxFields();        
        }

        public AddTreatment(SingleTreatment pTreatment)
        {
            InitializeComponent();
            treatment = pTreatment;
            this.patientID = treatment.PatientNumber;

            populateModifyBoxFields();
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
            lblName.Content = PatientTreatments.GetPatientName(patientID);

            setDateFormat();

            List<string> type = PatientTreatments.InitializeTreatmentTypesList();
            List<string> doctors = PatientTreatments.InitializeDoctorsList();

            foreach (string element in type)
            {
                boxTreatmentType.Items.Add(element);

            }
            //Adds each doctor to drop down box
            foreach (string element in doctors)
            {
                boxDoctors.Items.Add(element);
            }

        }

        public void populateModifyBoxFields()
        {
            populatePreBoxFields();

            List<string> details = new List<string>();
            string o = "";

            try
            {
                boxTreatmentType.SelectedValue = treatment.TreatmentType;
                boxDoctors.SelectedValue = PatientTreatments.FindDoctorName(treatment.TreatingDoctor);

                o = treatment.Day + "/" + treatment.Month + "/" + treatment.Year;

                boxDate.Text = o;

                boxTime.SelectedValue = treatment.RealTime;
                txtNotes.Text = treatment.Notes;
            }
            catch (FormatException)
            {
                MessageBox.Show("Error retreiving values from database");
            }
            catch (Exception)
            {
                MessageBox.Show("Error retreiving values from database or displaying information");
            }

        }

        public void getInputs()
        {
            //Variable declarations
            string input;               //string that reads the fields
            string TreatmentType;
            int DateMonth;
            int DateDay;
            int DateYear;
            string treatTime;
            string doc;
            string treatmentNotes = "";
            int err = 0;                      //Error flag

            try
            {
                //Treatment type from combo box that converts item to string
                input = boxTreatmentType.SelectedValue.ToString();
                TreatmentType = input;


                input = boxDoctors.SelectedValue.ToString();
                doc = input;

                //Treatment date parsed to integer value of day month year

                input = boxDate.SelectedDate.Value.ToString("dd-MM-yyyy");
                DateDay = parseInt(input.Substring(0, 2));
                DateMonth = parseInt(input.Substring(3, 2));
                DateYear = parseInt(input.Substring(6, 4));

                //RadTimePicker control gets time and converts to string
                DateTime dt = boxTime.SelectedValue.Value;
                treatTime = dt.ToShortTimeString().ToString();

                //If notes is empty a messagebox is displayed
                if (txtNotes.Text == "")
                {
                    err++;
                    MessageBox.Show("Enter notes");
                }
                else
                {
                    input = Regex.Replace(txtNotes.Text, @"[^\w\s.,]", "");
                    treatmentNotes = input;
                }

                //If no errors then code is executes
                if (err == 0)
                {
                    if (_isNewTreatment)
                    {
                        //Adds treatment
                        SingleTreatment.AddTreatment(patientID, TreatmentType, DateDay, DateMonth, DateYear, treatTime, treatmentNotes, PatientTreatments.doctorsList[doc]);
                    }
                    else
                    {
                        //Modify treatment
                        SingleTreatment.ModifyTreatment(treatment.TreatmentID, TreatmentType, DateDay, DateMonth, DateYear, treatTime, treatmentNotes, PatientTreatments.doctorsList[doc]);
                    }

                    //Refreshes the user control once query is added
                    Content = new PatientTreatmentView(patientID);
                }

            }
            //This error usually occurs when treatment type is left empty
            catch (NullReferenceException)
            {
                err++;
                MessageBox.Show("One of the fields contains an invalid entry");
            }
            //This error usually occurs when date is empty
            catch (ArgumentOutOfRangeException)
            {
                err++;
                MessageBox.Show("One of the fields contains an invalid entry");
            }
            //This error usually occurs when time is empty
            catch (InvalidOperationException)
            {
                err++;
                MessageBox.Show("One of the fields contains an invalid entry");
            }
            //Content = new UserControl_TreatmentView();
            err = 0;

        }

        /* This method parses a string to a integer
        * and catched exceptions that might occur during conversion
        */
        public int parseInt(string i)
        {
            //Initialize variables
            int number = 0;

            try
            {
                //Set number to convert string i to int
                number = Convert.ToInt32(i);
            }
            catch (FormatException)
            {
                //Not Sequence of digits
                MessageBox.Show("Error one of the fields is not a valid integer");
            }
            catch (OverflowException)
            {
                //Cannot fit value out of range 
                MessageBox.Show("Error one of the fields is a integer value out of range");
            }
            finally
            {
                if (number < Int32.MaxValue)
                {
                    //Not reached max value 
                }
                else
                {
                    //Max Value reached
                    MessageBox.Show("Max integer value reached for a field");
                }
            }
            return number;
        }
        #endregion  

        #region event handlers
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Content = new PatientTreatmentView(patientID);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            getInputs();

        }
        #endregion

    }
}
