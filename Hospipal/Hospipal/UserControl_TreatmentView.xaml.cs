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
    public partial class UserControl_TreatmentView : UserControl
    {
        private Database_Class.Treatment control;
        int patientID = 1;

        public UserControl_TreatmentView()
        {
            InitializeComponent();
            control = new Database_Class.Treatment(patientID);


            List<object[]> treatments = control.initializeTreatmentList();
            //List<object[]> obj = Database.Select("SELECT * FROM ismacaul_HospiPal.Treatment");


            string s = "";
            foreach (object[] element in treatments)
            {
                s = element[0].ToString();
                boxTreatmentType.Items.Add(s);

            }

            List<string> treatmentHistory = control.initializeTreatmentHistory();
            foreach (string element in treatmentHistory)
            {
                boxHistory.Items.Add(element);

            }

            lblName.Content = control.getPatientName();

                //MessageBox.Show("Treatment type has " + obj.Count);
                //This is test code
                //string history;
                //string id;
                //string type;
                //id = "1";
                //type = "test";
                //history = (id + " " + type);
                ////Populate treatment type from database
                //boxHistory.Items.Add(history);
                //boxTreatmentType.Items.Add("Test");
        }
        public UserControl_TreatmentView(int pID)
        {
            InitializeComponent();
            patientID = pID;
            control = new Database_Class.Treatment(patientID);


            List<object[]> treatments = control.initializeTreatmentList();
            //List<object[]> obj = Database.Select("SELECT * FROM ismacaul_HospiPal.Treatment");


            string s = "";
            foreach (object[] element in treatments)
            {
                s = element[0].ToString();
                boxTreatmentType.Items.Add(s);

            }

            List<string> treatmentHistory = control.initializeTreatmentHistory();
            foreach (string element in treatmentHistory)
            {
                boxHistory.Items.Add(element);

            }

            lblName.Content = control.getPatientName();
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
            catch (FormatException e)
            {
                //Not Sequence of digits
                MessageBox.Show("Error one of the fields is not a valid integer");
            }
            catch (OverflowException e)
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


        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            //When user clicks on X the main tab view is displayed
            Content = new UserControl_MainTabView();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            //Variable declarations
            string input;
            string TreatmentType;
            int DateMonth;
            int DateDay;
            int DateYear;
            string treatTime;
            string treatmentNotes = "";
            int err = 0;                      //Error flag

            try
            {
                ////Treatment ID input from text box
                //input = txtTreatmentID.Text;
                //TreatmentID = parseInt(input);
                //Treatment ID input from text box
                //input = txtTreatmentID.Text;
                //TreatmentID = parseInt(input);

                //Treatment type from combo box that converts item to string
                input = boxTreatmentType.SelectedValue.ToString();
                TreatmentType = input;

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

                if (err == 0)
                {
                    control.AddTreatment(TreatmentType,  DateDay, DateMonth, DateYear, treatTime, treatmentNotes);
                    //Database.Insert(@"INSERT INTO ismacaul_HospiPal.RecievesTreatment (patient, treatment, day, month, year, time, notes) 
                    //VALUES (" + patientID + ", '" + TreatmentType + "', " + DateDay + ", " + DateMonth + ", " + DateYear + ", '" + treatTime + "', '" + treatmentNotes + "');");
                    //Refreshes the user control once query is added
                    Content = new UserControl_TreatmentView();
                }
                
            }
            //This error usually occurs when treatment type is left empty
            catch (NullReferenceException)
            {
                err++;
                MessageBox.Show("Select treatment Type");
            }
            //This error usually occurs when date is empty
            catch (ArgumentOutOfRangeException)
            {
                err++;
                MessageBox.Show("Please select a Date");
            }
            //This error usually occurs when time is empty
            catch (InvalidOperationException oe)
            {
                err++;
                MessageBox.Show("Select valid time");
            }
            //Content = new UserControl_TreatmentView();
            err = 0;
        }

        private void boxHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
