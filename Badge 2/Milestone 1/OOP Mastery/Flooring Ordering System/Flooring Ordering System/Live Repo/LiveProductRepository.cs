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
    public class LiveProductRepository : IProductRepository
    {
        private const string path = @"C:\Data\Products.txt";
        List<Product> products = new List<Product>();
        public LiveProductRepository()
        {
            products = LoadProduct();
        }
        public List<Product> LoadProduct()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Product product = new Product();
                    string[] columns = line.Split(',');
                    product.ProductType = columns[0];
                    product.CostPerSquareFoot = decimal.Parse(columns[1]);
                    product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);
                    products.Add(product);
                }
            }
            return products;
        }

        public void SaveProduct(Product product)
        {
            products.Add(product);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("ProductType, CostPerSquareFoot, LaborCostPerSquareFoot");
                while (sw != null)
                {
                    foreach (var _product in products)
                    {
                        sw.WriteLine(_product.ProductType + "," + _product.CostPerSquareFoot + "," + _product.LaborCostPerSquareFoot);
                    }
                    
                }
            }
        }
    }
}
