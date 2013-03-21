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
        private string wardslug;
        private string priority;
        private int treatmentId;
        private string treatment;

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

        public string Ward
        {
            get
            {
                return wardslug;
            }
            set
            {
                wardslug=value;
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

        public string Treatment
        {
            get
            {
                return treatment;
            }
            set
            {
                treatment = value;
            }
        }
        #endregion 

        #region Constructors
        public WaitlistedPatient(int waitlistId, int pid, string fname, string lname, string priority, int rtid, string treatment)
        {
            this.waitlistId = waitlistId;
            this.pid = pid;
            this.fname = fname;
            this.lname = lname;
            this.priority = priority;
            this.treatmentId = rtid;
            this.treatment = treatment;
        }
        //FOR EXISTING WAITLIST ITEMS
        public WaitlistedPatient(int rtid)
        {
            treatmentId = rtid; 
            Select();
        }

        public WaitlistedPatient()
        {
            // TODO: Complete member initialization
        }
        #endregion

        #region Functions

        //FOR EXISTING WAITLIST ITEMS
        private void Select()
        {
            string query = "Select * from Waitlist where TreatmentID = " + treatmentId + ";";
            List<object[]> SingleRow = Database.Select(query);
            if (SingleRow != null && SingleRow.Count > 0)
            {
                foreach (object[] row in SingleRow)
                {
                    waitlistId = Convert.ToInt32(row[0]);
                    pid = Convert.ToInt32(row[1]);
                    wardslug = row[2].ToString();
                    priority = row[3].ToString();
                }
            }
        }

        public bool AssignPatientToBed(int bedId)
        {
            string query = "update Bed set pid = " + pid + ", state = 0 where bed_no = " + bedId + ";" +
                           "UPDATE ReceivesTreatment set treatmentStatus = 'Current' WHERE patient = " 
                           + pid + " AND rtid = " + treatmentId + ";";
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
            List<object[]> patients = Database.Select("SELECT waitlistId, pid, fname, lname, priority, treatmentID, treatment  from Waitlist, Patient, ReceivesTreatment where Waitlist.patientId = Patient.pid and ReceivesTreatment.rtid = Waitlist.treatmentID and Waitlist.wardName = '"+ ward + "' order by priority='Low', priority='Medium', priority='High', waitlistId;");
            List<WaitlistedPatient> getpatients = new List<WaitlistedPatient>();
            if (patients != null)
            foreach (object[] row in patients)
            {
                if (row.Length == 7)
                {
                    WaitlistedPatient newPatient = new WaitlistedPatient(Convert.ToInt32(row[0]), Convert.ToInt32(row[1]), row[2].ToString(), row[3].ToString(), row[4].ToString(), Convert.ToInt32(row[5]), row[6].ToString());
                    getpatients.Add(newPatient);
                }
            }

            return getpatients;
        }

        public static List<Bed> GetOpenBedsForWard(string ward)
        {
            List<object[]> beds = Database.Select("Select * FROM Bed WHERE state = 1 and ward = '" + ward + "' ORDER BY Bed_no");
            List<Bed> openbeds = new List<Bed>();
            if (beds != null)
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

        public static bool AddPatientToWaitlist(int pid, string ward, string priority, int rtid)
        {
            return Database.Insert("Insert into Waitlist (patientId, wardName, priority, treatmentID) values (" + pid + ", '" + ward + "', '" + priority + "', " + rtid + ")");
        }

        public static bool EditPatientInWaitlist(int pid, string ward, string priority, int rtid)
        {
            return Database.Update("Update Waitlist Set wardName='" + ward + "', priority='" + priority + "' WHERE treatmentID = " + rtid);
        }
        #endregion
    }
}
