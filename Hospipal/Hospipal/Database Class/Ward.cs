using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal.Database_Class
{
    class Ward
    {



        public void AddWard(string ward_name, string ward_slug)
        {
            Database.Insert(@"INSERT INTO ismacaul_HospiPal.Wards (ward_name, ward_slug) 
                    VALUES ('" + ward_name + "', '" + ward_slug + "');");

        }
    }
}
