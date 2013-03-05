using MySql.Data.MySqlClient;
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

        public Employee(int eid, string fname, string lname, string specialty, string employee_type, int supervisor_id)
        {
            _eid = eid; 
            _fname = fname;
            _lname = lname;
            _specialty = specialty;
            _employee_type = employee_type;
            _supervisor_id = supervisor_id;
        }
        #endregion 

        #region Database Functions 
        //Louay --No need to check duplicates as Eid should be auto-generated and never editable.
        public bool CheckDuplicates()
        {
            return Convert.ToInt32(Database.Select("SELECT * from Employee WHERE eid = " + _eid).ElementAt(0).ElementAt(0)) == _eid;
        }


        public bool Insert()
        {
            _employee_type = "Nurse";
            _supervisor_id = 1;
            MySqlCommand employee = new MySqlCommand("Insert_Employee(@fname,@lname,@specialty,@employee_type,@supervisor_id);");
            employee.Parameters.AddWithValue("fname", _fname);
            employee.Parameters.AddWithValue("lname", _lname);
            employee.Parameters.AddWithValue("specialty", _specialty);
            employee.Parameters.AddWithValue("employee_type", _employee_type);
            employee.Parameters.AddWithValue("supervisor_id", _supervisor_id);
            
            // We know that the generated ID is unique so go ahead and insert
            return Database.Insert(employee);
        }

        public bool Update()
        {
            _employee_type = "Nurse";
            _supervisor_id = 1;
            MySqlCommand employee = new MySqlCommand("Update_Employee(@eid,@fname,@lname,@specialty,@employee_type,@supervisor_id);");
            employee.Parameters.AddWithValue("eid", _eid);
            employee.Parameters.AddWithValue("fname", _fname);
            employee.Parameters.AddWithValue("lname", _lname);
            employee.Parameters.AddWithValue("specialty", _specialty);
            employee.Parameters.AddWithValue("employee_type", _employee_type);
            employee.Parameters.AddWithValue("supervisor_id", _supervisor_id);
            return Database.Update(employee);
           /* if (CheckDuplicates())
            {
                return Database.Update("Update Employee Set fname = '" + _fname + "', " +
                   "lname = '" + _lname + "', specialty = '" + _specialty + "', employee_type = '" + _employee_type + "', supervisor_id = " + _supervisor_id + " WHERE eid = " + _eid);
            }
            else
                return false;*/
        }

        public bool Delete()
        {
            return Database.Delete("DELETE FROM Employee WHERE eid = " + _eid);
        }

        /*public static int GenerateNewEid()  //Louay - Should replace eid with Auto-Number.  This will break if you delete the 2nd last one and try to insert one,
                                            //Eg. 3 rows, 1,2,3 delete #2 add a new one eid will generate #3 for ID, so it will never be able to insert a new one.
        {
            List<object[]> obj = Database.Select("SELECT eid FROM Employee");
            return obj.Count() + 1;
        }*/

        public static List<Employee> GetEmployees() //Send in empty string if no search
        {

            List<object[]> employeeList = Database.Select("Select * FROM Employee");
            List<Employee> getEmployees = new List<Employee>();
            foreach (object[] row in employeeList)
            {
                Employee newEmployee = new Employee(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(),
                                            row[3].ToString(), row[4].ToString(), Convert.ToInt32(row[5]));
                getEmployees.Add(newEmployee);
            }
            return getEmployees;
        }

        #endregion
    }
}
