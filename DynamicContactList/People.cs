using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicContactList
{
	class People
	{
        public string Nome { get; set; }
        public Phone[] phone { get; set; }

        public override string ToString()
        {
            string phones = "";
            foreach (Phone phn in phone)
                if (phn != null)
                    phones = phones + phn.ToString();

            return "\n>>DADOS DO CONTATO<<<\nNome:" + Nome + "\n" + phones;
        }
    }
}
