using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    public class Prescription
    { 
        #region Attributes
        private int _prescriptionID;
        private int _patientID;
        private int _Doctor;
        private string _prescriptionName;
        private string _notes;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _status;
        #endregion

        #region Getters/Setters
        public int PrescriptionID
        {
            get
            {
                return _prescriptionID;
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

        public string PrescriptionName
        {
            get
            {
                return _prescriptionName;
            }
            set
            {
                _prescriptionName = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
            }
        }

        public string StartDateToShortDateString
        {
            get
            {
                return _startDate.ToShortDateString();
            }
            set 
            {
                _startDate = Convert.ToDateTime(value);
            }
        }
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }

        public string EndDateToShortDateString
        {
            get
            {
                return _endDate.ToShortDateString();
            }
            set 
            {
                _endDate = Convert.ToDateTime(value);
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
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        
        #endregion

        #region Constructor

        public Prescription()
        {
            _startDate = new DateTime();
        }

        public Prescription(int pID, int doc, string pxName, string notes, DateTime startDate, DateTime endDate, string status)
        {

            _patientID = pID;
            _startDate = startDate;
            _endDate = endDate;
            _prescriptionName = pxName;
            _notes = notes;
            _Doctor = doc;
            _status = status;
        }

        #endregion      
  
        #region Database Functions

        public bool Select()
        {
            List<object[]> SingleRow = Database.Select("SELECT * FROM Prescription WHERE prid = " + _prescriptionID);
            if (SingleRow != null && SingleRow.Count > 0)
            {
                {
                    foreach (object[] row in SingleRow)
                    {
                        _prescriptionID = Convert.ToInt32(row[0]);
                        _patientID = Convert.ToInt32(row[1]);
                        _Doctor = Convert.ToInt32(row[2]);
                        _prescriptionName = row[3].ToString();
                        _notes = row[4].ToString();
                        _startDate = Convert.ToDateTime(row[5]);
                        _endDate = Convert.ToDateTime(row[6]);
                        _status = row[7].ToString();
                       
                    }
                }
                return true;
            }
            return false;
        }
        /* Method does a add query and adds patient to the database 
         */
        public bool Insert()
        {
            MySqlCommand prescription = new MySqlCommand("Insert_Prescription(@pid,@doctor,@pxName,@pxNotes,@startDate,@endDate,@status)");
            prescription.Parameters.AddWithValue("pid", _patientID);
            prescription.Parameters.AddWithValue("doctor", _Doctor); 
            prescription.Parameters.AddWithValue("pxName", _prescriptionName);
            prescription.Parameters.AddWithValue("pxNotes", _notes);
            prescription.Parameters.AddWithValue("startDate", StartDateToShortDateString);
            prescription.Parameters.AddWithValue("endDate", EndDateToShortDateString);
            prescription.Parameters.AddWithValue("status", _status);
            

            return Database.Insert(prescription);

        }

        /* Method does a update query and the database with the new fields
        */
        public bool Update()
        {
            MySqlCommand prescription = new MySqlCommand("Update_Prescription(@prid,@pid,@doctor,@pxName,@pxNotes,@startDate,@endDate,@status)");
            prescription.Parameters.AddWithValue("prid", _prescriptionID);
            prescription.Parameters.AddWithValue("pid", _patientID);
            prescription.Parameters.AddWithValue("doctor", _Doctor);
            prescription.Parameters.AddWithValue("pxName", _prescriptionName);
            prescription.Parameters.AddWithValue("pxNotes", _notes);
            prescription.Parameters.AddWithValue("startDate", StartDateToShortDateString);
            prescription.Parameters.AddWithValue("endDate", EndDateToShortDateString);
            prescription.Parameters.AddWithValue("status", _status);
            return Database.Update(prescription);
        }

        public bool Delete()
        {
            return Database.Delete("Delete from Prescription WHERE prid = " + _prescriptionID);
        }
        #endregion

        #region List Functions

        public static List<Prescription> GetPrescriptions(int pid,string status)
        {
            List<object[]> rows = Database.Select("Select * FROM Prescription WHERE pid = " + pid + " AND prescriptionstatus = '" + status + "'");
            List<Prescription> getPrescriptions = new List<Prescription>();
            if (rows != null)
            foreach (object[] row in rows)
            {
                Prescription prescription = new Prescription(Convert.ToInt32(row[1]), Convert.ToInt32(row[2]),
                                        row[3].ToString(), row[4].ToString(), Convert.ToDateTime(row[5]),
                                        Convert.ToDateTime(row[6]), row[7].ToString());
                prescription._prescriptionID = Convert.ToInt32(row[0]);
                getPrescriptions.Add(prescription);
            }
            return getPrescriptions;
        }

        
        #endregion

        public static int GenerateNextprid()
        {
            List<object[]> results = Database.Select("SELECT Auto_increment FROM information_schema.tables WHERE table_name= 'Prescription' AND table_schema = DATABASE();");
            return Convert.ToInt32(results[0][0]);
        }
    }
}
