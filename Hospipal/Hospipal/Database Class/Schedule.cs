using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace Hospipal.Database_Class
{
    class Schedule
    {
        private int _day;
        private int _month;
        private int _year;
        private int _start_time;
        private int _end_time;
        private int _employee;
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

        public int Start_time
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

        public int End_time
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

        public int Employee
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

        #region Constructors

        public Schedule()
        {
        }

        public Schedule(int day, int month, int year, int start_time, int end_time, int employee, string ward)
        {
            _day = day;
            _month = month;
            _year = year;
            _start_time = start_time;
            _end_time = end_time;
            _employee = employee;
            _ward = ward; 
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

        public bool Delete()
        {
           return Database.Delete("DELETE FROM Schedule WHERE day = " + _day + " AND month = " + _month + " AND year = " + _year + " AND start_time = " + _start_time + " AND end_time = " + _end_time + " AND employee = " + _employee);
        }

        public static List<Schedule> Select()
        {
            List<object[]> scheduleTable = Database.Select("SELECT * from Schedule");
            List<Schedule> allSchedules = new List<Schedule>();
            foreach (object[] row in scheduleTable)
            {
                Schedule newSchedule = new Schedule(Convert.ToInt32(row[0]), Convert.ToInt32(row[1]), Convert.ToInt32(row[2]),
                                            Convert.ToInt32(row[3]), Convert.ToInt32(row[4]), Convert.ToInt32(row[5]), row[6].ToString());
                allSchedules.Add(newSchedule);               
            }
            return allSchedules;
        }
        
        #endregion
    }
}
