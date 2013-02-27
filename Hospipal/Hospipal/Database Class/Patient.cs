using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(CheckDuplicates())
                return Database.Insert("Insert into Patient (Hc_no,fname,lname,dob_day,dob_month,dob_year,street_address,city,province,postal_code,home_phone_no,mobile_phone_no,work_phone_no)" +
                    "VALUES (" + _HealthCareNo + ",'" + _FirstName + "','" + _LastName + "'," + _DOB.Day + "," + _DOB.Month + "," + _DOB.Year + ",'" + _StreetAddress + "','" + _City + "','" + _Province + "','" + _PostalCode + "','" + _HomePhoneNo + "','" + _MobilePhoneNo + "','" + _WorkPhoneNo + "')");
            return false;
        }

        public bool Update()
        {
            if (CheckDuplicates())
                return Database.Update("Update Patient Set Hc_no = " + _HealthCareNo + ", fname = '" + _FirstName + "', " +
                   "lname = '" + _LastName + "', dob_day = " + _DOB.Day + ", dob_month = " + _DOB.Month + ", dob_year = " + _DOB.Year +
                   ", street_address = '" + _StreetAddress + "', city = '" + _City + "', province = '" + _Province +
                   "', postal_code = '" + _PostalCode + "', home_phone_no = '" + _HomePhoneNo + "', mobile_phone_no = '" + _MobilePhoneNo +
                   "', work_phone_no = '" + _WorkPhoneNo + "' WHERE Pid = " + _PatientID);;
            return false;
        }


        public bool Delete()
        {
            return Database.Delete("DELETE FROM Patient WHERE HC_NO = " + _HealthCareNo);
        }
        #endregion

        #region List Functions
        public static List<Patient> GetPatients() //Send in empty string if no search
        {

            List<object[]> rooms = Database.Select("Select * FROM Patient");
            List<Patient> getPatients = new List<Patient>();
            foreach (object[] row in rooms)
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
