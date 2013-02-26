﻿using System;
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
        private string _employee_type;
        private int _supervisor_id; 

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

        public string Employee_type
        {
            get
            {
                return _employee_type;
            }
            set
            {
                _employee_type = value;
            }

        }

        public int Supervisor_id
        {
            get
            {
                return _supervisor_id;
            }
            set
            {
                _supervisor_id = value;
            }
        }
        #endregion


        #region Constructors
        public Employee()
        {
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
            _employee_type = "Nurse";
            _supervisor_id = 1;
            // We know that the generated ID is unique so go ahead and insert
            return Database.Insert("Insert into Employee (eid,fname,lname,specialty,employee_type,supervisor_id)" +
                    "VALUES (" + _eid + ",'" + _fname + "','" + _lname + "','" + _specialty + "','" + _employee_type + "'," + _supervisor_id + ")");
        }

        public bool Update()
        {
            _employee_type = "Nurse";
            _supervisor_id = 1;

            if (CheckDuplicates())
            {
                return Database.Update("Update Employee Set fname = '" + _fname + "', " +
                   "lname = '" + _lname + "', specialty = '" + _specialty + "', employee_type = '" + _employee_type + "', supervisor_id = " + _supervisor_id + " WHERE eid = " + _eid);
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
