using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hospipal.Database_Class
{
    class Schedule
    {
        private int _day;
        private int _month;
        private int _year;
        private string _start_time;
        private string _end_time;
        private string _employee;
        private string _ward;


#region Getters/Setters

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

        public string Start_time
        {
            get
            {
                return _start_time;
            }
            set
            {
                _start_time = value;
            }
        }

        public string End_time
        {
            get
            {
                return _end_time;
            }
            set
            {
                _end_time = value;
            }

        }

        public string Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
            }
        }

        public string Ward
        {
            get
            {
                return _ward;
            }
            set
            {
                _ward = value; 
            }
        }

#endregion

#region Database functions

        public bool Insert()
        {
            MySqlCommand schedule = new MySqlCommand("Insert_Schedule(@day,@month,@year,@start_time,@end_time,@employee,@ward);");
            schedule.Parameters.AddWithValue("day", _day);
            schedule.Parameters.AddWithValue("month", _month);
            schedule.Parameters.AddWithValue("year", _year);
            schedule.Parameters.AddWithValue("start_time", _start_time);
            schedule.Parameters.AddWithValue("end_time", _end_time);
            schedule.Parameters.AddWithValue("employee", _employee);
            schedule.Parameters.AddWithValue("ward", _ward);
            return Database.Insert(schedule);
        }

        public bool Update()
        {
            MySqlCommand schedule = new MySqlCommand("Update_Schedule(@day,@month,@year,@start_time,@end_time,@employee,@ward);");
            schedule.Parameters.AddWithValue("day", _day);
            schedule.Parameters.AddWithValue("month", _month);
            schedule.Parameters.AddWithValue("year", _year);
            schedule.Parameters.AddWithValue("start_time", _start_time);
            schedule.Parameters.AddWithValue("end_time", _end_time);
            schedule.Parameters.AddWithValue("employee", _employee);
            schedule.Parameters.AddWithValue("ward", _ward);
            return Database.Update(schedule);
        }

        /*public bool Delete()
        {
           return Database.Delete("DELETE FROM Schedule WHERE day = " + _day + " AND month = " + _month + " AND year = " + _year);
        }*/
#endregion




    }
}
