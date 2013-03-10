using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;


namespace Hospipal.Database_Class
{  
    public class SingleTreatment
    {
        #region Attributes
        private int _treatmentID;
        private int _patientNumber;
        private string _treatmentType;
        private int _day;
        private int _month;
        private int _year;
        private string _time;
        private string _notes;
        private int _treatingDoctor;
        private DateTime _realDate;
        private DateTime _realTime;
        #endregion

        #region Getters/Setters
        public int TreatmentID
        {
            get
            {
                return _treatmentID;
            }
            set
            {
                _treatmentID = value;
            }
        }

        public int PatientNumber
        {
            get
            {
                return _patientNumber;
            }
            set
            {
                _patientNumber = value;
            }
        }

        public string TreatmentType
        {
            get
            {
                return _treatmentType;
            }
            set
            {
                _treatmentType = value;
            }
        }

        public int Day
        {
            get
            {
                return _day;
            }
            set
            {
                _day = value;
            }
        }

        public int Month
        {
            get
            {
                return _month;
            }
            set
            {
                _month = value;
            }
        }

        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
            }
        }

        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
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

        public int TreatingDoctor
        {
            get
            {
                return _treatingDoctor;
            }
            set
            {
                _treatingDoctor = value;
            }
        }

        public DateTime RealDate
        {
            get
            {
                return _realDate;
            }
            
        }

        public DateTime RealTime
        {
            get
            {
                return _realTime;
            }

        }
        #endregion

        #region Constructor
        public SingleTreatment(int pID, string type, int day, int month, int year, string time, string notes, int doc)
        {
            setDateFormat();

            _patientNumber = pID;
            _treatmentType = type;
            _day = day;
            _month = month;
            _year = year;
            _time = time;
            _notes = notes;
            _treatingDoctor = doc;
            _realDate = Convert.ToDateTime(day + "/" + month + "/" + year);
            _realDate = _realDate.Date;
            _realTime = Convert.ToDateTime(time);
        }

        #endregion      
  
        #region Functions and Logic
        public void setDateFormat()
        {
            //Sets a default date format to formate date because this will display differently on different computers and just standardizes it
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }

        /* Method does a add query and adds patient to the database 
         */
        public static bool AddTreatment(int pID, string TreatmentType, int DateDay, int DateMonth, int DateYear, string treatTime, string treatmentNotes, int dNum)
        {
            return Database.Insert(@"INSERT INTO ismacaul_HospiPal.ReceivesTreatment (patient, treatment, day, month, year, time, notes, treatingDoctor) 
                    VALUES (" + pID + ", '" + TreatmentType + "', " + DateDay + ", " + DateMonth + ", " + DateYear + ", '" + treatTime + "', '" + treatmentNotes + "', " + dNum + ");");

        }

        /* Method does a update query and the database with the new fields
        */
        public static bool ModifyTreatment(int rtid, string TreatmentType, int DateDay, int DateMonth, int DateYear, string treatTime, string treatmentNotes, int dNum)
        {
            return Database.Update(@"UPDATE `ismacaul_HospiPal`.`ReceivesTreatment` SET `treatment`='" + TreatmentType + "' , `day`='" + DateDay + "' , `month`='" + DateMonth + "' , `year`='" + DateYear + "' , `time`='" + treatTime + "' , `notes`='" + treatmentNotes + "' , `treatingDoctor`='" + dNum + "' WHERE `rtid`='" + rtid + "'");

        }

        #endregion
    }
}
