using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Capstone
{
    class Program
    {
        private const string MAIN_MENU_OPTION_DISPLAY_ITEMS = "Display Vending Machine Items";
    	private const string MAIN_MENU_OPTION_PURCHASE = "Purchase";
        private const string MAIN_MENU_OPTION_EXIT = "Exit";
        private const string SECRET_MENU_OPTION_SALES_REPORT = "";
	    private readonly string[] MAIN_MENU_OPTIONS = { MAIN_MENU_OPTION_DISPLAY_ITEMS, MAIN_MENU_OPTION_PURCHASE , MAIN_MENU_OPTION_EXIT, SECRET_MENU_OPTION_SALES_REPORT}; //const has to be known at compile time, the array initializer is not const in C#
        VendingMachine vm = new VendingMachine();
        private readonly IBasicUserInterface ui = new MenuDrivenCLI();

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        public void Run()
        {
            Console.WriteLine("Vendo - Matic 600");
            Console.WriteLine("by Umbrella Corp.");
            vm.PopulateVendingMachine();
            while(true)
            {
                string selection = (string)ui.PromptForSelection(MAIN_MENU_OPTIONS);
                if (selection==MAIN_MENU_OPTION_DISPLAY_ITEMS)
                {
                    vm.DisplayItems();
                }
                else if (selection==MAIN_MENU_OPTION_PURCHASE)
                {
                    vm.PurchaseItemsSubmenu();
                }
                else if (selection == MAIN_MENU_OPTION_EXIT)
                {
                    break;
                }
                else if (selection == SECRET_MENU_OPTION_SALES_REPORT)
                {
                    vm.GenerateSalesReport();
                    break;
                }
            }
        }


    }
}
