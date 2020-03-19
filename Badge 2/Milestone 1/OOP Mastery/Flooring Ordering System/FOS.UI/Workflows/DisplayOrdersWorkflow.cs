using Flooring_Ordering_System;
using FOS.BLL;
using FOS.Models;
using FOS.Models.Helpers;
using FOS.Models.Responses;
using FOS.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.UI
{
    public class DisplayOrdersWorkflow
    {
        public void Execute()
        {
            OrderManager orderManager = OrderManagerFactory.Create();
            DateTime orderDate = ConsoleIO.GetListDate("Enter Order Date: ");
            DisplayOrdersResponse response = orderManager.DisplayOrders(orderDate);
            if (response.Success)
            {
                Console.Clear();
                Console.WriteLine("Display Orders");
                Console.WriteLine("---------------------------------");

                foreach (var _order in response.Order)
                {
                    Console.WriteLine($"{_order.OrderNumber}|{orderDate.ToString("MM/dd/yyyy")}");
                    Console.WriteLine($"{_order.CustomerName}");
                    Console.WriteLine($"{_order.State}");
                    Console.WriteLine($"Product: {_order.ProductType}");
                    Console.WriteLine($"Materials: {_order.MaterialCost:c}");
                    Console.WriteLine($"Labor: {_order.LaborCost:c}");
                    Console.WriteLine($"Tax: {_order.Tax:c}");
                    Console.WriteLine($"Total: {_order.Total:c}");
                    Console.WriteLine("---------------------------------");
                }
            }
            else
            {
                Console.WriteLine(response.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
