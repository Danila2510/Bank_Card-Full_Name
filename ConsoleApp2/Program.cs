using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        public class Bank_card
        {
            public delegate void Events();
            public string Imya { get; set; }
            public string Nomer { get; set; }
            public DateTime Termin { get; set; }
            public string Pinkod { get; set; }
            public double Kredit { get; set; }
            public double Balanc { get; private set; }

            public event Events Dob;
            public event Events Risov;
            public event Events Use_Bank;
            public event Events Zel;
            public event Events Pin_smena;
            public Bank_card(string imya, string nomer, DateTime terimn, double kredit)
            {
                Imya = imya;
                Nomer = nomer;
                Termin = terimn;
                Kredit = kredit;
                Balanc = 0;
            }
            public void Init()
            {
                Console.Write("Card number ");
                Nomer = Console.ReadLine();
                Console.Write("Full_Name ");
                Imya = Console.ReadLine();
                Console.Write("Card term [Format Day/Month/Year] ");
                string date = Console.ReadLine();
                Termin.ToString(date);
                Console.Write("PINKODE ");
                Pinkod = Console.ReadLine();
                Console.Write("Credit limit ");
                Kredit = Convert.ToDouble(Console.ReadLine());
                Console.Write("Balans ");
                Balanc = Convert.ToDouble(Console.ReadLine());
            }
            public void Repl()
            {
                Console.Write("Enter the amount to replenish your account  ");
                double sum = Convert.ToDouble(Console.ReadLine());
                Balanc += sum;
                Console.WriteLine($"Your current balance  {Balanc}");
                Dob?.Invoke();
            }
            public void Pull()
            {
                Console.Write("Enter how much money you want to withdraw ");
                double buf = Convert.ToDouble(Console.ReadLine());
                if (Balanc < buf || Balanc == 0)
                {
                    Console.WriteLine($"Not enough money");
                    Console.Write("Enter the amount you want to withdraw from the credit limit  ");
                    double sum = Convert.ToDouble(Console.ReadLine());
                    if (Kredit == 0 && Kredit < sum)
                    {
                        Console.WriteLine($"You have spent the entire credit limit ");
                        return;
                    }
                    Kredit -= sum;
                }
                else
                {
                    Balanc -= buf;
                    Console.WriteLine($"Your current balance = {Balanc}");
                    Risov?.Invoke();
                }
                if (Balanc < 0)
                {
                    Console.WriteLine($"You used credit money =  {Balanc * -1}.");
                    Use_Bank?.Invoke();
                }
            }
            public void Targ()
            {
                Console.Write("Enter your goal amount  ");
                double targ = Convert.ToDouble(Console.ReadLine());

                if (Balanc == targ || Balanc > targ)
                {
                    Console.WriteLine($"You have reached the goal \n Goal {targ}\n Your Balance {Balanc}");
                    return;
                }
                Console.WriteLine($"You didn't reach the goal \n Goal {targ}\n Your Balance {Balanc}");
            }
            public void ChangePin()
            {
                Console.WriteLine($"Your current password  {Pinkod}");
                Console.Write($"Enter a new password  ");
                Pinkod = Console.ReadLine();
            }

        }
        static void Main(string[] args)
        {
            Bank_card card = new Bank_card("Дмитрий", "4242 5123 5216 8621", DateTime.Now.AddYears(2), 2000);
            card.Dob += Added;
            card.Risov += OnFundsSpent;
            card.Use_Bank += UsedCredit;
            card.Zel += TargetReached;
            card.Pin_smena += PinChanged;
            while (true)
            {
                Console.WriteLine(" 1) Top up your account \n 2) Withdraw money \n 3) Check balance \n 4)Change PIN\n 5) Exit");
                Console.Write("Select an action: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        card.Repl();
                        break;
                    case 2:
                        card.Pull();
                        break;
                    case 3:
                        card.Targ();
                        break;
                    case 4:
                        card.ChangePin();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("\nYou entered an invalid value");
                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                }
            }
        }
        static void Added() => Console.WriteLine("\nAccount successfully replenished\n");
        static void OnFundsSpent() => Console.WriteLine("\nMoney has been debited from your account\n");
        static void UsedCredit() => Console.WriteLine("\nYou have used the entire credit limit\n");
        static void TargetReached() => Console.WriteLine("\nYou have reached the goal\n");
        static void PinChanged() => Console.WriteLine("\nPIN has been changed\n");
    }
}