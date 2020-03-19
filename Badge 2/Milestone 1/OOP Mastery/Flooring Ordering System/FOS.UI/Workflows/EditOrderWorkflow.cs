using FOS.BLL;
using System;
using FOS.Models.Responses;
using FOS.Tests;
using FOS.Models.Responses.OrderResponses;
using FOS.Models;
using FOS.Models.Helpers;
using Flooring_Ordering_System;

namespace FOS.UI
{
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager orderManager = OrderManagerFactory.Create();
            Order editedOrder = new Order();
            string answer = "";

            // input
            DateTime orderDate = ConsoleIO.GetListDate("Enter Order Date: ");
            int orderNumber = ConsoleIO.GetIntNumber("Enter Order Number: ");
            
            DisplayOrdersByOrderNumberResponse curOrder = orderManager.DisplayOrdersByOrderNumber(orderDate, orderNumber);

            editedOrder.OrderNumber = curOrder.Order.OrderNumber;

            Console.Clear();
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

            Console.WriteLine("Only customer name, state, product and area can be edited.");
            Console.WriteLine("Press enter to leave field in it's current state.");
            Console.WriteLine("Press any key to continue editing order displayed above.");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Customer Name: ");
            Console.WriteLine($"{curOrder.Order.CustomerName}");
            answer = Console.ReadLine();
            if (answer != "")
            {
                editedOrder.CustomerName = answer;
            }
            else
            {
                editedOrder.CustomerName = curOrder.Order.CustomerName;
            }
            Console.Clear();

            Console.WriteLine("State(initials): ");
            Console.WriteLine($"{curOrder.Order.State}");
            answer = Console.ReadLine();
            if (answer != "")
            {
                editedOrder.State = answer.ToUpper();
            }
            else
            {
                editedOrder.State = curOrder.Order.State.ToUpper();
            }
            Console.Clear();

            Console.WriteLine("Product Type: ");
            Console.WriteLine($"{curOrder.Order.ProductType}");
            answer = Console.ReadLine();
            if (answer != "")
            {
                editedOrder.ProductType = answer;
            }
            else
            {
                editedOrder.ProductType = curOrder.Order.ProductType;
            }
            Console.Clear();

            Console.WriteLine("Area in Sq. Ft:");
            Console.WriteLine($"{curOrder.Order.Area}");
            answer = Console.ReadLine();
            if (answer != "")
            {
                editedOrder.Area = decimal.Parse(answer);
            }
            else
            {
                editedOrder.Area = curOrder.Order.Area;
            }
            Console.Clear();

            editedOrder = orderManager.Calculate(editedOrder);
            Console.Clear();
            Console.WriteLine("Display Orders");
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"{editedOrder.OrderNumber}|{orderDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine($"{editedOrder.CustomerName}");
            Console.WriteLine($"{editedOrder.State}");
            Console.WriteLine($"Product: {editedOrder.ProductType}");
            Console.WriteLine($"Materials: {editedOrder.MaterialCost:c}");
            Console.WriteLine($"Labor: {editedOrder.LaborCost:c}");
            Console.WriteLine($"Tax: {editedOrder.Tax:c}");
            Console.WriteLine($"Total: {editedOrder.Total:c}");
            Console.WriteLine("---------------------------------");
            Console.Write("Is this edit correct?: ");

            answer = Console.ReadLine();
            if (answer.ToLower().Contains("y"))
            {
              orderManager.ReplaceEdit(orderDate, curOrder.Order.OrderNumber, editedOrder);
            }
            else
            {
                Console.WriteLine("Edit cancled");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}