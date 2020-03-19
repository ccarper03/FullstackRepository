using Flooring_Ordering_System;
using FOS.BLL;
using FOS.Models;
using FOS.Models.Helpers;
using FOS.Models.Models;
using FOS.Models.Responses;
using FOS.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FOS.UI
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager orderManager = OrderManagerFactory.Create();
            TaxRepository taxRepo = new TaxRepository();
            ProductRepository prodRepo = new ProductRepository();
            Order order = new Order();

            string custName;
            string state;
            string prodType;
            decimal area;
            
            // use order instead of declaring new variables
            // input
            Console.Clear();
            DateTime orderDate = ConsoleIO.GetDate("Enter Order Date: ");
            Console.Clear();
            custName = ConsoleIO.GetCustomerName("Customer Name: ");
            Console.Clear();
            state = ConsoleIO.GetState(taxRepo, "State: ");
            Console.Clear();
            prodType = ConsoleIO.GetProductType(prodRepo, "Choose a Product Type: ");
            Console.Clear();
            area = ConsoleIO.GetDecimalArea("Area: ");
            Console.Clear();

            order.ProductType = prodType;
            order.State = state;
            order.Area = area;

            order = orderManager.Calculate(order);

            // confirm input
            Console.Clear();
            Console.WriteLine("Confirm Order");
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"{orderDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine($"{custName}");
            Console.WriteLine($"{state}");
            Console.WriteLine($"Product: {prodType}");
            Console.WriteLine($"Area: {area}");
            Console.WriteLine($"Materials: {order.MaterialCost:c}");
            Console.WriteLine($"Labor: {order.LaborCost:c}");
            Console.WriteLine($"Tax: {order.Tax:c}");
            Console.WriteLine($"Total: {order.Total:c}");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Add this Order? ");
            string answer = Console.ReadLine();

            // if confirmed add order
            if (answer.ToLower() == "y" || answer.ToLower().Contains("yes"))
            {
                // Add order
                AddOrderResponse response = orderManager.AddOrder(orderDate, custName, state, prodType, area.ToString());
                if (response.Success)
                {
                    Console.Clear();
                    Console.WriteLine("Success, Order has been added.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("An error occured: ");
                    Console.WriteLine(response.Message);
                }
            }
            else // if not cancle
            {
                Console.Clear();
                Console.WriteLine("Order Cancled");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}