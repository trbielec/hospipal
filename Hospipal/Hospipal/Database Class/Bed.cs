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
        private string _roomNo;
        private string _assigningNurse;

        

      
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
                if (_bedNo.ToString().Length < 2)
                    return _roomNo + "00" + _bedNo.ToString();
                else
                    return _roomNo + "0" + _bedNo.ToString();
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

        public Bed(int bedNo, States state, int pid, string roomNo, string assigningNurse)
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
                    _bedNo = Convert.ToInt32(row[0]);
                    _state = (States) Convert.ToInt32(row[1]);
                    _pid = Convert.ToInt32(row[2]);
                    _roomNo = row[3].ToString();
                    _assigningNurse = row[4].ToString();
                }
                return true;
            }
            return false;
        }


        public bool Insert()
        {
            return Database.Insert("Insert into Bed (bed_No,state,pid,roomNo,assigning_Nurse)" +
                    "VALUES (" + _bedNo + "," + (int)_state + "," + _pid + ", '" + _roomNo + "', '" + _assigningNurse + "')");
        }

        public bool Delete()
        {
            return Database.Delete("DELETE FROM Bed WHERE bed_No = " + _bedNo);
        }

        public static List<Bed> GetBeds(string RoomNo)
        {
            List<object[]> rooms = Database.Select("Select * FROM Bed WHERE roomno ='" + RoomNo + "' ORDER BY Bed_no");
            List<Bed> getbeds = new List<Bed>();
            foreach (object[] row in rooms)
            {
                Bed newBed = new Bed(Convert.ToInt32(row[0]), (States)Convert.ToInt32(row[1].ToString()), Convert.ToInt32(row[2]), row[3].ToString(), row[4].ToString());
                getbeds.Add(newBed);
            }
            return getbeds;
        }
    }

        #endregion

}
