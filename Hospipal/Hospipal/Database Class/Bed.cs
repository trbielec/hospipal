using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Hospipal.Database_Class
{
    public class Bed
    {
        public enum States { Occupied, Available, Maintainence };
        private int _bedNo;
        private States _state;
        private int _pid;
        private int _roomNo;
        private string _assigningNurse;
        private string _ward;





        #region Getters/Setters

        public int bedNo
        {
            get
            {
                return _bedNo;
            }
        }

        public string bed
        {
            get
            {
                Room getRoomName = new Room(_roomNo, _ward);
                if (_bedNo.ToString().Length < 2)
                    return getRoomName.RoomString + "00" + _bedNo.ToString();
                else
                    return getRoomName.RoomString + "0" + _bedNo.ToString();
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

        public int roomNo
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


        public string ward
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

        public Bed(int bedNo, States state, int pid, int roomNo, string assigningNurse, string ward)
        {
            _bedNo = bedNo;
            _state = state;
            _pid = pid;
            _roomNo = roomNo;
            _assigningNurse = assigningNurse;
            _ward = ward;
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
                    _bedNo = Convert.ToInt32(row[0]);
                    _state = (States)Convert.ToInt32(row[1]);
                    _pid = Convert.ToInt32(row[2]);
                    _roomNo = Convert.ToInt32(row[3].ToString());
                    _assigningNurse = row[4].ToString();
                    _ward = row[5].ToString();
                }
                return true;
            }
            return false;
        }


        public bool Insert()
        {
            return Database.Insert("Insert into Bed (bed_No,state,pid,roomNo,assigning_Nurse,ward)" +
                    "VALUES (" + _bedNo + "," + (int)_state + "," + _pid + ", " + _roomNo + ", '" + _assigningNurse + "','" + _ward + "')");
        }

        public bool Delete()
        {
            return Database.Delete("DELETE FROM Bed WHERE bed_No = " + _bedNo);
        }

        public static List<Bed> GetBeds(int RoomNo, string ward)
        {
            List<object[]> rooms = Database.Select("Select * FROM Bed WHERE roomno =" + RoomNo + " AND ward = '" + ward + "' ORDER BY Bed_no");
            List<Bed> getbeds = new List<Bed>();
            if (rooms != null)
                foreach (object[] row in rooms)
                {
                    Bed newBed = new Bed(Convert.ToInt32(row[0]), (States)Convert.ToInt32(row[1].ToString()), Convert.ToInt32(row[2]), Convert.ToInt32(row[3]), row[4].ToString(), row[5].ToString());
                    getbeds.Add(newBed);
                }
            return getbeds;
        }
    }

        #endregion

}
