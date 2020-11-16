using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class ReportFiles
    {
        public string auditFilePath = @"C:\Users\Student\workspace\vm-capstone\VendingMachine\Example Files\Log.txt";
        public string salesReportFilePath = @"C:\Users\Student\workspace\vm-capstone\VendingMachine\Example Files\SalesReport.txt";
        public void AuditLogTransaction(string auditText, decimal amount, decimal vmBalance)
        {
            using (StreamWriter sw = new StreamWriter(auditFilePath, true))
            {
                sw.WriteLine($"{DateTime.Now, -10} {auditText, -15} ${amount, -5} ${vmBalance, -5}");
            }
        }
        public void RunSalesReport(List<InventoryItem> vmItems, decimal vendingMachineBalance)
        {
            using (StreamWriter sw = new StreamWriter(salesReportFilePath, false))
            {
                foreach (InventoryItem inventoryItem in vmItems)
                {
                    sw.WriteLine(inventoryItem.Name + " | " + (5-inventoryItem.Quantity));
                }
                    sw.WriteLine("***TOTAL SALES*** " + "$" + vendingMachineBalance);
            }
        }
    }
}
