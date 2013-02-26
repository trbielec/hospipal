using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    class Room
    {
        private int _FloorNo;
        private int _RoomNo;
        private string _WardName;

        
        #region Getters/Setters
        
        public int FloorNo
        {
            get
            {
                return _FloorNo;
            }
            set
            {
                _FloorNo = value;
            }
        }

        public int RoomNo
        {
            get
            {
                return _RoomNo;
            }
            set
            {
                    _RoomNo = value;
            }
        }

        public String WardName 
        {
            get
            {
                return _WardName;
            }
            set
            {
                _WardName = value;
            }
        }

        #endregion

        #region Constructors

        public Room()
        {
        }

        public Room(int RoomNo)
        {
            _RoomNo = RoomNo;
            Select();
        }

        public Room(int FloorNo, int RoomNo, string WardName)
        {
            _FloorNo = FloorNo;
            _RoomNo = RoomNo;
            _WardName = WardName;
        }
        #endregion

        #region DatabaseCalls

        public bool Select()
        {
            List<object[]> SingleRoomRow = Database.Select("SELECT * from Room WHERE room_no = " + _RoomNo);
            if (SingleRoomRow != null && SingleRoomRow.Count > 0)
            {
                foreach (object[] row in SingleRoomRow)
                {
                    _RoomNo = Convert.ToInt32(row[0]);
                    _WardName = row[1].ToString();
                    _FloorNo = Convert.ToInt32(row[2]);
                }
                return true;
            }
            return false;
        }

        public bool Insert()
        {
                return Database.Insert("Insert into Room (room_no,floor_no,ward)" +
                    "VALUES (" + _RoomNo + "," + _FloorNo + ",'" + _WardName + "')");
        }

        public bool Update()
        {
                return Database.Update("Update Room Set floor_no = " + _FloorNo + ", " +
                   "ward = '" + _WardName + "' WHERE room_no = " + _RoomNo);;
        }

        public bool Delete()
        {
            return Database.Delete("DELETE * FROM Room WHERE room_no = " + _RoomNo);
        }
        #endregion

    }
}
