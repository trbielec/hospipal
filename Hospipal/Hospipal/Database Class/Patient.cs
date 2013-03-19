using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Hospipal.Database_Class
{
    public class Patient  //Self Explanatory functions and identifiers
    {
        private int _PatientID;
        private int _HealthCareNo;
        private string _FirstName;
        private string _LastName;
        private DateTime _DOB;
        private string _StreetAddress;
        private string _City;
        private string _Province;
        private string _PostalCode;
        private string _HomePhoneNo;
        private string _MobilePhoneNo;
        private string _WorkPhoneNo;
        
        #region Getters/Setters
        public int PatientID
        {
            get
            {
                return _PatientID;
            }
            set
            {
                _PatientID = value;
            }
        }
        public int HealthCareNo
        {
            get
            {
                return _HealthCareNo;
            }
            set
            {
                _HealthCareNo = value;
                
            }
        }
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }
        public DateTime DOB
        {
            get
            {
                return _DOB;
            }
            set
            {
                _DOB = value;
            }
        }

        public string getDOB 
        {
            get
            {
                return _DOB.ToShortDateString();
            }
        }

        public string StreetAddress
        {
            get
            {
                return _StreetAddress;
            }
            set
            {
                _StreetAddress = value;
            }
        }
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
            }
        }
        public string Province
        {
            get
            {
                return _Province;
            }
            set
            {
                _Province = value;
            }
        }
        public string PostalCode
        {
            get
            {
                return _PostalCode;
            }
            set
            {
                _PostalCode = value;
            }
        }
        public string HomePhoneNo
        {
            get
            {
                return _HomePhoneNo;
            }
            set
            {
                _HomePhoneNo = value;
            }
        }
        public string WorkPhoneNo
        {
            get
            {
                return _WorkPhoneNo;
            }
            set
            {
                _WorkPhoneNo = value;
            }
        }
        public string MobilePhoneNo
        {
            get
            {
                return _MobilePhoneNo;
            }
            set
            {
                _MobilePhoneNo = value;
            }
        }
        #endregion

        #region Constructors

        public Patient()
        {
            _DOB = new DateTime(1990, 1, 1);
        }


        public Patient(int healthCareNo, string firstName, string lastName, DateTime dOB, string streetAddress, string city, string province, string postalCode, string homePhoneNo, string mobilePhoneNo, string workPhoneNo)
        {
            HealthCareNo = healthCareNo;
            FirstName = firstName;
            LastName = lastName;
            DOB = dOB;
            StreetAddress = streetAddress;
            City = city;
            Province = province;
            PostalCode = postalCode;
            HomePhoneNo = homePhoneNo;
            MobilePhoneNo = mobilePhoneNo;
            WorkPhoneNo = workPhoneNo;
        }
        public Patient(int HealthCareNo)
        {
            _HealthCareNo = HealthCareNo;
            Select();
        }
        #endregion

        #region DatabaseCalls

        private bool CheckDuplicates()
        {
            if (Database.Select("Select * from Patient WHERE HC_NO = " + _HealthCareNo).Count > 0)
                return Convert.ToInt32(Database.Select("SELECT * from Patient WHERE HC_NO = " + _HealthCareNo).ElementAt(0).ElementAt(0)) == _PatientID;
            else
                return true;
        }
        public bool Select()
        {
            List<object[]> SingleRow = Database.Select("SELECT * from Patient WHERE HC_NO = " + _HealthCareNo);
            if (SingleRow != null && SingleRow.Count > 0)
            {
                foreach (object[] row in SingleRow)
                {
                    _PatientID = Convert.ToInt32(row[0]);
                    _FirstName = row[2].ToString();
                    _LastName = row[3].ToString();
                    _DOB = new DateTime(Convert.ToInt32(row[6]), Convert.ToInt32(row[5]), Convert.ToInt32(row[4]));
                    _StreetAddress = row[7].ToString();
                    _City = row[8].ToString();
                    _Province = row[9].ToString();
                    _PostalCode = row[10].ToString();
                    _HomePhoneNo = row[11].ToString();
                    _MobilePhoneNo = row[12].ToString();
                    _WorkPhoneNo = row[13].ToString();
                }
                return true;
            }
            return false;
        }

        public bool Insert()
        {
            MySqlCommand patient = new MySqlCommand("Insert_Patient(@new_hc_no,@new_fname,@new_lname,@new_dob_day,@new_dob_month,@new_dob_year,@new_street_address,@new_city,@new_province,@new_postal_code,@new_home_phone_no,@new_mobile_phone_no,@new_work_phone_no);");
            if (CheckDuplicates())
            {
                patient.Parameters.AddWithValue("new_hc_no", _HealthCareNo);
                patient.Parameters.AddWithValue("new_fname", _FirstName);
                patient.Parameters.AddWithValue("new_lname", _LastName);
                patient.Parameters.AddWithValue("new_dob_day", _DOB.Day);
                patient.Parameters.AddWithValue("new_dob_month", _DOB.Month);
                patient.Parameters.AddWithValue("new_dob_year", _DOB.Year);
                patient.Parameters.AddWithValue("new_street_address", _StreetAddress);
                patient.Parameters.AddWithValue("new_city", _City);
                patient.Parameters.AddWithValue("new_province", _Province);
                patient.Parameters.AddWithValue("new_postal_code", _PostalCode);
                patient.Parameters.AddWithValue("new_home_phone_no", _HomePhoneNo);
                patient.Parameters.AddWithValue("new_mobile_phone_no", _MobilePhoneNo);
                patient.Parameters.AddWithValue("new_work_phone_no", _WorkPhoneNo);
                return Database.Insert(patient);
            } 
           return false;
        }

        public bool Update()
        {
            MySqlCommand patient = new MySqlCommand("Update_Patient(@same_pid,@new_hc_no,@new_fname,@new_lname,@new_dob_day,@new_dob_month," +
                                                                "@new_dob_year,@new_street_address,@new_city,@new_province,@new_postal_code," +
                                                                "@new_home_phone_no,@new_mobile_phone_no,@new_work_phone_no);");
            if (CheckDuplicates())
            {
                patient.Parameters.AddWithValue("same_pid", _PatientID);
                patient.Parameters.AddWithValue("new_hc_no", _HealthCareNo);
                patient.Parameters.AddWithValue("new_fname", _FirstName);
                patient.Parameters.AddWithValue("new_lname", _LastName);
                patient.Parameters.AddWithValue("new_dob_day", _DOB.Day);
                patient.Parameters.AddWithValue("new_dob_month", _DOB.Month);
                patient.Parameters.AddWithValue("new_dob_year", _DOB.Year);
                patient.Parameters.AddWithValue("new_street_address", _StreetAddress);
                patient.Parameters.AddWithValue("new_city", _City);
                patient.Parameters.AddWithValue("new_province", _Province);
                patient.Parameters.AddWithValue("new_postal_code", _PostalCode);
                patient.Parameters.AddWithValue("new_home_phone_no", _HomePhoneNo);
                patient.Parameters.AddWithValue("new_mobile_phone_no", _MobilePhoneNo);
                patient.Parameters.AddWithValue("new_work_phone_no", _WorkPhoneNo);
                return Database.Update(patient);
            }
            return false;
        }


        public bool Delete()
        {
            return Database.Delete("DELETE FROM Patient WHERE HC_NO = " + _HealthCareNo);
        }

        public bool GetPatient(int PatientID)
        {
            List<object[]> SingleRow = Database.Select("SELECT * from Patient WHERE pid = " + PatientID);
            if (SingleRow != null && SingleRow.Count > 0)
            {
                foreach (object[] row in SingleRow)
                {
                    _PatientID = Convert.ToInt32(row[0]);
                    _HealthCareNo = Convert.ToInt32(row[1]);
                    _FirstName = row[2].ToString();
                    _LastName = row[3].ToString();
                    _DOB = new DateTime(Convert.ToInt32(row[6]), Convert.ToInt32(row[5]), Convert.ToInt32(row[4]));
                    _StreetAddress = row[7].ToString();
                    _City = row[8].ToString();
                    _Province = row[9].ToString();
                    _PostalCode = row[10].ToString();
                    _HomePhoneNo = row[11].ToString();
                    _MobilePhoneNo = row[12].ToString();
                    _WorkPhoneNo = row[13].ToString();
                }
                return true;
            }
            return false;
        }
        #endregion

        #region List Functions
        public static List<Patient> GetPatients() //Send in empty string if no search
        {

            List<object[]> rows = Database.Select("Select * FROM Patient");
            List<Patient> getPatients = new List<Patient>();
            if (rows != null)
            foreach (object[] row in rows)
            {
                Patient newPatient = new Patient(Convert.ToInt32(row[1]), row[2].ToString(), row[3].ToString(),  
                                            new DateTime(Convert.ToInt32(row[6]), Convert.ToInt32(row[5]), Convert.ToInt32(row[4])),
                                            row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), 
                                            row[11].ToString(), row[12].ToString(), row[13].ToString());
                newPatient._PatientID = Convert.ToInt32(row[0]);
                getPatients.Add(newPatient);
            }
            return getPatients;
        }
        #endregion

    }
}
