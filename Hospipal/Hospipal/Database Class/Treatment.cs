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
            List<object[]> history = Database.Select("SELECT * FROM ismacaul_HospiPal.ReceivesTreatment WHERE patient = " + patientID);
            List<string> historyString = new List<string>();
            string s;

            foreach (object[] element in history)
            {
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
