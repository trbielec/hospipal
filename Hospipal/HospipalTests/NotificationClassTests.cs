﻿using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;
using Hospipal;

namespace HospipalTests
{
    [TestFixture]
    public class NotificationClassTests
    {
        [SetUp]
        public void SetDB()
        {
            Database.useTestDB();
        }
/*
        [TestCase]
        public void TestRetreiveNotification()
        {
            Notification n = new Notification();
            // Save old text if any exists
            n.RetrieveNotification();
            string oldText = n.Text;

            n.Text = "unit test";
            n.SendNotification();
            n.RetrieveNotification();
            
            Assert.True(n.Text.Equals("UNIT TEST"));

            n.Text = oldText;
            n.SendNotification();
        }

        [TestCase]
        public void TestSendNotification()
        {
            Notification n = new Notification();

            // Save old text if any exists
            n.RetrieveNotification();
            string oldText = n.Text;

            n.Text = "Test Text";
            n.SendNotification();
            n.Text.ToUpper();

            string s = Database.Select("SELECT * from Notification")[0][1].ToString();
            Assert.True(n.Text.Equals(s));

            n.Text = oldText;
            n.SendNotification();
        }
*/
        [TestCase]
        public void TestGetSetText()
        {
            Notification n = new Notification("Test");
            Assert.True(n.Text == "Test");
            n.Text = "T";
            Assert.True(n.Text == "T");
        }
    }
}
