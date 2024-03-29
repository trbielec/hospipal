﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using System.Collections.ObjectModel;

namespace Hospipal.Database_Class
{
    public class Schedule
    {
        private int _sid;
        private DateTime _start_time;
        private DateTime _end_time;
        private int _employee;
        private string _ward;


        #region Getters/Setters

        public int Sid
        {
            get
            {
                return _sid;
            }
            set
            {
                _sid = value;
            }
        }

        public DateTime Start_time
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

        public DateTime End_time
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

        public Schedule(int sid,DateTime start_time, DateTime end_time, int employee, string ward)
        {
            _sid = sid;
            _start_time = start_time;
            _end_time = end_time;
            _employee = employee;
            _ward = ward; 
        }

        #endregion


        #region Database functions

        public bool CheckforInsertConflicts()
        {
            List<object[]> scheduleTable = Database.Select("SELECT * from Schedule WHERE employee = " + _employee);
            if (scheduleTable != null)

                foreach (object[] row in scheduleTable)
                {
                    int currScheduleID = Convert.ToInt32(row[0]);
                    DateTime currScheduleStart = Convert.ToDateTime(row[1]);
                    DateTime currScheduleEnd = Convert.ToDateTime(row[2]);
                    int currScheduleEmployee = Convert.ToInt32(row[3]);


                    if (_end_time <= currScheduleStart)
                    {
                        continue;
                    }
                    else if (_start_time >= currScheduleEnd)
                    {
                        continue;
                    }
                    else
                        return false;
                }

            return true;
        }



        public bool CheckforUpdateConflicts()
        {
            List<object[]> scheduleTable = Database.Select("SELECT * from Schedule WHERE employee = " + _employee);
            if (scheduleTable != null)

                foreach (object[] row in scheduleTable)
                {
                    int currScheduleID = Convert.ToInt32(row[0]);
                    DateTime currScheduleStart = Convert.ToDateTime(row[1]);
                    DateTime currScheduleEnd = Convert.ToDateTime(row[2]);
                    int currScheduleEmployee = Convert.ToInt32(row[3]);

                    if (currScheduleID != _sid)
                    {
                        if (_end_time <= currScheduleStart)
                        {
                            continue;
                        }
                        else if (_start_time >= currScheduleEnd)
                        {
                            continue;
                        }
                        else
                            return false;

                    }
                   
                }

            return true;
        }

        public bool Insert()
        {
            MySqlCommand schedule = new MySqlCommand("Insert_Schedule(@start_time,@end_time,@employee,@ward);");
            schedule.Parameters.AddWithValue("start_time", _start_time);
            schedule.Parameters.AddWithValue("end_time", _end_time);
            schedule.Parameters.AddWithValue("employee", _employee);
            schedule.Parameters.AddWithValue("ward", _ward);
            return Database.Insert(schedule);
        }

        public bool Update()
        {
            MySqlCommand schedule = new MySqlCommand("Update_Schedule(@sid,@start_time,@end_time,@employee,@ward);");
            schedule.Parameters.AddWithValue("sid", _sid);
            schedule.Parameters.AddWithValue("start_time", _start_time);
            schedule.Parameters.AddWithValue("end_time", _end_time);
            schedule.Parameters.AddWithValue("employee", _employee);
            schedule.Parameters.AddWithValue("ward", _ward);
            return Database.Update(schedule);
        }

        public bool Delete()
        {
           return Database.Delete("DELETE FROM Schedule WHERE sid = " + _sid);
        }

        public static ObservableCollection<Appointment> Select()
        {
            List<object[]> scheduleTable = Database.Select("SELECT * from Schedule");
            ObservableCollection<Appointment> allAppointments = new ObservableCollection<Appointment>();
            if (scheduleTable != null)
            foreach (object[] row in scheduleTable)
            {
                Appointment newAppt = new Appointment();
                newAppt.Url = row[0].ToString();
                newAppt.Start = Convert.ToDateTime(row[1]);
                newAppt.End = Convert.ToDateTime(row[2]);
                newAppt.Body = row[3].ToString();
                newAppt.Subject = "Employee:  " + GetFullName(row[3].ToString()) + Environment.NewLine + "Ward: " + row[4].ToString();
                newAppt.Location = row[4].ToString();
                allAppointments.Add(newAppt);                  
            }
            return allAppointments;
        }
        
        public static int GenerateNextSid()
        {
            List<object[]> results = Database.Select("SELECT Auto_increment FROM information_schema.tables WHERE table_name= 'Schedule' AND table_schema = DATABASE();");
            return Convert.ToInt32(results[0][0]);
        }

        public static string GetFullName(string empID)
        {
            List<object[]> nameResult = Database.Select("SELECT * FROM Employee WHERE eid = " + empID);
            if (nameResult.Count > 0)
                return (nameResult[0][1].ToString() + " " + nameResult[0][2].ToString());
            else
                return "";
        }
        #endregion
    }
}
