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
        private string _RoomNo;
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

        public string RoomNo
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

        public Room(string RoomNo)
        {
            _RoomNo = RoomNo;
            Select();
        }

        public Room(string RoomNo, string WardName,int FloorNo)
        {
            _FloorNo = FloorNo;
            _RoomNo = RoomNo;
            _WardName = WardName;
        }
        #endregion

        #region DatabaseCalls

        public bool Select()
        {
            List<object[]> SingleRoomRow = Database.Select("SELECT * from Room WHERE room_no = '" + _RoomNo + "'");
            if (SingleRoomRow != null && SingleRoomRow.Count > 0)
            {
                foreach (object[] row in SingleRoomRow)
                {
                    _RoomNo = row[0].ToString();
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
                    "VALUES ('" + _RoomNo + "'," + _FloorNo + ",'" + _WardName + "')");
        }

        public bool Update()
        {
                return Database.Update("Update Room Set floor_no = " + _FloorNo + ", " +
                   "ward = '" + _WardName + "' WHERE room_no = '" + _RoomNo + "'");
        }

        public bool Delete()
        {
            return Database.Delete("DELETE FROM Room WHERE room_no = '" + _RoomNo + "'");
        }
        
        #endregion
#region List Functions
        public static List<Room> GetRooms(String WardName)
        {
            List<object[]> rooms = Database.Select("Select * FROM Room WHERE ward ='" + WardName + "'");
            List<Room> getrooms = new List<Room>();
            foreach (object[] row in rooms)
            {
                Room newRoom = new Room(row[0].ToString(),row[1].ToString(),Convert.ToInt32(row[2]));
                getrooms.Add(newRoom);
            }
            return getrooms;
        }
#endregion

    }
}
