using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApplication
{
    class Program
    {
        public static List<Client> Client = new List<Client>();
        public static List<Client> Balans = new List<Client>();
        static void Main(string[] args)
        {
            Balans.AddRange(Client);
            Client client = new Client();
            int num = 1;
            while (num != 0)
            {
                Console.Clear();
                TimerCallback kull = new TimerCallback(Clients);
                Timer x = new Timer(kull, Client, 0, 1000);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1.Insert");
                Console.WriteLine("2.Update");
                Console.WriteLine("3.Delete");
                Console.WriteLine("4.Select");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nВведите команду: ");
                var number = int.TryParse(Console.ReadLine(), out var Number);
                int id = 0;
                switch (Number)
                {
                    case 1:
                        Console.Write("Введите ID: ");
                        id = int.Parse(Console.ReadLine());
                        Console.Write("Введите баланс: ");
                        decimal balance = decimal.Parse(Console.ReadLine());
                        Thread novainsert = new Thread(new ThreadStart(() => { client.Insert(id, balance); }));
                        novainsert.Start();
                        break;
                    case 2:
                        Console.Write("Введите ID: ");
                        id = int.Parse(Console.ReadLine());
                        Console.Write("Введите новый баланс: ");
                        decimal Balans = decimal.Parse(Console.ReadLine());
                        Thread novauptade = new Thread(new ThreadStart(() => { client.Update(id, Balans); }));
                        novauptade.Start();
                        break;
                    case 3:
                        Console.Write("Введите ID: ");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Клиент удалилён");
                        Thread novadelete = new Thread(new ThreadStart(() => { client.Delete(id); }));
                        novadelete.Start();
                        break;
                    case 4:
                        Console.Write("Введите ID: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        Thread novaselect = new Thread(new ThreadStart(() => { client.Select(id); }));
                        novaselect.Start();
                        break;
                    default:
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Вы не правильно ввели команду!");
                        System.Console.ForegroundColor = ConsoleColor.White;
                        break;

                }
                // Console.WriteLine("\nНажмите ENTER");
                Console.ReadKey();
            }

        }
        static void Clients(object obj)
        {
            for (int i = 0; i < Client.Count; i++)
            {
                if (Client[i].Balance != Balans[i].Balance)
                {
                    if (Balans[i].Balance <= Client[i].Balance)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    string Difference = (Balans[i].Balance <= Client[i].Balance) ? $"+{Client[i].Balance - Balans[i].Balance}" : $"{Client[i].Balance - Balans[i].Balance}";
                    Console.WriteLine($"ID Клиента: {Client[i].ID}  Начальный баланс: {Balans[i].Balance} Новый баланс: {Client[i].Balance}  Разница: " + Difference);
                    Balans[i].Balance = Client[i].Balance;
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }

    class Client
    {
        public int ID { get; set; }
        public decimal Balance { get; set; }


        public void Insert(int Id, decimal balance)
        {
            this.ID = Id;
            this.Balance = balance;

            Program.Client.Add(new Client() { ID = Id, Balance = balance });
            Program.Balans.Add(new Client() { ID = Id, Balance = balance });
            Console.WriteLine("Клиент успешно добавлен!");
            Console.WriteLine("ID Клиента: " + Id);
            Console.WriteLine("\nНажмите ENTER");
            return;
            
        }

        public void Update(int id, decimal balance)
        {
            for (int i = 0; i < Program.Client.Count; )
            {
                Program.Client[i].Balance = balance;
                Console.WriteLine("\nНажмите ENTER");
                return;
            }
        }

        public void Delete(int id)
        {
            for (int i = 0; i < Program.Client.Count; i++)
            {
                if (id == Program.Client[i].ID)
                {
                    Program.Client.RemoveAt(i);
                    Console.WriteLine("\nНажмите ENTER");
                    return;
                }
            }
        }

        public void Select(int id)
        {
            for (int i = 0; i < Program.Client.Count; i++)
            {
                if (id == Program.Client[i].ID)
                {
                    Console.WriteLine("ID Клиента: " + Program.Client[i].ID);
                    Console.WriteLine("Баланс Клиента: " + Program.Client[i].Balance);
                    Console.WriteLine("\nНажмите ENTER");
                    return;
                }
            }
        }

    }
}