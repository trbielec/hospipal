using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    class Treatment
    {
        int patientID;

        Dictionary<string, int> doctorsList;

        public Treatment(int pID)
        {
            patientID = pID;
        }

        public List<object[]> initializeTreatmentList()
        {
            List<object[]> obj = Database.Select("SELECT * FROM ismacaul_HospiPal.Treatment");
            return obj;
        }




        public List<string> initializeTreatmentHistory()
        {

            List<object[]> history = Database.Select("SELECT * FROM ismacaul_HospiPal.ReceivesTreatment WHERE patient = " + patientID + " ORDER BY (rtid)" );

            List<object[]> history = Database.Select("SELECT * FROM ismacaul_HospiPal.ReceivesTreatment WHERE patient = " + patientID);

            List<string> historyString = new List<string>();
            string s;

            foreach (object[] element in history)
            {

                historyString.Add(element[2].ToString() + " " + element[3].ToString() + "/" + element[4].ToString() + "/" + element[5].ToString());

                historyString.Add(element[1].ToString() + " " + element[2].ToString() + "/" + element[3].ToString() + "/" + element[4].ToString());

            }
            return historyString;
        }

        public string getPatientName()
        {
            string name;
            List<object[]> patientInfo = Database.Select("SELECT fname, lname FROM ismacaul_HospiPal.Patient WHERE pid = " + patientID);

            name = patientInfo[0][0].ToString() + " " + patientInfo[0][1].ToString();
            //            historyString.Add(element[1].ToString() + " " + element[2].ToString() + "/" + element[3].ToString() + "/" + element[4].ToString());


            return name;
        }



        public void AddTreatment(string TreatmentType, int DateDay, int DateMonth, int DateYear, string treatTime, string treatmentNotes, string d)
        {
            Database.Insert(@"INSERT INTO ismacaul_HospiPal.ReceivesTreatment (patient, treatment, day, month, year, time, notes, treatingDoctor) 
                    VALUES (" + patientID + ", '" + TreatmentType + "', " + DateDay + ", " + DateMonth + ", " + DateYear + ", '" + treatTime + "', '" + treatmentNotes + "', " + doctorsList[d] + ");");
        
        }

        public List<string> initializeDoctorList()
        {
            List<object[]> obj = Database.Select("SELECT eid, fname, lname FROM ismacaul_HospiPal.Employee WHERE employee_type = 'Doctor'");
            List<string> docList = new List<string>();
            string s;
            doctorsList = new Dictionary<string,int>();

            foreach (object[] element in obj)
            {
                s = element[1].ToString() + " " + element[2].ToString();
                docList.Add(s);
                doctorsList.Add(s, Convert.ToInt32(element[0]));
            }

            return docList;

        internal void AddTreatment()
        {
            throw new NotImplementedException();
        }

        public void AddTreatment(string TreatmentType, int DateDay, int DateMonth, int DateYear, string treatTime, string treatmentNotes)
        {
            Database.Insert(@"INSERT INTO ismacaul_HospiPal.ReceivesTreatment (patient, treatment, day, month, year, time, notes) 
                    VALUES (" + patientID + ", '" + TreatmentType + "', " + DateDay + ", " + DateMonth + ", " + DateYear + ", '" + treatTime + "', '" + treatmentNotes + "');");
        
        }
    }
}
