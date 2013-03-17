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
    class Notification
    {
        private string _Text;


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
            _Text = Convert.ToString(Database.Select("SELECT * from Notification;").ElementAt(0).ElementAt(1));
            //MessageBoxResult result = MessageBox.Show("Retrieved value: " + _Text);
        }

        public void SendNotification()
        {
            //MessageBoxResult result = MessageBox.Show("Value to send to database: " + _Text);
            MySqlCommand notification = new MySqlCommand("Update_Notification(@_Text);");
            notification.Parameters.AddWithValue("_Text", _Text);
            Database.Update(notification);
        }
        #endregion
    }
}
