using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    class Search
    {
        #region Attributes
        string _tableName;
        StringBuilder _query = new StringBuilder("SELECT * FROM ");
        #endregion

        #region Getter/Setters
        public string TableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                _tableName = value;
            }
        }
 
        #endregion

        #region Constructor
        public Search(string FromTableName)
        {
            TableName = FromTableName;
            this._query.Append(TableName + " WHERE ");
        }
        #endregion

        #region Functions and Logic
        /* Returns a list of patients based on query built from entries in UI
         */
        public static List<Patient> SearchPatient(string queryBuilt) 
        {
            List<Patient> getPatients = new List<Patient>();
            try
            {
                List<object[]> dbPatient = Database.Select(queryBuilt);
                foreach (object[] row in dbPatient)
                {
                    Patient newPatient = new Patient(Convert.ToInt32(row[1]), row[2].ToString(), row[3].ToString(),
                                                new DateTime(Convert.ToInt32(row[6]), Convert.ToInt32(row[5]), Convert.ToInt32(row[4])),
                                                row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(),
                                                row[11].ToString(), row[12].ToString(), row[13].ToString());
                    getPatients.Add(newPatient);
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Error getting data from database and/or converting");
            }
            return getPatients;
        }

        /* Returns a list of employees based on query built from entries in UI
        */
        public static List<Employee> SearchEmployees(string queryBuilt)
        {
            List<Employee> getEmployees = new List<Employee>();
            try
            {
                List<object[]> employeeList = Database.Select(queryBuilt);
                foreach (object[] row in employeeList)
                {
                    Employee newEmployee = new Employee(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(),
                                                row[3].ToString(), row[4].ToString(), Convert.ToInt32(row[5]));
                    getEmployees.Add(newEmployee);
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Error getting data from database and/or converting");
            }
            return getEmployees;
        }

        /* Uses lists generated from UI to build query where cSideVariables matches dbSideVariables for database call
        */
        public void UseInputs(List<string> dbSideVariables, List<string> cSideVariables)
        {
            for (int i = 0; i < cSideVariables.Count(); i++)
            {
                if (!(i == (cSideVariables.Count - 1)))
                {
                    BuildStringQuery(dbSideVariables[i], cSideVariables[i]);
                    BuildStringQuery(" AND ");
                }
                else
                {
                    if (dbSideVariables[i] == "home_phone_no")
                    {
                        dbSideVariables.Add("mobile_phone_no");
                        dbSideVariables.Add("work_phone_no");
                        BuildStringQuery("(");
                        BuildStringQuery(dbSideVariables[i], cSideVariables[i]);
                        BuildStringQuery(" OR ");
                        BuildStringQuery(dbSideVariables[i + 1], cSideVariables[i]);
                        BuildStringQuery(" OR ");
                        BuildStringQuery(dbSideVariables[i + 2], cSideVariables[i]);
                        BuildStringQuery(")");
                    }
                    else
                    {
                        BuildStringQuery(dbSideVariables[i], cSideVariables[i]);
                    }
                }
            }

        }

        public void BuildStringQuery(string dbSideVariables, string cSideVariables)
        {
            _query.Append("(" + dbSideVariables + " LIKE '%" + cSideVariables +"%')");
        }

        public void BuildStringQuery(string singleArgument)
        {
            _query.Append(singleArgument);
        }

        public string GetBuiltQuery()
        {
            return this._query.ToString();
        }
        
        #endregion
    }
}
