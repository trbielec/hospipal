using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;

namespace HospipalTests
{
    [TestFixture]
    public class RoomClassTests
    {
        [TestCase]
        public void TestSelectRoom()
        {
            Room room = new Room(100000, "UnitTestWard", 1);
            room.Insert();

            Assert.True(room.Select());

            room.Delete();
        }

        [TestCase]
        public void TestInsertRoom()
        {
            Room room = new Room(100000, "UnitTestWard", 1);
            Assert.True(room.Insert());

            room.Delete();  
        }

        [TestCase]
        public void TestUpdateRoom()
        {
            Room room = new Room(100000, "UnitTestWard", 1);
            room.Insert();

            room.FloorNo = 2;
            Assert.True(room.Update());

            room.Delete();
        }

        [TestCase]
        public void TestDeleteRoom()
        {
            Room room = new Room(100000, "UnitTestWard", 1);
            room.Insert();

            Assert.True(room.Delete());
        }

        [TestCase]
        public void TestGetRoomsWithInvalidWard()
        {
            List<Room> rooms = Room.GetRooms("UnitTestRoom1");
            Assert.True(rooms.Count == 0);
        }

        [TestCase]
        public void TestGetRoomsWithValidWard()
        {
            Room room = new Room(100000, "UnitTestWard", 1);
            room.Insert();

            List<Room> rooms = Room.GetRooms("UnitTestWard");
            Assert.True(rooms.Count == 1);

            room.Delete();
        }
    }
}
