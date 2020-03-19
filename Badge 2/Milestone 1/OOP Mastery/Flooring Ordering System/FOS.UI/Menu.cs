using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.UI
{
    public class Menu
    {
        public static void Show()
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                DisplayMenu();
                continueRunning = ProcessChoice();
            }
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Flooring Program");
            Console.WriteLine("------------------------");
            Console.WriteLine("1. Display Orders");
            Console.WriteLine("2. Add an Order");
            Console.WriteLine("3. Edit an Order");
            Console.WriteLine("4. Remove an Order");
            Console.WriteLine("5. Quit");
            Console.Write("\nEnter selection: ");
        }
        private static bool ProcessChoice()
        {
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    DisplayOrdersWorkflow displayOrders = new DisplayOrdersWorkflow();
                    displayOrders.Execute();
                    break;
                case "2":
                    Console.WriteLine("Add an Order");
                    AddOrderWorkflow addOrder = new AddOrderWorkflow();
                    addOrder.Execute();
                    break;
                case "3":
                    Console.WriteLine("Edit an Order");
                    EditOrderWorkflow editOrder = new EditOrderWorkflow();
                    editOrder.Execute();
                    break;
                case "4":
                    Console.WriteLine("Remove an Order");
                    RemoveOrderWorkflow removeOrder = new RemoveOrderWorkflow();
                    removeOrder.Execute();
                    break;
                case "5":
                    return false;
            }
            return true;
        }
    }
}
