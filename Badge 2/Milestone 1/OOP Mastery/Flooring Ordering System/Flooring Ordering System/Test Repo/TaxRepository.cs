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
    public class TaxRepository : ITaxRepository
    {
        public List<Tax> taxes = new List<Tax>();
        public TaxRepository()
        {
            taxes = new List<Tax>();
            taxes.Add(new Tax() { StateAbbreviation = "OH",StateName = "Ohio", TaxRate = 6.15M });
            taxes.Add(new Tax() { StateAbbreviation = "PA", StateName = "Pennsylvania", TaxRate = 6.75M });
            taxes.Add(new Tax() { StateAbbreviation = "MI", StateName = "Michigan", TaxRate = 5.75M });
            taxes.Add(new Tax() { StateAbbreviation = "IN", StateName = "Indiana", TaxRate = 6.00M });
        }
        public List<Tax> LoadTax()
        {
            return taxes;
        }
    }
}
