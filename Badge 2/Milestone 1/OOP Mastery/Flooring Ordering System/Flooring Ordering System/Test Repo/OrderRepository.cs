using FOS.Models;
using FOS.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using FOS.Models.Models;

namespace Flooring_Ordering_System
{
    public class OrderRepository : IOrderRepository
    {
        private static List<Order> _orders;
        public OrderRepository()
        {
            if (_orders == null)
            {
                _orders = new List<Order>();
                _orders.Add(new Order() { OrderNumber = 1, CustomerName = "Wise", State = "OH", TaxRate = 6.25M, ProductType = "Wood", Area = 100.00M, CostPerSquareFoot = 5.15M, LaborCostPerSquareFoot = 4.75M, MaterialCost = 515.00M, LaborCost = 475.00M, Tax = 61.88M, Total = 1051.88M});
                _orders.Add(new Order() { OrderNumber = 2, CustomerName = "Carper", State = "PA", TaxRate = 6.00M, ProductType = "Wood", Area = 100.00M, CostPerSquareFoot = 5.20M, LaborCostPerSquareFoot = 5.00M, MaterialCost = 520.00M, LaborCost = 480.00M, Tax = 60.00M, Total = 1100.00M });
            }
        }

        public List<Order> DisplayOrder(DateTime orderDate)
        {
            return _orders;
        }
        public Order DisplayOrderByOrderNumber(DateTime orderDate, int orderNumber)
        {
            var orderQuery = _orders.Find(x => x.OrderNumber == orderNumber);
            return orderQuery;
        }

        public Order AddOrder(DateTime orderDate, string custName, string state, string prodType, string area)
        {
            ProductRepository products = new ProductRepository();
            TaxRepository taxes = new TaxRepository();
            decimal tempTaxRateDecimal = 0M;
            decimal tempCostPerSquareFoot = 0M;
            decimal tempLaborCostPerSquareFoot = 0M;
            int orderCount = _orders.Max(x => x.OrderNumber) + 1;

            foreach (var product in products.LoadProduct())
            {
                if (prodType.ToLower() == product.ProductType.ToLower())
                {
                    tempLaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
                    tempCostPerSquareFoot = product.CostPerSquareFoot;
                }
            }

            foreach (var tax in taxes.LoadTax())
            {
                if (state.ToUpper() == tax.StateName.ToUpper() || state.ToUpper() == tax.StateAbbreviation.ToUpper())
                {
                    tempTaxRateDecimal = tax.TaxRate;
                }
            }
            // if new order of the date create a new file
            decimal parseArea = decimal.Parse(area);
            
            Order order = new Order
            {
                OrderNumber = orderCount,
                CustomerName = custName,
                State = state,
                TaxRate = tempTaxRateDecimal,
                ProductType = prodType,
                Area = parseArea,
                CostPerSquareFoot = tempCostPerSquareFoot,
                LaborCostPerSquareFoot = tempLaborCostPerSquareFoot,
                MaterialCost = parseArea * tempCostPerSquareFoot,
                LaborCost = parseArea * tempCostPerSquareFoot,
                Tax = ((parseArea * tempCostPerSquareFoot) + (parseArea * tempCostPerSquareFoot) *(int)(tempTaxRateDecimal/100)),
                Total = (parseArea * tempCostPerSquareFoot) + (parseArea * tempCostPerSquareFoot) + ((parseArea * tempCostPerSquareFoot) + (parseArea * tempCostPerSquareFoot) * (int)(tempTaxRateDecimal / 100))
            };
            _orders.Add(order);
            return order;
        }

        public Order EditOrder(DateTime orderDate, string orderNumber)
        {
            IEnumerable<Order> orderQuery =
                from order in _orders
                where order.OrderNumber == int.Parse(orderNumber)
                select order;
            
            return orderQuery.FirstOrDefault();
        }

        public Order RemoveOrder(DateTime orderDate, int orderNumber)
        {
            foreach (var order in _orders)
            {
                if (orderNumber == order.OrderNumber)
                {
                    _orders.Remove(order);
                    return order;
                }
            }
            return null;
        }

