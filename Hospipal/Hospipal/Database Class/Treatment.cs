using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    public class Treatment
    {
        //Treatments are linked to a patientsID
        int patientID;

        //Dictionary for a list of doctors that performs treatments
        Dictionary<string, int> doctorsList;
        //Maps a index starting from 0 to a patients treatment because a patient can have multiple treatments
        Dictionary<int, int> treatmentIDList;
        
        /* Constructor thats sets patientID
         */
        public Treatment(int pID)
        {
            //patientID is set to pID
            this.patientID = pID;
        }

        /* This method gets a list of treatments a patient can receive from the database
         returns: a list of object of treatments
         */
        public List<object[]> initializeTreatmentList()
        {
            List<object[]> obj = Database.Select("SELECT * FROM ismacaul_HospiPal.Treatment");
            return obj;
        }

        /* This method gets a list of treatments a patient can receive from the database
        returns: a list of strings
         */
        public List<string> initializeTreatmentHistory()
        {
            //Initialize treatmentIDList
            treatmentIDList = new Dictionary<int, int>();

            //Database query to get all the treatments of a patient
            List<object[]> history = Database.Select("SELECT * FROM ismacaul_HospiPal.ReceivesTreatment WHERE patient = " + patientID + " ORDER BY (rtid)" );
            List<string> historyString = new List<string>();
            int i = 0;

            //Converts the elements received from database to a string
            foreach (object[] element in history)
            {
                historyString.Add(element[2].ToString() + " " + element[3].ToString() + "/" + element[4].ToString() + "/" + element[5].ToString());
                //Maps dictionary index to rtid
                treatmentIDList.Add(i, Convert.ToInt32(element[0]));
                i++;
            }
            return historyString;
        }

        /* This method gets a list of treatments a patient can receive from the database
        */
        public string getPatientName()
        {
            string name;
            List<object[]> patientInfo = Database.Select("SELECT fname, lname FROM ismacaul_HospiPal.Patient WHERE pid = " + patientID);

            name = patientInfo[0][0].ToString() + " " + patientInfo[0][1].ToString();
        
            return name;
        }

        /* This method gets a list of treatments a patient can receive from the database
        returns: string list of treatmentDetails
         */
        public List<string> getTreatmentDetails(int index)
        {
            List<string> info = new List<string>();
            List<object[]> patientInfo = Database.Select("SELECT * FROM ismacaul_HospiPal.ReceivesTreatment WHERE rtid = " + treatmentIDList[index]);
           // List<object[]> patientInfo2 = Database.Select("SELECT * FROM ismacaul_HospiPal.ReceivesTreatment WHERE rtid = 1");


            //Converts the attributes starting from treatmentType to string
            for (int i = 2; i < 9; i++)
            {
                info.Add(patientInfo[0][i].ToString());
            }

            //Checks to see if valid is a null
            //If not null convert to int
            int look = 0;
            if (patientInfo[0][8] != null)
            {
                look = Convert.ToInt32(info.ElementAt(6));
            }
            
            string s = "";
            //List of keys for doctors = string
            List<string> keyDoctors = new List<string>(this.doctorsList.Keys);
            
            //Look for a value from doctors because its converted to a int and returns the key as a string
            //Basically finds the key of the key value pair
            foreach (string n in keyDoctors)
            {
                if (doctorsList.ContainsValue(look)) // Will match once
                {
                    s = n;
                }
                else
                {
                    s = "";
                }
            }

            //Adds to list
            info.Add(s);

            //Returns list
            return info;
        }

        /* Method does a add query and adds patient to the database 
         */
        public bool AddTreatment(string TreatmentType, int DateDay, int DateMonth, int DateYear, string treatTime, string treatmentNotes, string d)
        {
            return Database.Insert(@"INSERT INTO ismacaul_HospiPal.ReceivesTreatment (patient, treatment, day, month, year, time, notes, treatingDoctor) 
                    VALUES (" + patientID + ", '" + TreatmentType + "', " + DateDay + ", " + DateMonth + ", " + DateYear + ", '" + treatTime + "', '" + treatmentNotes + "', " + doctorsList[d] + ");");
        
        }

        /* Method does a update query and the database with the new fields
         * Basically updates all the information except rtid as it is the primary key
        */
        public bool ModifyTreatment(int rtid, string TreatmentType, int DateDay, int DateMonth, int DateYear, string treatTime, string treatmentNotes, string d)
        {
            return Database.Update(@"UPDATE `ismacaul_HospiPal`.`ReceivesTreatment` SET `treatment`='" + TreatmentType + "' , `day`='" + DateDay + "' , `month`='" + DateMonth + "' , `year`='" + DateYear + "' , `time`='" + treatTime + "' , `notes`='" + treatmentNotes + "' , `treatingDoctor`='" + doctorsList[d] + "' WHERE `rtid`='" + treatmentIDList[rtid] + "'");

        }

        /* Removes treatment based on rtid as it is linked the the text box index
         */
        public bool RemoveTreatment(int rtid)
        {
            //More specific delete query
            //Database.Delete("DELETE FROM `ismacaul_HospiPal`.`ReceivesTreatment` WHERE `patient`=" + patientID + " and`treatment`=" + TreatmentType + " and`day`=" + DateDay + "  and`month`=" + DateMonth + "  and`year`=" + DateYear + " and`time`=" + treatTime +"; ");
            return Database.Delete("DELETE FROM `ismacaul_HospiPal`.`ReceivesTreatment` WHERE `rtid`=" + treatmentIDList[rtid] );

        }

        /* Gets a list of doctors
         * returns: string of doctors
         */
        public List<string> initializeDoctorList()
        {
            //Query to get a list of doctors based on a table from the database
            List<object[]> obj = Database.Select("SELECT eid, fname, lname FROM ismacaul_HospiPal.Employee WHERE employee_type = 'Doctor'");
            //String of doctors
            List<string> docList = new List<string>();
            string s;
            //Doctor name and int pair
            doctorsList = new Dictionary<string, int>();

            //Converts each object to string
            foreach (object[] element in obj)
            {
                s = element[1].ToString() + " " + element[2].ToString();
                if (!doctorsList.ContainsKey(s))
                {
                    docList.Add(s);
                    doctorsList.Add(s, Convert.ToInt32(element[0]));
                }
            }

            //returns a string list of doctors
            return docList;
        }

       
    }
}
