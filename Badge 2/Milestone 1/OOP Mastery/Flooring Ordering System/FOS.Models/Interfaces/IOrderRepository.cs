using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Models.Interfaces
{
   public interface IOrderRepository
    {
        List<Order> LoadOrders(string myString);
        void SaveAOrder(DateTime datetime, List<Order> orders);
        List<Order> DisplayOrder(DateTime orderDate);
        Order DisplayOrderByOrderNumber(DateTime orderDate, int orderNumber);
        Order AddOrder(DateTime orderDate, string custName, string state, string prodType, string area);
        Order EditOrder(DateTime orderDate, string orderNumber);
        Order RemoveOrder(DateTime orderDate, int orderNumber);
        Order ReplaceOrderEdit(DateTime orderDate, int orderNumber, Order order);
    }
}