        public List<Order> LoadOrders(string pathdate)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(pathdate))
                {
                    string headerLine = sr.ReadLine();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Order order = new Order();
                        string[] columns = line.Split(',');
                        order.OrderNumber = int.Parse(columns[0]);
                        order.CustomerName = columns[1];
                        order.State = columns[2];
                        order.TaxRate = decimal.Parse(columns[3]);
                        order.ProductType = columns[4];
                        order.Area = decimal.Parse(columns[5]);
                        order.CostPerSquareFoot = decimal.Parse(columns[6]);
                        order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                        order.MaterialCost = decimal.Parse(columns[8]);
                        order.LaborCost = decimal.Parse(columns[9]);
                        order.Tax = decimal.Parse(columns[10]);
                        order.Total = decimal.Parse(columns[11]);
                        _orders.Add(order);
                    }
                }
            }
            catch (IOException e)
            {

            }
            return _orders;
        }

        public void SaveAOrder(DateTime datetime,List<Order> orders)
        {
            //Order o;
            //IEnumerable<Order> orderQuery =
            //   from order in _orders
            //   where order.OrderNumber == myOrder.OrderNumber
            //   select order;
            //o = orderQuery.FirstOrDefault();

            //if (o != null)
            //{
            //    _orders.Remove(o);
            //}

            //o.OrderDate = myOrder.OrderDate;
            //o.OrderNumber = myOrder.OrderNumber;
            //o.CustomerName = myOrder.CustomerName;
            //o.State = myOrder.State;
            //o.TaxRate = myOrder.TaxRate;
            //o.ProductType = myOrder.ProductType;
            //o.Area = myOrder.Area;
            //o.CostPerSquareFoot = myOrder.CostPerSquareFoot;
            //o.LaborCostPerSquareFoot = myOrder.LaborCostPerSquareFoot;
            //o.MaterialCost = myOrder.MaterialCost;
            //o.LaborCost = myOrder.LaborCost;
            //o.Tax = myOrder.Tax;
            //o.Total = myOrder.Total;
            //_orders.Add(o);
        }

        public Order ReplaceOrderEdit(DateTime orderDate, int orderNumber, Order order)
        {
            throw new NotImplementedException();
        }
        // Return order by ID + date  create
    }
}




//public Order LoadOrder(string orderNumber)
//{
//    using (StreamReader sr = new StreamReader(path))
//    {
//        sr.ReadLine();
//        string line;
//        while ((line = sr.ReadLine()) != null)
//        {
//            Order order = new Order();
//            string[] columns = line.Split(',');
//            order.OrderNumber = int.Parse(columns[0]);
//            order.CustomerName = columns[1];
//            order.State = columns[2];
//            order.TaxRate = decimal.Parse(columns[3]);
//            order.ProductType = columns[4];
//            order.Area = decimal.Parse(columns[5]);
//            order.CostPerSquareFoot = decimal.Parse(columns[6]);
//            order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
//            order.MaterialCost = decimal.Parse(columns[8]);
//            order.LaborCost = decimal.Parse(columns[9]);
//            order.Tax = decimal.Parse(columns[10]);
//            order.Total = decimal.Parse(columns[11]);
//            orders.Add(order);
//        }

//        foreach (var _order in orders)
//        {
//            if (int.Parse(orderNumber) == _order.OrderNumber)
//            {
//                return _order;
//            }
//        }
//    }
//    return null;
//}

//public void SaveAOrder(Order order)
//{
//string typeString = "";

//orders[orders.FindIndex(x => x.OrderNumber.Equals(order.OrderNumber))].OrderNumber = order.OrderNumber;

//if (File.Exists(path))
//{
//    File.Delete(path);
//}
//using (StreamWriter sr = new StreamWriter(path))
//{
//    sr.WriteLine("AccountNumber,Name,Balance,Type");
//    foreach (var ac in orders)
//    {
//        sr.WriteLine(string.Format("{0},{1},{2},{3}", ac.OrderNumber, ac., ac.Balance, typeString));
//    }
//}
//}



// for production Make helper methods
// load orders (datetime), save orders(List of orders) use linq on all orders