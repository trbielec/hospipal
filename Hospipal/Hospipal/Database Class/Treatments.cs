using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;


namespace Hospipal.Database_Class
{  
    public class Treatment
    {
        #region Attributes
        private int _treatmentID;
        private int _patientID;
        private string _Type;
        private DateTime _Date;
        private string _Time;
        private string _notes;
        private int _Doctor;
        private string _Status;
        #endregion

        #region Getters/Setters
        public int TreatmentID
        {
            get
            {
                return _treatmentID;
            }
        }

        public int PatientID
        {
            get
            {
                return _patientID;
            }
            set
            {
                _patientID = value;
            }
        }

        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
            }
        }

        public string DateToShortDateString
        {
            get
            {
                return _Date.ToShortDateString();
            }
        }
        public string Time
        {
            get
            {
                return _Time;
            }
            set
            {
                _Time = value;
            }
        }

        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
            }
        }

        public int Doctor
        {
            get
            {
                return _Doctor;
            }
            set
            {
                _Doctor = value;
            }
        }

        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        #endregion

        #region Constructor

        public Treatment()
        {
            _Date = new DateTime(1990, 1, 1);
        }

        public Treatment(int pID, string type, int day, int month, int year, string time, string notes, int doc,string status)
        {

            _patientID = pID;
            _Date = new DateTime(year, month, day);
            _Time = time;
            _Type = type;
            _notes = notes;
            _Doctor = doc;
            _Status = status;   
        }

        #endregion      
  
        #region Database Functions

        public bool Select()
        {
            List<object[]> SingleRow = Database.Select("SELECT * FROM ReceivesTreatment WHERE rtid = " + _treatmentID);
            if (SingleRow != null && SingleRow.Count > 0)
            {
                {
                    foreach (object[] row in SingleRow)
                    {
                        _treatmentID = Convert.ToInt32(row[0]);
                        _patientID = Convert.ToInt32(row[1]);
                        _Type = row[2].ToString();
                        _Date = new DateTime(Convert.ToInt32(row[5]), Convert.ToInt32(row[4]), Convert.ToInt32(row[3]));
                        _Time = row[6].ToString();
                        _notes = row[7].ToString();
                        _Doctor = Convert.ToInt32(row[8]);
                        _Status = row[9].ToString();
                    }
                }
                return true;
            }
            return false;
        }
        /* Method does a add query and adds patient to the database 
         */
        public static bool Insert(int pID, string TreatmentType, int DateDay, int DateMonth, int DateYear, string treatTime, string treatmentNotes, int dNum)
        {
            return Database.Insert(@"INSERT INTO ismacaul_HospiPal.ReceivesTreatment (patient, treatment, day, month, year, time, notes, treatingDoctor) 
                    VALUES (" + pID + ", '" + TreatmentType + "', " + DateDay + ", " + DateMonth + ", " + DateYear + ", '" + treatTime + "', '" + treatmentNotes + "', " + dNum + ");");

        }

        /* Method does a update query and the database with the new fields
        */
        public static bool Update(int rtid, string TreatmentType, int DateDay, int DateMonth, int DateYear, string treatTime, string treatmentNotes, int dNum)
        {
            return Database.Update(@"UPDATE `ismacaul_HospiPal`.`ReceivesTreatment` SET `treatment`='" + TreatmentType + "' , `day`='" + DateDay + "' , `month`='" + DateMonth + "' , `year`='" + DateYear + "' , `time`='" + treatTime + "' , `notes`='" + treatmentNotes + "' , `treatingDoctor`='" + dNum + "' WHERE `rtid`='" + rtid + "'");

        }

        public bool Delete()
        {
            return Database.Delete("Delete from ReceivesTreatment WHERE RTID = " + _treatmentID);
        }
        #endregion

        #region List Functions

        public static List<Treatment> GetTreatments(int pid)
        {
            List<object[]> rows = Database.Select("Select * FROM ReceivesTreatment WHERE patient = " + pid);
            List<Treatment> getTreatments = new List<Treatment>();

            foreach (object[] row in rows)
            {
                Treatment newTreatment = new Treatment(Convert.ToInt32(row[1]), row[2].ToString(),
                                        Convert.ToInt32(row[3]), Convert.ToInt32(row[4]), Convert.ToInt32(row[5]),
                                        row[6].ToString(), row[7].ToString(), Convert.ToInt32(row[8]),row[9].ToString());
                newTreatment._treatmentID = Convert.ToInt32(row[0]);
                getTreatments.Add(newTreatment);
            }
            return getTreatments;
        }

        public static List<String> GetTreatmentTypes()
        {
            List<object[]> rows = Database.Select("Select * FROM Treatment");
            List<String> allTreatmentTypes = new List<String>();

            foreach (object[] row in rows)
            {
                String treatmentType = row[0].ToString();
                allTreatmentTypes.Add(treatmentType);
            }
            return allTreatmentTypes;
        }

        #endregion
    }
}
