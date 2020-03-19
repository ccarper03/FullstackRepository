using FOS.Models.Interfaces;
using FOS.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring_Ordering_System
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> products = new List<Product>();
        public ProductRepository()
        {
            products = new List<Product>();
            products.Add(new Product() { ProductType = "Carpet", CostPerSquareFoot = 2.25M, LaborCostPerSquareFoot = 2.10M });
            products.Add(new Product() { ProductType = "Laminate", CostPerSquareFoot = 1.75M, LaborCostPerSquareFoot = 2.10M });
            products.Add(new Product() { ProductType = "Tile", CostPerSquareFoot = 3.50M, LaborCostPerSquareFoot = 4.15M });
            products.Add(new Product() { ProductType = "Wood", CostPerSquareFoot = 5.15M, LaborCostPerSquareFoot = 4.75M });
        }

        public List<Product> LoadProduct()
        {
            return products;
        }
    }
}
