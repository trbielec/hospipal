using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospipal.Database_Class;

namespace Hospipal.Database_Class
{
    public class WaitlistedPatient
    {
        private int waitlistId;
        private int pid;
        private string fname;
        private string lname;
        private string priority;

        #region Getters/Setters

        public int Pid
        {
            get
            {
                return pid;
            }
            set
            {
                pid = value;
            }
        }

        public string Fname
        {
            get
            {
                return fname;
            }
            set
            {
                fname = value;
            }
        }

        public string Lname
        {
            get
            {
                return lname;
            }
            set
            {
                lname = value;
            }
        }

        public string Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }
        #endregion 

        #region Constructors
        public WaitlistedPatient(int waitlistId, int pid, string fname, string lname, string priority)
        {
            this.waitlistId = waitlistId;
            this.pid = pid;
            this.fname = fname;
            this.lname = lname;
            this.priority = priority;
        }
        #endregion

        #region Functions
        public bool AssignPatientToBed(int bedId)
        {
            string query = "update Bed set pid = " + pid + ", state = 0 where bed_no = " + bedId + ";";
            return Database.Update(query);
        }

        public bool RemovedPatientFromWaitlist()
        {
            string query = "delete from Waitlist where waitlistId = " + waitlistId + ";";
            return Database.Delete(query);
        }
        #endregion

        #region Static Methods
        public static List<WaitlistedPatient> GetWaitlistedPatientsForWard(string ward)
        {
            List<object[]> patients = Database.Select("SELECT waitlistId, pid, fname, lname, priority from Waitlist, Patient where Waitlist.patientId = Patient.pid and Waitlist.wardName = '"+ ward + "' order by priority='Low', priority='Medium', priority='High', waitlistId;");
            List<WaitlistedPatient> getpatients = new List<WaitlistedPatient>();
            foreach (object[] row in patients)
            {
                if (row.Length == 5)
                {
                    WaitlistedPatient newPatient = new WaitlistedPatient(Convert.ToInt32(row[0]), Convert.ToInt32(row[1]), row[2].ToString(), row[3].ToString(), row[4].ToString());
                    getpatients.Add(newPatient);
                }
            }

            return getpatients;
        }

        public static List<Bed> GetOpenBedsForWard(string ward)
        {
            List<object[]> beds = Database.Select("Select * FROM Bed WHERE state = 1 and ward = '" + ward + "' ORDER BY Bed_no");
            List<Bed> openbeds = new List<Bed>();
            foreach (object[] row in beds)
            {
                if (row.Length == 6)
                {
                    Bed newBed = new Bed(Convert.ToInt32(row[0]), (Bed.States)Convert.ToInt32(row[1].ToString()), Convert.ToInt32(row[2]), Convert.ToInt32(row[3]), row[4].ToString(), row[5].ToString());
                    openbeds.Add(newBed);
                }
            }

            return openbeds;
        }
        #endregion
    }
}
