using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;
using Telerik.Windows.Controls.ScheduleView;
using System.Collections.ObjectModel;
using Hospipal;

namespace HospipalTests
{

    [TestFixture]
    public class ScheduleClassTests
    {
        [SetUp]
        public void SetDB()
        {
            Database.useTestDB();
        }

        [TestCase]
        public void TestScheduleInsert()
        {
            Schedule s = new Schedule(Schedule.GenerateNextEid(), new DateTime(), new DateTime(), 1, "STW");
            Assert.True(s.Insert());

            s.Delete();
        }

        [TestCase]
        public void TestScheduleUpdate()
        {
            Schedule s = new Schedule(Schedule.GenerateNextEid(), new DateTime(), new DateTime(), 1, "STW");
            s.Insert();

            s.Start_time = new DateTime(2014, 10, 2);
            Assert.True(s.Update());

            s.Delete();
        }

        [TestCase]
        public void TestScheduleDelete()
        {
            Schedule s = new Schedule(Schedule.GenerateNextEid(), new DateTime(), new DateTime(), 1, "STW");
            s.Insert();

            Assert.True(s.Delete());
        }

        [TestCase]
        public void TestScheduleSelectForAllAppointments()
        {
            Schedule s = new Schedule(Schedule.GenerateNextEid(), new DateTime(), new DateTime(), 1, "STW");
            s.Insert();

            ObservableCollection<Appointment> appts = Schedule.Select();

            Assert.True(appts.Count >= 1);

            s.Delete();
        }

        [TestCase]
        public void TestScheduleCheckforInsertConflicts()
        {
            Schedule s1 = new Schedule(Schedule.GenerateNextEid(), new DateTime(2013, 4, 1, 11, 0, 0), new DateTime(2013, 4, 1, 12, 30, 0), 1, "A");
            s1.Insert();

            Schedule overlapAfterS1 = new Schedule(Schedule.GenerateNextEid(), new DateTime(2013, 4, 1, 12, 0, 0), new DateTime(2013, 4, 1, 13, 0, 0), 2, "B");
            Assert.False(overlapAfterS1.CheckforInsertConflicts());

            Schedule overlapBeforeS1 = new Schedule(Schedule.GenerateNextEid(), new DateTime(2013, 4, 1, 10, 0, 0), new DateTime(2013, 4, 1, 12, 0, 0), 2, "B");
            Assert.False(overlapBeforeS1.CheckforInsertConflicts());

            Schedule insideS1 = new Schedule(Schedule.GenerateNextEid(), new DateTime(2013, 4, 1, 11, 30, 1), new DateTime(2013, 4, 1, 12, 0, 0), 2, "B");
            Assert.False(insideS1.CheckforInsertConflicts());

            Schedule containsS1 = new Schedule(Schedule.GenerateNextEid(), new DateTime(2013, 4, 1, 10, 0, 0), new DateTime(2013, 4, 1, 13, 0, 0), 2, "B");
            Assert.False(containsS1.CheckforInsertConflicts());

            Schedule noS1Overlap = new Schedule(Schedule.GenerateNextEid(), new DateTime(2013, 4, 1, 13, 0, 0), new DateTime(2013, 4, 1, 14, 0, 0), 2, "B");
            Assert.True(noS1Overlap.CheckforInsertConflicts());
        }

        [TestCase]
        public void TestScheduleGenerateNextSID()
        {
            List<object[]> obj = Database.Select("SELECT Auto_increment FROM information_schema.tables WHERE table_name= 'Schedule'AND table_schema = DATABASE();");
            int next = Convert.ToInt32(obj[0][0]);
            Assert.True(Schedule.GenerateNextEid() == next);
        }

        #region Get/Set Tests
        [TestCase]
        public void TestGetSetSid()
        {
            Schedule s = new Schedule();
            s.Sid = 1;
            Assert.True(s.Sid == 1);
        }

        [TestCase]
        public void TestGetSetStartTime()
        {
            Schedule r = new Schedule();
            DateTime d = new DateTime(2013, 2, 3);
            r.Start_time = d;
            Assert.True(r.Start_time == d);
        }

        [TestCase]
        public void TestGetSetEndTime()
        {
            Schedule r = new Schedule();
            DateTime d = new DateTime(2013, 2, 3);
            r.End_time = d;
            Assert.True(r.End_time == d);
        }

        [TestCase]
        public void TestGetSetEmployee()
        {
            Schedule r = new Schedule();
            r.Employee = 1;
            Assert.True(r.Employee == 1);
        }

        [TestCase]
        public void TestGetSetWard()
        {
            Schedule r = new Schedule();
            r.Ward = "UTW";
            Assert.True(r.Ward == "UTW");
        }
        #endregion
    }
}
