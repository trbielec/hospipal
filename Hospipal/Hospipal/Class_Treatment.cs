using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal
{
    class Class_Treatment
    {
        int TreatmentID;
        string TreatmentType;
        int DateMonth;
        int DateDay;
        int DateYear;
        int treatTime;
        string treatmentNotes;

        public Class_Treatment()
        {
        }

        public Class_Treatment(int id, string type, int month, int day, int year, int time, string notes)
        {
            this.TreatmentID = id;
            this.TreatmentType = type;
            this.DateMonth = month;
            this.DateDay = day;
            this.DateYear = year;
            this.treatTime = time;
            this.treatmentNotes = notes;
        }
    }
}
