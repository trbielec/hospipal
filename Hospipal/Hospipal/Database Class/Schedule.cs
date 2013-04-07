using System;
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

        public bool CheckforConflicts()
        {
            List<object[]> scheduleTable = Database.Select("SELECT * from Schedule");
            if (scheduleTable != null)

                foreach (object[] row in scheduleTable)
                {
                    // Conflict if start times or end times the same
                    if (_start_time == Convert.ToDateTime(row[1]) ||
                         _end_time == Convert.ToDateTime(row[2]))
                        return false;

                    // New schedule starts before start time of existing schedule
                    if (_start_time < Convert.ToDateTime(row[1]))
                    {
                        // Conflict if SS starts before end time of PS
                        if (Convert.ToDateTime(row[1]) < _end_time)
                            return false;
                    }
                    // Else new schedule starts after start time of existing schedule
                    else
                    {
                        //Conflict if PS starts before end time of PS
                        if (_start_time < Convert.ToDateTime(row[2]))
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
                newAppt.UniqueId = row[0].ToString();
                newAppt.Start = Convert.ToDateTime(row[1]);
                newAppt.End = Convert.ToDateTime(row[2]);
                newAppt.Subject = row[3].ToString();
                newAppt.Location = row[4].ToString();
                /*
                newAppt.Body = "Start - " + newAppt.Start.ToString() + "\n" +
                            "End - " + newAppt.End.ToString() + "\n" +
                            "Ward - " + newAppt.Location.ToString();
                */


                allAppointments.Add(newAppt);               
            }
            return allAppointments;
        }

        public static Appointment Select(string sid)
        {
            List<object[]> scheduleTable = Database.Select("SELECT * from Schedule");
            Appointment apt = new Appointment();
            foreach (object[] row in scheduleTable)
            {
                
                if (sid.Equals(row[0].ToString()))
                {
                    apt.UniqueId = row[0].ToString();
                    apt.Start = Convert.ToDateTime(row[1]);
                    apt.End = Convert.ToDateTime(row[2]);
                    apt.Subject = row[3].ToString();
                    apt.Location = row[4].ToString();
                    /*
                    apt.Body = "Start - " + apt.Start.ToString() + "\n" +
                                "End - " + apt.End.ToString() + "\n" +
                                "Ward - " + apt.Location.ToString();
                     */
                }
            }
            return apt;
        }

        public static int GenerateNextEid()
        {
            List<object[]> results = Database.Select("SELECT Auto_increment FROM information_schema.tables WHERE table_name= 'Schedule' AND table_schema = DATABASE();");
            return Convert.ToInt32(results[0][0]);
        }
        
        #endregion
    }
}
