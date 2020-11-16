using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Money
    {
        public decimal VMBalance { get; set; } = 0M;
        public decimal UserBalance { get; set; }
        public int Quarters { get; private set; }
        public int Dimes { get; private set; }
        public int Nickels { get; private set; }
        ReportFiles auditFile = new ReportFiles();
        public void MakeChange (decimal userBalance)
        {
            decimal changeMade = userBalance;
            
            while (userBalance >= 0.25M)
            {
                userBalance -= 0.25m;
                Quarters++;
            }
            while (userBalance >= 0.10M)
            {
                userBalance -= 0.10m;
                Dimes++;
            }
            while (userBalance >= 0.05M)
            {
                userBalance -= 0.05m;
                Nickels++;
            }
            
            UserBalance = userBalance;
            auditFile.AuditLogTransaction("GIVE CHANGE", changeMade, UserBalance);
            Console.Clear();
            Console.WriteLine($"Your change is {Quarters} quarter(s), {Dimes} dime(s), and {Nickels} nickel(s).");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
            
        }
        public void FeedMoney()
        {
            Console.Clear();
            while (true)
            {
                int deposit;
                Console.WriteLine("How much money would you like to deposit (up to $100, whole dollar amounts only)?");
                bool isValidUserInput = Int32.TryParse(Console.ReadLine(), out deposit);
                    if (isValidUserInput && deposit > 0 && deposit < 101)
                    {
                        UserBalance += deposit;
                        Console.Clear();
                        Console.WriteLine("Your current balance is $" + UserBalance);
                        auditFile.AuditLogTransaction("FEED MONEY", deposit, UserBalance);
                        break;
                    }
                    else
                        Console.WriteLine("Invalid input, please select again");
            }
        }
    }
}
