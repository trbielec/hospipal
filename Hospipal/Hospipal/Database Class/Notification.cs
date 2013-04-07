using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows;

namespace Hospipal.Database_Class
{
    public class Notification
    {
        private string _Text;
        private DateTime _Now;
        private string _Role;
        private DateTime _Then;
        private string _TheirRole;


        #region Getters/Setters
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        public DateTime Then
        {
            get
            {
                return _Then;
            }
        }

        public string TheirRole
        {
            get
            {
                return _TheirRole;
            }
        }
        
        #endregion

        #region Constructors
        public Notification()
        {
            
        }
        public Notification(string text)
        {
            _Text = text;
        }
        #endregion

        #region Database Calls

        public void RetrieveNotification()
        {
            List<object[]> text = Database.Select("SELECT * from Notification order by ROW desc LIMIT 1;");
            if(text != null && text.Count > 0 )
            _Text = Convert.ToString(text.ElementAt(0).ElementAt(1)).ToUpper();
            _Then = (DateTime)text.ElementAt(0).ElementAt(2);
            
            string check_role = Convert.ToString(text.ElementAt(0).ElementAt(3));
            if (check_role == "")
                _TheirRole = "UNKNOWN";
            else
                _TheirRole = check_role;
            //MessageBoxResult result = MessageBox.Show("Retrieved value: " + _Text);
        }

        public void SendNotification()
        {
            _Now = DateTime.Now;
            _Role = Properties.Settings.Default.Role;
            //MessageBoxResult result = MessageBox.Show("Value to send to database: " + _Text);
            MySqlCommand notification = new MySqlCommand("Insert_Notification(@_Text,@_Now,@_Role);");
            notification.Parameters.AddWithValue("_Text", _Text);
            notification.Parameters.AddWithValue("_Now", _Now);
            notification.Parameters.AddWithValue("_Role", _Role);
            Database.Update(notification);
        }
        #endregion
    }
}
