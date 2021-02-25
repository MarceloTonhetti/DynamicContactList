using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DynamicContactList
{
	class Program
	{
        static void Main(string[] args)
        {
            List<People> myContacts = new List<People>();
            ReadFile(myContacts);
            int op;

            do
            {
                op = Menu();
                switch (op)
                {
                    case 1:
                        myContacts = PushContact(myContacts);
                        break;
                    case 2:
                        PopContact(myContacts);
                        break;
                    case 3:
                        FindContact(myContacts);
                        break;
                    case 4:
                        PrintContactList(myContacts);
                        break;
                    case 5:
                        PrintContactByContact(myContacts);
                        break;
                    case 6:
                        QtdContacts(myContacts);
                        break;
                }
            } while (op != 0);

            Console.ReadKey();
        }

        static int Menu()
        {
            int op;

            Console.Clear();
            Console.WriteLine(">>>> Agenda de Contatos <<<<\n");
            Console.WriteLine("1 - Inserir Contato");
            Console.WriteLine("2 - Remover Contato");
            Console.WriteLine("3 - Localizar Contato");
            Console.WriteLine("4 - Imprimir Contatos");
            Console.WriteLine("5 - Impressão Contato por Contato");
            Console.WriteLine("6 - Quantidade de Contatos");
            Console.WriteLine("0 - Sair");
            Console.Write("\nEscolha uma opção do menu: ");
            op = int.Parse(Console.ReadLine());

            return op;
        }

        static List<People> PushContact(List<People> myContacts)
        {
            string addPhone;

            Console.Clear();
            Console.WriteLine(">>> Cadastrando contato\n");
            Console.WriteLine("Obs: maximo de 5 numeros de phone por contato.\n");
            Console.Write("Nome: ");
            string name = Console.ReadLine();

            List<Phone> phones = new List<Phone>();
            int i = 0;
            do
            {
                Console.Write("Adicionar numero de phone? s/n: ");
                addPhone = Console.ReadLine();
                if (addPhone.ToUpper() == "S")
                {
                    Console.Write("Tipo: ");
                    string tipo = Console.ReadLine();
                    Console.Write("DDD: ");
                    int ddd = int.Parse(Console.ReadLine());
                    Console.Write("Numero: ");
                    int numero = int.Parse(Console.ReadLine());

                    phones.Add( new Phone { Tipo = tipo, DDD = ddd, Numero = numero });
                    i++;
                }
            } while (addPhone.ToUpper() == "S" && i < 5);

            if (i == 5)
                Console.WriteLine("Limite de numeros do contato atingido.!!");

            myContacts.Add( new People { Nome = name, phone = phones.ToArray() });
            myContacts = myContacts.OrderBy(Cl => Cl.Nome).ToList();
            WriteFile(myContacts);
			Console.WriteLine("Contato cadastrado com sucesso!!!");
            Console.ReadKey();
            return myContacts;
        }

        static void PopContact(List<People> myContacts)
        {
            People foundPeople = new People();
            foundPeople = FindContact(myContacts, foundPeople);

            if (foundPeople != null)
            {
                if (myContacts.Remove(foundPeople))
                {
                    WriteFile(myContacts);
                    Console.WriteLine("Contato removido com sucesso!!!");
                    Console.ReadKey();
                }
            }
        }

        static People FindContact(List<People> myContacts, People foundPeople)
        {
            Console.Clear();
            Console.WriteLine(">>> Buscar contato");
            Console.Write("Nome: ");
            string name = Console.ReadLine();

            return foundPeople = myContacts.Find(o => o.Nome.Equals(name));
        }

        static void FindContact(List<People> myContacts)
        {
            Console.Clear();
            Console.WriteLine(">>> Buscar contato");
            Console.Write("Nome: ");
            string name = Console.ReadLine();

            People foundPeople = myContacts.Find(o => o.Nome.Equals(name));
            
            if(foundPeople != null)
			    Console.WriteLine(foundPeople.ToString());
            else
				Console.WriteLine("Contato não encontrado!!!");

            Console.ReadKey();
        }

        static void PrintContactList(List<People> myContacts)
        {
            myContacts.ForEach(i => Console.WriteLine(i));

            Console.ReadKey();
        }

        static void PrintContactByContact(List<People> myContacts)
        {
            int i;
            for (i = 0; i < myContacts.Count; i++)
            {
                Console.WriteLine(myContacts[i]);
                Console.WriteLine("Continuar? s/n: ");
                string choice = Console.ReadLine();
                if (choice.ToLower() != "s")
                    break;
            }

            if( i == myContacts.Count)
					Console.WriteLine(">>> Fim da lista de contatos.");

            Console.ReadKey();
        }

        static void QtdContacts(List<People> myContacts)
        {
			Console.WriteLine("Numeros de contatos cadastrados: " + myContacts.Count);
            Console.ReadKey();
        }

       static void WriteFile(List<People> myContacts)
        {
            string fileName = "contactList.txt";
            string filePath = "C:\\Users\\Marcelo Junior\\Desktop\\listaDeContatos\\"+ fileName;

            using (StreamWriter stream = new StreamWriter(filePath))
            {
				for (int i = 0; i < myContacts.Count; i++)
				{
                    stream.Write(myContacts[i].Nome + ";" + myContacts[i].phone.Length + ",");
					foreach (Phone phone in myContacts[i].phone)
                        stream.Write(phone.PhoneFields());
                    stream.WriteLine(";");
                }
            }
        }


        static void ReadFile(List<People> myContacts)
        {
            string fileName = "contactList.txt";
            string filePath = "C:\\Users\\Marcelo Junior\\Desktop\\listaDeContatos\\" + fileName;

            if (File.Exists(filePath))
                using (StreamReader stream = new StreamReader(filePath))
                {
                    string line;
                    string[] filds;
                    string[] phone;
                    List<Phone> phones;
                    People people;

                    while ((line = stream.ReadLine()) != null)
                    {
                        filds = line.Split(';');
                        phone = filds[1].Split(',');
                        phones = new List<Phone>();

					    for (int i = 1, y = 0; y < int.Parse(phone[0]); i+=3, y++)
					    {
                            phones.Add(new Phone() {Tipo = phone[i], DDD = int.Parse(phone[i+1]), Numero = int.Parse(phone[i+2]) });
					    }

                        people = new People()
                        { 
                            Nome = filds[0],
                            phone = phones.ToArray()
                        };

                        myContacts.Add(people);
                    }
                }
        }


        
    }
}
