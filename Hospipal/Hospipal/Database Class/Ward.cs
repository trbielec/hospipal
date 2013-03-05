using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    public class Ward
    {
        
        private string _wardName;
        private string _slugName;


        #region Getters/Setters

        public string WardName
        {
            get
            {
                return _wardName;
            }
            set
            {
                _wardName = value;
            }
        }

        public string SlugName
        {
            get
            {
                return _slugName;
            }
            set
            {
                _slugName = value;
            }
        }

        #endregion

        #region Constructors

        public Ward()
        {
        }

        public Ward(string wardName)
        {
            _wardName = WardName;
            Select();
        }

        public Ward(string wardName, string slugName)
        {
           
            WardName = wardName;
            SlugName = slugName;
        }
        #endregion

        #region DatabaseCalls

        public bool Select()
        {
            List<object[]> SingleRoomRow = Database.Select("SELECT * from Ward WHERE ward_name = '" + _wardName + "'");
            if (SingleRoomRow != null && SingleRoomRow.Count > 0)
            {
                foreach (object[] row in SingleRoomRow)
                {
                    _wardName = row[0].ToString();
                    _slugName = row[1].ToString();
                }
                return true;
            }
            return false;
        }

        public bool Insert()
        {
            MySqlCommand ward = new MySqlCommand("Insert_Ward(@ward,@slug);");
            ward.Parameters.AddWithValue("ward", _wardName);
            ward.Parameters.AddWithValue("slug", _slugName);
            
            return Database.Insert(ward);
        }

        public bool Update()
        {
            return Database.Update("Update Ward Set ward_slug = '" + _slugName + "' WHERE ward_name = '" + _wardName + "'");
        }

        public bool Delete()
        {
            return Database.Delete("DELETE FROM Ward WHERE ward_name = '" + _wardName + "'");
        }

        #endregion
        #region List Functions
        public static List<Ward> GetWards()
        {
            List<object[]> wards = Database.Select("Select * FROM Ward");
            List<Ward> getwards = new List<Ward>();
            foreach (object[] row in wards)
            {
                Ward newWard = new Ward(row[0].ToString(), row[1].ToString());
                getwards.Add(newWard);
            }
            return getwards;
        }
        #endregion

    }
}
