using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

namespace Hospipal.Database_Class
{
    public class PatientTreatments
    {
        #region Attributes
        private int patientID;
        List<SingleTreatment> receiveTreatments = new List<SingleTreatment>();
        public static Dictionary<string, int> doctorsList;

        // Maps a index starting from 0 to a patients treatment because a patient can have multiple treatments
        public static Dictionary<int, int> treatmentIDList;
        #endregion

        #region Constructor
        public PatientTreatments(int pID)
        {
            this.patientID = pID;
        }

        #endregion      
  
        #region Functions and Logic

        /* This method gets a list of treatments a patient can receive from the database
         returns: a list of string of treatments
         */
        public static List<string> InitializeTreatmentTypesList()
        {
            List<object[]> databaseTreatments = Database.Select("SELECT * FROM ismacaul_HospiPal.Treatment");
            List<string> treatmentTypes = new List<string>();            

            try
            {
                foreach (object[] element in databaseTreatments)
                {
                    treatmentTypes.Add(element[0].ToString());

                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Trouble converting from database to string for treatment list");
            }

            return treatmentTypes;
        }
        
        /* Gets a list of doctors from database
         * returns: string of doctors
        */
        public static List<string> InitializeDoctorsList()
        {
            //Query to get a list of doctors based on a table from the database
            List<object[]> doctorsFromDatabase = Database.Select("SELECT eid, fname, lname FROM ismacaul_HospiPal.Employee WHERE employee_type = 'Doctor'");
            List<string> docList = new List<string>();
            
            string doctorName;
            //Doctor name and int pair
            doctorsList = new Dictionary<string, int>();

            //Converts each object to string
            foreach (object[] element in doctorsFromDatabase)
            {
                doctorName = element[1].ToString() + " " + element[2].ToString();
                //If name doesn't already exist in dictionary add's distinct and prevents duplication
                if (!doctorsList.ContainsKey(doctorName))
                {
                    docList.Add(doctorName);
                    doctorsList.Add(doctorName, Convert.ToInt32(element[0]));
                }
            }

            return docList;
        }

        /* This method gets a list of treatments a patient can receive from the database
        */
        public static string GetPatientName(int pID)
        {
            string name;
            List<object[]> patientInfo = Database.Select("SELECT fname, lname FROM ismacaul_HospiPal.Patient WHERE pid = " + pID);

            name = patientInfo[0][0].ToString() + " " + patientInfo[0][1].ToString();

            return name;
        }

        public static List<SingleTreatment> GetTreatmentHistory(int pID) 
        {
            List<object[]> treatmentsFromDatabase = Database.Select("Select * FROM ismacaul_HospiPal.ReceivesTreatment  WHERE patient = " + pID + " ORDER BY (rtid)");
            List<SingleTreatment> treatments = new List<SingleTreatment>();
            treatmentIDList = new Dictionary<int, int>();

            int i = 0;

            foreach (object[] element in treatmentsFromDatabase)
            {
                SingleTreatment treatmentForPatient = new SingleTreatment(pID, element[2].ToString(), Convert.ToInt32(element[3]), Convert.ToInt32(element[4]), Convert.ToInt32(element[5]), element[6].ToString(), element[7].ToString(), Convert.ToInt32(element[8]));
                treatmentForPatient.TreatmentID = Convert.ToInt32(element[0]);               
                treatments.Add(treatmentForPatient);

                if (!treatmentIDList.ContainsKey(i))
                {
                    treatmentIDList.Add(i, Convert.ToInt32(element[0]));
                }       
                i++;
            }
            return treatments;
        }

        /* This method finds a doctor name from the int value
        */
        public static string FindDoctorName(int doctorValue)
        {            
            string foundDoctor = "";
            
            // List of keys for doctors = string
            List<string> keyDoctors = new List<string>(doctorsList.Keys);
            
            // Look for a value from dictionary because its converted to a int and returns the key as a string
            // Basically finds the key of the key value pair
            foreach (string n in keyDoctors)
            {
                if (doctorsList.ContainsValue(doctorValue)) // Will match once
                {
                    foundDoctor = n;
                }               
            }
            
            return foundDoctor;
        }

        /* Removes treatment based on rtid
         */
        public bool RemoveTreatment(int rtid)
        {
            return Database.Delete("DELETE FROM `ismacaul_HospiPal`.`ReceivesTreatment` WHERE `rtid`=" + rtid);
        }

        #endregion
    } // Class
} // Namespace
