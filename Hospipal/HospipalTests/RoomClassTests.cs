using System;
using NUnit.Framework;
using Hospipal.Database_Class;

namespace HospipalTests
{
    [TestFixture]
    public class RoomClassTests
    {
        private Room room;

        [SetUp]
        public void setup()
        {
            room = new Room(1, "TestRoom", 1);
            room.Insert();
        }

        [TearDown]
        public void teardown()
        {
            room.Delete();
        }

        [TestCase]
        public void TestUpdateRoom()
        {
            room.FloorNo = -1;
            Assert.True(room.Update());

            room.RoomNo = 1;
            Assert.True(room.Update());
        }

        [TestCase]
        public void TestInsertRoom()
        {
            Room testRoom = new Room(1, "TestWard", 1);
            Assert.True(testRoom.Insert());
            testRoom.Delete();
        }

        [TestCase]
        public void TestDeleteRoom()
        {
            Room testRoom = new Room(1, "TestWard", 1);
            testRoom.Insert();
            Assert.True(testRoom.Delete());
        }

        [TestCase]
        public void TestRoomSelect()
        {
            Room testroom = new Room(-1, null);
            Assert.False(testroom.Select());

            Assert.True(room.Select());
        }

        [TestCase]
        public void TestGetRoomsWithInvlaidWardName()
        {
            Assert.IsEmpty(Room.GetRooms(null));
        }
    }
}
