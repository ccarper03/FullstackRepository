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
    public class TestTax
    {
        [Test]
        public void CanLoadFileTestData()
        {
            OrderManager manager = OrderManagerFactory.Create();
            LoadTaxesResponse response = manager.LoadTaxes();

            Assert.AreEqual(response.Tax[0].StateAbbreviation, "OH");
            Assert.AreEqual(response.Tax[0].StateName, "Ohio");
            Assert.AreEqual(response.Tax[0].TaxRate, 6.15M);

            Assert.AreEqual(response.Tax[1].StateAbbreviation, "PA");
            Assert.AreEqual(response.Tax[1].StateName, "Pennsylvania");
            Assert.AreEqual(response.Tax[1].TaxRate, 6.75m);

            Assert.AreEqual(response.Tax[2].StateAbbreviation, "MI");
            Assert.AreEqual(response.Tax[2].StateName, "Michigan");
            Assert.AreEqual(response.Tax[2].TaxRate, 5.75M);

            Assert.AreEqual(response.Tax[3].StateAbbreviation, "IN");
            Assert.AreEqual(response.Tax[3].StateName, "Indiana");
            Assert.AreEqual(response.Tax[3].TaxRate, 6.00M);

            Assert.IsNotNull(response.Tax);
            Assert.IsTrue(response.Success);
        }
    }
}
