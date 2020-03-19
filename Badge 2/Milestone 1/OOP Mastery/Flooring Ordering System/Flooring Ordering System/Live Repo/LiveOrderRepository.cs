using FOS.Models;
using FOS.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring_Ordering_System
{
    public class LiveOrderRepository : IOrderRepository
    {
        
        private const string path = @"C:\Data\Orders_";
        private List<Order> _orders = new List<Order>();
        public LiveOrderRepository()
        {
            
        }
        public LiveOrderRepository(DateTime datetime)
        {
            //_orders = DisplayOrder(datetime);
        }
        public Order AddOrder(DateTime orderDate, string custName, string state, string prodType, string area)
        {
            LiveProductRepository products = new LiveProductRepository();
            LiveTaxRepository taxes = new LiveTaxRepository();
            string date = path + orderDate.Month.ToString("00") + orderDate.Day.ToString("00") + orderDate.Year.ToString("0000") + ".txt";
            List<Order> _orders = LoadOrders(date);
            decimal tempTaxRateDecimal = 0M;
            decimal tempCostPerSquareFoot = 0M;
            decimal tempLaborCostPerSquareFoot = 0M;
            decimal parseArea = decimal.Parse(area);
            int orderCount = _orders.Count;
            orderCount++;

            foreach (var product in products.LoadProduct()) // remove these load calls, redundent 
            {
                if (prodType.ToLower() == product.ProductType.ToLower())
                {
                    tempLaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
                    tempCostPerSquareFoot = product.CostPerSquareFoot;
                }
            }
            foreach (var tax in taxes.LoadTax())// remove these load calls, redundent
            {
                if (state.ToUpper() == tax.StateName.ToUpper() || state.ToUpper() == tax.StateAbbreviation.ToUpper())
                {
                    tempTaxRateDecimal = tax.TaxRate;
                }
            }

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
                Tax = ((parseArea * tempCostPerSquareFoot) + (parseArea * tempCostPerSquareFoot) * (tempTaxRateDecimal / 100)),
                Total = (parseArea * tempCostPerSquareFoot) + (parseArea * tempCostPerSquareFoot) + ((parseArea * tempCostPerSquareFoot) + (parseArea * tempCostPerSquareFoot) * (int)(tempTaxRateDecimal / 100))
            };
            _orders.Add(order);
            SaveAOrder(orderDate,_orders);
            return order;
        }

        public List<Order> DisplayOrder(DateTime orderDate)
        {
            _orders = new List<Order>();
            string date = path + orderDate.Month.ToString("00") + orderDate.Day.ToString("00") + orderDate.Year.ToString("0000")+".txt";
            return _orders = LoadOrders(date);
        }
        public Order DisplayOrderByOrderNumber(DateTime orderDate, int orderNumber)
        {
            _orders = new List<Order>();
            string date = path + orderDate.Month.ToString("00") + orderDate.Day.ToString("00") + orderDate.Year.ToString("0000") + ".txt";
             _orders = LoadOrders(date);
            var orderQuery = _orders.Find(x => x.OrderNumber == orderNumber);
            return orderQuery;
        }

        public Order EditOrder(DateTime orderDate, string orderNumber)
        {
            IEnumerable<Order> orderQuery =
               from order in _orders
               where order.OrderNumber == int.Parse(orderNumber)
               select order;

            return orderQuery.FirstOrDefault();
        }

        public Order ReplaceOrderEdit(DateTime orderDate,int orderNumber,Order myOrder)
        {
            string date = path + orderDate.Month.ToString("00") + orderDate.Day.ToString("00") + orderDate.Year.ToString("0000") + ".txt";
            _orders = new List<Order>();
            _orders = LoadOrders(date);
            IEnumerable<Order> orderQuery =
               from order in _orders
               where order.OrderNumber == orderNumber
               select order;
            _orders.Remove(orderQuery.FirstOrDefault());
            _orders.Add(myOrder);
            SaveAOrder(orderDate, _orders);
            return myOrder;
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


        public Order RemoveOrder(DateTime orderDate, int orderNumber)
        {
            _orders = new List<Order>();
            string date = path + orderDate.Month.ToString("00") + orderDate.Day.ToString("00") + orderDate.Year.ToString("0000") + ".txt";
            _orders = LoadOrders(date);
            IEnumerable<Order> orderQuery =
               from order in _orders
               where order.OrderNumber == orderNumber
               select order;
            _orders.Remove(orderQuery.FirstOrDefault());
            SaveAOrder(orderDate, _orders);
            return orderQuery.FirstOrDefault();
        }

        public void SaveAOrder(DateTime orderdate, List<Order> orders)
        {
            string date = path + orderdate.Month.ToString("00") + orderdate.Day.ToString("00") + orderdate.Year.ToString("0000") + ".txt";
            if (!File.Exists(date))
            {
                File.Create(path);
                using (StreamWriter sw = new StreamWriter(date))
                {
                    sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                    foreach (var order in orders)
                    {
                        sw.WriteLine(order.OrderNumber + "," + order.CustomerName + "," + order.State + "," + order.TaxRate + "," + order.ProductType + "," + order.Area + "," + order.CostPerSquareFoot + "," + order.LaborCostPerSquareFoot + "," + order.MaterialCost + "," + order.LaborCost + "," + order.Tax.ToString(".00") + "," + order.Total.ToString(".00"));
                    }
                }
            }
            else
            {
                File.WriteAllText(date, string.Empty);
                using (StreamWriter sw = new StreamWriter(date))
                {
                    sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                    foreach (var order in orders)
                    {
                        sw.WriteLine(order.OrderNumber + "," + order.CustomerName + "," + order.State + "," + order.TaxRate + "," + order.ProductType + "," + order.Area + "," + order.CostPerSquareFoot + "," + order.LaborCostPerSquareFoot + "," + order.MaterialCost + "," + order.LaborCost + "," + order.Tax.ToString(".00") + "," + order.Total.ToString(".00"));
                    }
                }
            }
        }
    }
}
