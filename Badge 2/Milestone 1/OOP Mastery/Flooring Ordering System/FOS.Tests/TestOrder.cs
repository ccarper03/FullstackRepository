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
    public class TestOrder
    {

        [Test]
        public void CanLoadOrderTestData()
        {
            OrderManager manager = OrderManagerFactory.Create();
            DisplayOrdersResponse response = manager.DisplayOrders(new DateTime(2019,12,27));

            Assert.AreEqual(response.Order[0].OrderNumber,1);
            Assert.AreEqual(response.Order[0].CustomerName,"Wise");
            Assert.AreEqual(response.Order[0].State,"OH");
            Assert.AreEqual(response.Order[0].TaxRate,6.25M);
            Assert.AreEqual(response.Order[0].ProductType,"Wood");
            Assert.AreEqual(response.Order[0].Area,100.00M);
            Assert.AreEqual(response.Order[0].CostPerSquareFoot,5.15M);
            Assert.AreEqual(response.Order[0].LaborCostPerSquareFoot,4.75M);
            Assert.AreEqual(response.Order[0].MaterialCost,515.00M);
            Assert.AreEqual(response.Order[0].LaborCost,475.00M);
            Assert.AreEqual(response.Order[0].Tax,61.88M);
            Assert.AreEqual(response.Order[0].Total,1051.88M);

            Assert.AreEqual(response.Order[1].OrderNumber, 2);
            Assert.AreEqual(response.Order[1].CustomerName, "Carper");
            Assert.AreEqual(response.Order[1].State, "PA");
            Assert.AreEqual(response.Order[1].TaxRate, 6.00M);
            Assert.AreEqual(response.Order[1].ProductType, "Wood");
            Assert.AreEqual(response.Order[1].Area, 100.00M);
            Assert.AreEqual(response.Order[1].CostPerSquareFoot, 5.20M);
            Assert.AreEqual(response.Order[1].LaborCostPerSquareFoot, 5.00M);
            Assert.AreEqual(response.Order[1].MaterialCost, 520.00M);
            Assert.AreEqual(response.Order[1].LaborCost, 480.00M);
            Assert.AreEqual(response.Order[1].Tax, 60.00M);
            Assert.AreEqual(response.Order[1].Total, 1100.00M);

            Assert.IsNotNull(response.Order);
            Assert.IsTrue(response.Success);
        }

        [TestCase("Dec 27, 2019", 1, true)]
        [TestCase("Dec 27, 2019", 3, false)]
        public void RemoveOrderTestData(DateTime date, int orderNumber, bool expectedResult)
        {
            OrderManager manager = OrderManagerFactory.Create();
            RemoveOrderResponse response = manager.RemoveOrder(date, orderNumber);

            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}
