using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Hospipal.Database_Class
{
    class Bed
    {
        public enum States { Occupied, Available, Maintainence };
        private string _bedNo;
        private States _state;
        private int _pid;
        private string _roomNo;
        private string _assigningNurse;

        

      
        #region Getters/Setters 

        public string bedNo
        {
            get
            {
                return _bedNo;
            }
            set
            {
                _bedNo = value;
            }
        }

        public States state
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public int pid
        {
            get
            {
                return _pid;
            }
            set
            {
                _pid = value;
            }
        }

        public string roomNo
        {
            get
            {
                return _roomNo;
            }
            set
            {
                _roomNo = value;
            }
        }

        public string assigningNurse
        {
            get
            {
                return _assigningNurse;
            }
            set
            {
                _assigningNurse = value;
            }
        }

        #endregion

        #region Constructors

        public Bed()
        {

        }

        public Bed(string bedNo, States state, int pid, string roomNo, string assigningNurse)
        {
            _bedNo =  bedNo;
            _state = state;
            _pid =  pid;
            _roomNo =  roomNo;
            _assigningNurse =  assigningNurse;
        }

        #endregion

        #region Database Functions

        public bool Select()
        {
            List<object[]> SingleRow = Database.Select("SELECT * from Bed WHERE bed_no = " + _bedNo);
            if (SingleRow != null && SingleRow.Count > 0)
            {
                foreach (object[] row in SingleRow)
                {
                    _bedNo = row[0].ToString();
                    _state = (States) Convert.ToInt32(row[1]);
                    _pid = Convert.ToInt32(row[2]);
                    _roomNo = row[3].ToString();
                    _assigningNurse = row[4].ToString();
                }
                return true;
            }
            return false;
        }


        public bool Insert(string roomNo)
        {
            return Database.Insert("Insert into Beds (bed_No,state,pid,roomNo,assigningNurse)" +
                    "VALUES ('" + _bedNo + "'," + _state + "," + _pid + ", '" + _roomNo + "', '" + _assigningNurse + "')");
        }

        public bool Delete(string roomNo)
        {
            return Database.Delete("DELETE * FROM Beds WHERE bed_No = " + _bedNo);
        }

        public static List<Bed> GetRooms(int RoomNo)
        {
            List<object[]> rooms = Database.Select("Select * FROM Bed WHERE roomno ='" + RoomNo + "'");
            List<Bed> getbeds = new List<Bed>();
            foreach (object[] row in rooms)
            {
                Bed newBed = new Bed(row[0].ToString(), (States)Convert.ToInt32(row[1].ToString()), Convert.ToInt32(row[2]), row[3].ToString(), row[4].ToString());
                getbeds.Add(newBed);
            }
            return getbeds;
        }
    }

        #endregion

}
