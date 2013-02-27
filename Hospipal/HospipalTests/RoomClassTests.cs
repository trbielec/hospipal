using System;
using NUnit.Framework;
using Hospipal.Database_Class;

namespace HospipalTests
{
    [TestFixture]
    public class RoomClassTests
    {
        private Room room;

        [TestCase]
        public void TestInsertUpdateDeleteRoom()
        {
            room = new Room("TestRoom", "TestWard", 1);
            Assert.True(room.Insert());
            Assert.False(room.Insert());

            room.WardName = "TestChangedWardName";
            Assert.True(room.Update());

            Assert.True(room.Delete());
            Assert.False(room.Delete());
        }

        [TestCase]
        public void TestRoomSelect()
        {
            room = new Room(null, null, 0);
            Assert.False(room.Select());

            room = new Room("TestRoom", "TestWard", 1);
            Assert.True(room.Insert());
            Assert.True(room.Select());

            Assert.True(room.Delete());
        }

        [TestCase]
        public void TestGetRooms()
        {
            Assert.IsEmpty(Room.GetRooms(null));
            Assert.IsNotEmpty(Room.GetRooms("Psychiatric"));
        }

    }
}
