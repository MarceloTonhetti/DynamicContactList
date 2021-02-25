using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicContactList
{
	class Phone
	{
		public int DDD { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; }

        public string PhoneFields()
        {
            return Tipo + "," + DDD + "," + Numero + ",";
        }

        public override string ToString()
        {
            return Tipo + "\t- " + DDD + "\t- " + Numero + "\n";
        }
	}
}
