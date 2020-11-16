using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class VendingMachine
    {
        public List<InventoryItem> availableItems = new List<InventoryItem>();
        public Dictionary<string, InventoryItem> kvp = new Dictionary<string, InventoryItem>();
        public Money money = new Money();
        ReportFiles auditFile = new ReportFiles();
        public void PopulateVendingMachine()
        {
            using (StreamReader sr = new StreamReader(@"C:\Users\Student\workspace\vm-capstone\VendingMachine\Example Files\Inventory.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] lineSplit = line.Split("|");
                    decimal price = decimal.Parse(lineSplit[2]);
                    InventoryItem ii = new InventoryItem(lineSplit[0], lineSplit[1], price, lineSplit[3]);
                    availableItems.Add(ii);
                    kvp.Add(lineSplit[0], ii);
                }
            }
        }
        public void DisplayItems()
        {
            Console.Clear();
            Console.WriteLine($"{"Slot", -10} {"Name", -20} {"Price", -20} {"Type", -20} {"Quantity Available",-20}");
            foreach (InventoryItem item in availableItems)
            {
                item.InventoryRemaining();
                Console.WriteLine($"{item.SlotPosition, -10} { item.Name, -20} { item.Price, -20} { item.Type, -20} { item.Quantity, -20}");
            }
            Console.WriteLine("Or enter * to go back.");
        }
        //public VendingMachine ()
        public void PurchaseItemsSubmenu()
        {
            Console.Clear();
            bool stayOnSubMenu = true;
            while (stayOnSubMenu)
            {
                Console.WriteLine("1) Feed Money");
                Console.WriteLine("2) Select Product");
                Console.WriteLine("3) Finish Transaction");
                
                try
                {
                    int userInput = Int32.Parse(Console.ReadLine());
                    switch (userInput)
                    {
                        case 1:
                            Console.WriteLine("Your current balance is $" + money.UserBalance + "\n");
                            money.FeedMoney();
                            break;
                        case 2:
                            Console.WriteLine("Your current balance is $" + money.UserBalance + "\n");
                            PurchaseProduct();
                            break;
                        case 3:
                            Console.WriteLine("Your current balance is $" + money.UserBalance + "\n");
                            money.MakeChange(money.UserBalance);
                            stayOnSubMenu = false;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input, please select again");
                }
            }
        }
        public void PurchaseProduct()
        {
            DisplayItems();
            Console.WriteLine("What would you like to purchase? Please select a slot location.");
            bool onPurchaseMenu = true;
            while (onPurchaseMenu)
            {
                string userSelection = Console.ReadLine().ToUpper();

                if (userSelection == "*")
                {
                    Console.Clear();
                    break;
                }
                try
                {
                    InventoryItem item = kvp[userSelection];
                    if (item.Quantity > 0)
                    {
                        if (money.UserBalance >= item.Price)
                        {
                            Console.Clear();
                            item.Quantity--;
                            money.UserBalance -= item.Price;
                            money.VMBalance += item.Price;
                            Console.WriteLine(item.Name +" - " + item.SoundPlayed + " Enjoy your item!");
                            Console.WriteLine("Your remaining balance is $" + money.UserBalance + "\n");
                            auditFile.AuditLogTransaction(item.Name, item.Price, money.UserBalance);
                            onPurchaseMenu = false;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Insufficient funds. Please deposit more money or make another selection.");
                            onPurchaseMenu = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Item is sold out, please make another selection. Press any key to continue.");
                        Console.ReadLine();
                        //PurchaseProduct();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input, please select again.");
                    onPurchaseMenu = false;
                }
            }
        }
        public void GenerateSalesReport()
        {
            auditFile.RunSalesReport(availableItems, money.VMBalance);
        }
    }
}
