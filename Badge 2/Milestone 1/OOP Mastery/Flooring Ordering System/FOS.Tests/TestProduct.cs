using FOS.BLL;
using FOS.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Tests
{
    [TestFixture]
    public class TestProduct
    {
        [Test]
        public void CanLoadProductTestData()
        {
            OrderManager manager = OrderManagerFactory.Create();
            LoadProductsResponse response = manager.LoadProducts();

            Assert.AreEqual(response.Product[0].ProductType, "Carpet");
            Assert.AreEqual(response.Product[0].CostPerSquareFoot, 2.25M);
            Assert.AreEqual(response.Product[0].LaborCostPerSquareFoot,2.10M);

            Assert.AreEqual(response.Product[1].ProductType, "Laminate");
            Assert.AreEqual(response.Product[1].CostPerSquareFoot, 1.75M);
            Assert.AreEqual(response.Product[1].LaborCostPerSquareFoot, 2.10M);
            
            Assert.AreEqual(response.Product[2].ProductType, "Tile");
            Assert.AreEqual(response.Product[2].CostPerSquareFoot, 3.50M);
            Assert.AreEqual(response.Product[2].LaborCostPerSquareFoot, 4.15M);
            
            Assert.AreEqual(response.Product[3].ProductType, "Wood");
            Assert.AreEqual(response.Product[3].CostPerSquareFoot, 5.15M);
            Assert.AreEqual(response.Product[3].LaborCostPerSquareFoot, 4.75M);

            Assert.IsNotNull(response.Product);
            Assert.IsTrue(response.Success);
        }
    }
}
