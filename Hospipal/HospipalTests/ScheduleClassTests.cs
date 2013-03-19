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
        public void TestScheduleSelectForSingleAppointment()
        {
            int sid = Schedule.GenerateNextEid();
            Schedule s = new Schedule(sid, new DateTime(), new DateTime(), 1, "STW");
            s.Insert();

            Appointment a = Schedule.Select(sid.ToString());
            Assert.True(Convert.ToInt32(a.UniqueId) == sid);

            s.Delete();
        }

        [TestCase]
        public void TestScheduleGenerateNextSID()
        {
            List<object[]> obj = Database.Select("SELECT Auto_increment FROM information_schema.tables WHERE table_name= 'Schedule'AND table_schema = DATABASE();");
            int next = Convert.ToInt32(obj[0][0]);
            Assert.True(Schedule.GenerateNextEid() == next);
        }
    }
}
