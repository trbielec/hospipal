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
    class Schedule
    {
        private DateTime _start;
        private DateTime _end;
        private int _employee;
        private string _ward;


#region Getters/Setters


        public DateTime Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        public DateTime End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
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

        public Schedule(DateTime start, DateTime end, int employee, string ward)
        {
            
            _start = start;
            _end = end;
            _employee = employee;
            _ward = ward; 
        }

        #endregion


        #region Database functions

        public bool Insert()
        {
            MySqlCommand schedule = new MySqlCommand("Insert_Schedule(@start,@end,@employee,@ward);");

            schedule.Parameters.AddWithValue("start", _start);
            schedule.Parameters.AddWithValue("end", _end);
            schedule.Parameters.AddWithValue("employee", _employee);
            schedule.Parameters.AddWithValue("ward", _ward);
            return Database.Insert(schedule);
        }

        public bool Update()
        {
            MySqlCommand schedule = new MySqlCommand("Update_Schedule(@start,@end,@employee,@ward);");

            schedule.Parameters.AddWithValue("start", _start);
            schedule.Parameters.AddWithValue("end", _end);
            schedule.Parameters.AddWithValue("employee", _employee);
            schedule.Parameters.AddWithValue("ward", _ward);
            return Database.Update(schedule);
        }

        public bool Delete()
        {
           return Database.Delete("DELETE FROM Schedule WHERE start = " + _start + " AND end = " + _end + " AND employee = " + _employee);
        }

        public static ObservableCollection<Appointment> Select()
        {
            List<object[]> scheduleTable = Database.Select("SELECT * from Schedule");
            ObservableCollection<Appointment> allAppointments = new ObservableCollection<Appointment>();
            foreach (object[] row in scheduleTable)
            {
                Appointment newAppt = new Appointment() { 
                    Start = Convert.ToDateTime(row[0]),
                    End = Convert.ToDateTime(row[1]),
                    Subject = row[2].ToString(),
                    Body = row[3].ToString(),
                };

                allAppointments.Add(newAppt);               
            }
            return allAppointments;
        }
        
        #endregion
    }
}
