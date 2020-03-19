using Flooring_Ordering_System;
using FOS.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Tests
{
    public class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString(); // AppSettings is a Dictionary

            switch (mode)
            {
                case "Test":
                    return new OrderManager(new OrderRepository(),new ProductRepository(),new TaxRepository());
                case "Prod":
                    return new OrderManager(new LiveOrderRepository(), new LiveProductRepository(), new LiveTaxRepository());
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }
        }
    }
}
