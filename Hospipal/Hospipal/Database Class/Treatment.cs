using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    public class Treatment
    {
        int patientID;

        Dictionary<string, int> doctorsList;
     Dictionary<int, int> treatmentIDList;
        
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
 
            treatmentIDList = new Dictionary<int, int>();
            List<object[]> history = Database.Select("SELECT * FROM ismacaul_HospiPal.ReceivesTreatment WHERE patient = " + patientID + " ORDER BY (rtid)" );

          

            List<string> historyString = new List<string>();
            int i = 0;

            foreach (object[] element in history)
            {

                historyString.Add(element[2].ToString() + " " + element[3].ToString() + "/" + element[4].ToString() + "/" + element[5].ToString());
                treatmentIDList.Add(i, Convert.ToInt32(element[0]));
                i++;
            }
            return historyString;
        }

        public string getPatientName()
        {
            string name;
            List<object[]> patientInfo = Database.Select("SELECT fname, lname FROM ismacaul_HospiPal.Patient WHERE pid = " + patientID);

            name = patientInfo[0][0].ToString() + " " + patientInfo[0][1].ToString();
        
            return name;
        }

        public List<string> getTreatmentDetails(int index)
        {
            List<string> info = new List<string>();
            List<object[]> patientInfo = Database.Select("SELECT * FROM ismacaul_HospiPal.ReceivesTreatment WHERE rtid = " + treatmentIDList[index]);
           // List<object[]> patientInfo2 = Database.Select("SELECT * FROM ismacaul_HospiPal.ReceivesTreatment WHERE rtid = 1");


            for (int i = 2; i < 9; i++)
            {
                info.Add(patientInfo[0][i].ToString());
            }

              int look = 0;
              if (patientInfo[0][8] != null)
              {
                  look = Convert.ToInt32(info.ElementAt(6));
              }
            string s = "";
            List<string> keyDoctors = new List<string>(this.doctorsList.Keys);
            //info.Add(doctorsList.GetKey(look));

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

            info.Add(s);
            return info;
        }


        public void AddTreatment(string TreatmentType, int DateDay, int DateMonth, int DateYear, string treatTime, string treatmentNotes, string d)
        {
            Database.Insert(@"INSERT INTO ismacaul_HospiPal.ReceivesTreatment (patient, treatment, day, month, year, time, notes, treatingDoctor) 
                    VALUES (" + patientID + ", '" + TreatmentType + "', " + DateDay + ", " + DateMonth + ", " + DateYear + ", '" + treatTime + "', '" + treatmentNotes + "', " + doctorsList[d] + ");");
        
        }

        public void ModifyTreatment(int rtid, string TreatmentType, int DateDay, int DateMonth, int DateYear, string treatTime, string treatmentNotes, string d)
        {
            Database.Update(@"UPDATE `ismacaul_HospiPal`.`ReceivesTreatment` SET `treatment`='" + TreatmentType + "' , `day`='" + DateDay + "' , `month`='" + DateMonth + "' , `year`='" + DateYear + "' , `time`='" + treatTime + "' , `notes`='" + treatmentNotes + "' , `treatingDoctor`='" + doctorsList[d] + "' WHERE `rtid`='" + treatmentIDList[rtid] + "'");

        }

        public void RemoveTreatment(int rtid)
        {
            //More specific delete query
            //Database.Delete("DELETE FROM `ismacaul_HospiPal`.`ReceivesTreatment` WHERE `patient`=" + patientID + " and`treatment`=" + TreatmentType + " and`day`=" + DateDay + "  and`month`=" + DateMonth + "  and`year`=" + DateYear + " and`time`=" + treatTime +"; ");
            Database.Delete("DELETE FROM `ismacaul_HospiPal`.`ReceivesTreatment` WHERE `rtid`=" + treatmentIDList[rtid] );

        }

        public List<string> initializeDoctorList()
        {
            List<object[]> obj = Database.Select("SELECT eid, fname, lname FROM ismacaul_HospiPal.Employee WHERE employee_type = 'Doctor'");
            List<string> docList = new List<string>();
            string s;
            doctorsList = new Dictionary<string, int>();

            foreach (object[] element in obj)
            {
                s = element[1].ToString() + " " + element[2].ToString();
                docList.Add(s);
                doctorsList.Add(s, Convert.ToInt32(element[0]));
            }

            return docList;
        }

       
    }
}
