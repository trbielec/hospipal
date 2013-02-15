using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal
{
    class Class_PatientsList
    {
        public Class_PatientsList()
        {
            ObservableCollection<Class_Patients> PatientsList = new ObservableCollection<Class_Patients>();

            PatientsList.Add(new Class_Patients("what what", 1));
        }

    }
}
