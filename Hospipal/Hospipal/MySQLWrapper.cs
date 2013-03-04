using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal
{
    using MySql.Data.MySqlClient;

    public class MySQLWrapper
    {
        private MySqlConnection connection; // connection object to the database
        private string server;              // server name
        private string database;            // database name
        private string userid;              // user id for database
        private string password;            // password for user

        /*
        * Constructor: Initializes server, database, userid, and password variables and creates a MySQLConnection object
        * 	@param server
        *		- A string for the server of the DB
        * 	@param database
        * 		- A string for the database name
        *	@param userid
        *		- A string for the user id
        *	@param password
        *		- A string for the password
        */
        public MySQLWrapper(string server, string database, string userid, string password)
        {
            this.server = server;
            this.database = database;
            this.userid = userid;
            this.password = password;
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + userid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        /*
         * OpenConnection: Opens the connection to the database server, outputs an error message if one occurs
         *  - returns true if successful, false otherwise
         */
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Console.WriteLine("Error: Unable to connect to server");
                        break;
                    default:
                        Console.WriteLine("MySQL Error " + e.Number + ":" + e.Message);
                        break;
                }
                return false;
            }
        }

        /*
         * CloseConnection: Closes the connection to the database server
         */
        private void CloseConnection()
        {
            connection.Close();
        }

        /*
         * Update: Used to run an UPDATE query
         *  @param query
         *      - The string value of the query
         *  - Returns true if successful, false otherwise
         */
        public bool Update(string query)
        {
            return RunQueryWithNoReturn(query);
        }

        /*
         * Insert: Used to run an INSERT query
         *  @param query
         *      - The string value of the query
         *  - Returns true if successful, false otherwise
         */
        public bool Insert(string query)
        {
            return RunQueryWithNoReturn(query);
        }

        /*
         * Select: Used to run an SELECT query
         *  @param query
         *      - The string value of the query
         *  - Returns the values returned from the database as a List<object[]>
         */
        public List<object[]> Select(string query)
        {
            return RunQueryWithReturn(query);
        }

        /*
         * Delete: Used to run an DELETE query
         *  @param query
         *      - The string value of the query
         *  - Returns true if successful, false otherwise
         */
        public bool Delete(string query)
        {
            return RunQueryWithNoReturn(query);
        }

        /*
         * RunQueryWithNoReturn: Runs the provided query string on the database.
         *  @param query
         *      - The string value of the query
         *  - Returns true if the query is successful, false otherwise
         */
        private bool RunQueryWithNoReturn(string query)
        {
            if (OpenConnection())
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    int ret = command.ExecuteNonQuery();
                    CloseConnection();
                    if (ret > 0)
                        return true;
                    else
                        return false;
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("MySQL Error " + e.Number + ":" + e.Message);
                    CloseConnection();
                }
            }

            return false;
        }

        /*
         * RunQueryWithReturn: Runs a query on the datbase that returns data
         *  @param query
         *      - The string value of the query
         *	- Returns a List<object[]> where each element in the List is one row, null otherwise
         */
        private List<object[]> RunQueryWithReturn(string query)
        {
            if (OpenConnection())
            {
                try
                {
                    // Create SQL Command from query and connection object
                    MySqlCommand command = new MySqlCommand(query, connection);
                    // Execute command
                    MySqlDataReader data = command.ExecuteReader();

                    List<object[]> allData = new List<object[]>();
                    // Loop through all returned objects from the DB and add them to the List
                    // Each element in the list is an object[] containing all the data for each row
                    while (data.Read())
                    {
                        object[] rowData = new object[data.FieldCount];
                        for (int i = 0; i < rowData.Length; i++)
                        {
                            rowData[i] = data[i];
                        }
                        allData.Add(rowData);
                    }

                    // clean up
                    data.Close();
                    CloseConnection();
                    return allData;
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("MySQL Error " + e.Number + ":" + e.Message);
                    CloseConnection();
                }
            }

            return null;
        }

        /*
         * TestConnection(): Tests the connection to the database server
         *  - Returns true is successful, false otherwise
         */
        public bool TestConnection()
        {
            if (OpenConnection())
            {
                CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool Update(MySqlCommand command)
        {
            return RunQueryWithNoReturn(command);
        }
        internal bool Insert(MySqlCommand command)
        {
            return RunQueryWithNoReturn(command);
        }

        private bool RunQueryWithNoReturn(MySqlCommand command)
        {
            if (OpenConnection())
            {
                try
                {
                    command.Connection = connection;
                    int ret = command.ExecuteNonQuery();
                    CloseConnection();
                    if (ret > 0)
                        return true;
                    else
                        return false;
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("MySQL Error " + e.Number + ":" + e.InnerException.Message);
                    CloseConnection();
                }
            }

            return false;
        }

        private List<object[]> RunQueryWithReturn(MySqlCommand command)
        {
            if (OpenConnection())
            {
                try
                {
                    // Create SQL Command from query and connection object
                    command.Connection =  connection;
                    // Execute command
                    MySqlDataReader data = command.ExecuteReader();
                    List<object[]> allData = new List<object[]>();
                    // Loop through all returned objects from the DB and add them to the List
                    // Each element in the list is an object[] containing all the data for each row
                    while (data.Read())
                    {
                        object[] rowData = new object[data.FieldCount];
                        for (int i = 0; i < rowData.Length; i++)
                        {
                            rowData[i] = data[i];
                        }
                        allData.Add(rowData);
                    }

                    // clean up
                    data.Close();
                    CloseConnection();
                    return allData;
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("MySQL Error " + e.Number + ":" + e.Message);
                    CloseConnection();
                }
            }

            return null;
        }
    }



}
