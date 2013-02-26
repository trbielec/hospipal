using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    public class Employee
    {
        private int _eid;
        private string _fname;
        private string _lname;
        private string _specialty;

        #region Getters/Setters

        public int Eid
        {
            get
            {
                return _eid;
            }
            set
            {
                _eid = value;
            }
        }

        public string Fname
        {
            get
            {
                return _fname;
            }
            set
            {
                _fname = value;
            }
        }

        public string Lname
        {
            get
            {
                return _lname;
            }
            set
            {
                _lname = value;
            }
        }

        public string Specialty
        {
            get
            {
                return _specialty;
            }
            set
            {
                _specialty = value; 
            }

        }
        #endregion


        #region Constructors
        public Employee()
        {
            //_eid = GenerateNewEid();
            //_fname = "";
            //_lname = "";
            //_specialty = "General";
        }

        public Employee(int eid)
        {
            _eid = eid; 
        }

        public Employee(int eid, string fname, string lname, string specialty)
        {
            _eid = eid; 
            _fname = fname;
            _lname = lname;
            _specialty = specialty; 
        }
        #endregion 

        #region Database Functions 
        public bool CheckDuplicates()
        {
            return Convert.ToInt32(Database.Select("SELECT * from Employee WHERE eid = " + _eid).ElementAt(0).ElementAt(0)) == _eid;
        }


        public bool Insert()
        {
            if (CheckDuplicates())
                return Database.Insert("Insert into Employee (eid,fname,lname,specialty)" +
                    "VALUES (" + _eid + ",'" + _fname + "','" + _lname + "'," + _specialty + "')");
            else
                return false;
        }

        public bool Update()
        {
            if (CheckDuplicates())
            {
                return Database.Update("Update Employee Set fname = '" + _fname + "', " +
                   "lname = '" + _lname + "' WHERE eid = " + _eid); ;
            }
            else
                return false;
        }

        public static int GenerateNewEid()
        {
            List<object[]> obj = Database.Select("SELECT eid FROM Employee");
            return obj.Count() + 1;
        }

        #endregion
    }
}
