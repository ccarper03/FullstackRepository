using System;
using FOS.BLL;
using FOS.Models.Helpers;
using FOS.Models.Responses;
using FOS.Models.Responses.OrderResponses;
using FOS.Tests;
namespace FOS.UI
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            OrderManager orderManager = OrderManagerFactory.Create();
            DateTime orderDate = ConsoleIO.GetDate("Enter Order Date: ");
            int orderNumber = ConsoleIO.GetIntNumber("Enter Order Number: ");

            var curOrder = orderManager.DisplayOrdersByOrderNumber(orderDate, orderNumber);

            Console.Clear();
            Console.WriteLine("Remove Order");
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"{curOrder.Order.OrderNumber}|{orderDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine($"{curOrder.Order.CustomerName}");
            Console.WriteLine($"{curOrder.Order.State}");
            Console.WriteLine($"Product: {curOrder.Order.ProductType}");
            Console.WriteLine($"Materials: {curOrder.Order.MaterialCost:c}");
            Console.WriteLine($"Labor: {curOrder.Order.LaborCost:c}");
            Console.WriteLine($"Tax: {curOrder.Order.Tax:c}");
            Console.WriteLine($"Total: {curOrder.Order.Total:c}");
            Console.WriteLine("---------------------------------");
            Console.Write("Are you sure you want to delete?: ");
            string answer = Console.ReadLine();
            // if confirmed add order
            if (answer.ToLower() == "y" || answer.ToLower().Contains("yes"))
            {
                RemoveOrderResponse response = orderManager.RemoveOrder(orderDate, orderNumber);
                if (response.Success)
                {
                    Console.Clear();
                    Console.WriteLine("Success order deleted.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("An error occured: ");
                    Console.WriteLine(response.Message);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Delete Cancled.");
            }
                
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}