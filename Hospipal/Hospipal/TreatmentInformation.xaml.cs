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
    /// Interaction logic for UserControl_TreatmentView.xaml
    /// </summary>
    public partial class TreatmentInformation : UserControl
    {
        //Database treatment control object;
        private Database_Class.Treatment control;

        //Default patientID to 1
        int patientID = 1;
        //Set curTreatment number will link to text box
        int curTreatment;

        /*  Constructor that takes no arguments
         */ 
        public TreatmentInformation()
        {
            InitializeComponent();
          
            //try
            //{
            //    control = new Database_Class.Treatment(patientID);

            //    //Types of treatments available
            //    List<object[]> treatments = control.initializeTreatmentList();

            //    List<string> doctors = control.initializeDoctorList();
            //    List<string> treatmentHistory = control.initializeTreatmentHistory();

            //    string s = "";

            //    foreach (object[] element in treatments)
            //    {
            //        s = element[0].ToString();
            //        boxTreatmentType.Items.Add(s);

            //    }

            //    foreach (string element in doctors)
            //    {
            //        boxDoctors.Items.Add(element);

            //    }

            //    foreach (string element in treatmentHistory)
            //    {
            //        boxHistory.Items.Add(element);

            //    }

            //    lblName.Content = control.getPatientName();

            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Error with getting treatment");
            //}

            MessageBox.Show("No Patient Found Call Constructor with patientID");
            Content = new UserControl_MainTabView();
        }

        /*  Constructor that takes patientID 
        * 
        * 
        */
        public TreatmentInformation(int pID)
        {
            InitializeComponent();
            try
            {
                this.patientID = pID;
                //Instantiate control as treatment class in database class folder
                control = new Database_Class.Treatment(patientID);

                //Types of treatments available
                List<object[]> treatments = control.initializeTreatmentList();

                //List of doctors
                List<string> doctors = control.initializeDoctorList();
                //List of treatment history
                List<string> treatmentHistory = control.initializeTreatmentHistory();

                string s = "";

                //Converts each type of treatment to string and adds to text box
                foreach (object[] element in treatments)
                {
                    s = element[0].ToString();
                    boxTreatmentType.Items.Add(s);

                }

                //Adds each doctor to drop down box
                foreach (string element in doctors)
                {
                    boxDoctors.Items.Add(element);

                }

                //Adds each treatment to drop down box
                foreach (string element in treatmentHistory)
                {
                    boxHistory.Items.Add(element);

                }
    
                //Sets the name label to display patient name
                lblName.Content = control.getPatientName();

            }
            catch(Exception)
            {
                MessageBox.Show("Error with getting treatment, there might be an issue with patientID");
            }
        }

        /* Add button click event
         * 
         */ 
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            int executeCode = 1;
            getInputs(executeCode);
        }

        /* Modify button click event 
         * 
         */
        private void buttonModify_Click(object sender, RoutedEventArgs e)
        {
            int executeCode = 2;
            getInputs(executeCode);
        }

        /* Remove button click event
         * 
         */ 
        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            int executeCode = 3;
            getInputs(executeCode);
        }

        /* Cancel button click event
         * 
         */
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            //When user clicks on X the main tab view is displayed
            Content = new UserControl_PatientsView();
        }

        /* User selects a item in the list box
         * 
         */
        private void boxHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> details = new List<string>();

            try
            {
                //cur treatment is set to index of item selected
                curTreatment = boxHistory.SelectedIndex;

                //Get details of the treatment
                details = control.getTreatmentDetails(curTreatment);

                //Populate the details based on information received
                boxTreatmentType.SelectedValue = details.ElementAt(0);
                boxDoctors.SelectedValue = details.ElementAt(7);

                boxDate.Text = details.ElementAt(1) + "/" + details.ElementAt(2) + "/" + details.ElementAt(3);

                boxTime.SelectedValue = Convert.ToDateTime(details.ElementAt(4));
                txtNotes.Text = details.ElementAt(5);
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

        /* This method parses a string to a integer
         * and catched exceptions that might occur during conversion
         */
        private int parseInt(string i)
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

        /* This method gets all the fields in the text box and parses
         * executes from when a button is pressed
         * 
        */
        public void getInputs(int code)
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

                //List of doctors
                if (boxDoctors.SelectedIndex == -1)
                {
                    doc = "";
                }
                else
                {
                    input = boxDoctors.SelectedValue.ToString();
                    doc = input;
                }

                //Treatment date parsed to integer value of day month year
                input = boxDate.Text;
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
                    //Store string description of notes
                    treatmentNotes = txtNotes.Text;
                }

                //If no errors then code is executes
                if (err == 0)
                {
                    if (code == 1)
                    {
                        //Adds treatment
                        control.AddTreatment(TreatmentType, DateDay, DateMonth, DateYear, treatTime, treatmentNotes, doc);
                    }
                    else if (code == 2)
                    {
                        //Modify treatment
                        control.ModifyTreatment(curTreatment, TreatmentType, DateDay, DateMonth, DateYear, treatTime, treatmentNotes, doc);
                    }
                    else if (code == 3)
                    {
                        //Deletes treatment
                        if (curTreatment != -1)
                        {
                            control.RemoveTreatment(curTreatment);
                        }
                    }

                    //Refreshes the user control once query is added
                    Content = new TreatmentInformation(patientID);
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
    }
}
