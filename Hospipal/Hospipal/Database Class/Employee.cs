using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    class Employee
    {
        private int _eid;
        private string _fname;
        private string _lname;
        private string _specialty;

        public Employee()
        {
            _fname = "";
            _lname = "";
            _specialty = "General";
            GenerateEid();
        }

        public Employee(int eid, string fname, string lname, string specialty)
        {
            _eid = eid; 
            _fname = fname;
            _lname = lname;
            _specialty = specialty; 
        }

        public int GetEid()
        {
            return _eid;
        }

        public void SetName(string first, string last)
        {
            _fname = first;
            _lname = last; 
        }

        public List<string> GetName()
        {
            List<string> list = new List<string>();
            list.Add(_fname);
            list.Add(_lname);
            return list;
        }

        public void SetSpecialty(string spec)
        {
            _specialty = spec; 
        }

        public string GetSpecialty()
        {
            return _specialty;
        } 

        private void GenerateEid()
        {
            List<object[]> obj = Database.Select("SELECT eid FROM ismacaul_HospiPal.Employee");
            _eid = obj.Count() + 1;
        }
    }
}
