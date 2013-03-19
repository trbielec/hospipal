using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    public class Room
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

        public string RoomString
        {
            get
            {
                if (_RoomNo.ToString().Length < 2)
                    return _WardName + "00" + _RoomNo.ToString();
                else
                    return _WardName + "0" + _RoomNo.ToString();
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

        public Room(int RoomNo, string WardSlug)
        {
            _RoomNo = RoomNo;
            _WardName = WardSlug;
            Select();
        }

        public Room(int RoomNo, string WardName,int FloorNo)
        {
            _FloorNo = FloorNo;
            _RoomNo = RoomNo;
            _WardName = WardName;
        }
        #endregion

        #region DatabaseCalls

        public bool Select()
        {
            List<object[]> SingleRoomRow = Database.Select("SELECT * from Room WHERE room_no = " + _RoomNo + " AND ward = '" + _WardName + "'");
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
                return Database.Update("Update Room set floor_no = " + _FloorNo + ", " +
                   "ward = '" + _WardName + "' WHERE room_no = " + _RoomNo + " AND ward = '" + _WardName + "'");
        }

        public bool Delete()
        {
            return Database.Delete("DELETE FROM Room WHERE room_no = " + _RoomNo + " AND ward = '" + _WardName + "'");
        }
        
        #endregion
#region List Functions
        public static List<Room> GetRooms(String WardName)
        {
            List<object[]> rooms = Database.Select("Select * FROM Room WHERE ward ='" + WardName + "'");
            List<Room> getrooms = new List<Room>();
            if (rooms != null)
            foreach (object[] row in rooms)
            {
                Room newRoom = new Room(Convert.ToInt32(row[0]),row[1].ToString(),Convert.ToInt32(row[2]));
                getrooms.Add(newRoom);
            }
            return getrooms;
        }
#endregion

    }
}
