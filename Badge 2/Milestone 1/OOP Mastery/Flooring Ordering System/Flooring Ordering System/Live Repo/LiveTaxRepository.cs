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
    public class LiveTaxRepository : ITaxRepository
    {
        private static string path = @"C:\Data\Taxes.txt";
        List<Tax> taxes = new List<Tax>();
        public LiveTaxRepository()
        {
            taxes = LoadTax();
        }
        public List<Tax> LoadTax()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Tax tax = new Tax();
                    string[] columns = line.Split(',');
                    tax.StateAbbreviation = columns[0];
                    tax.StateName = columns[1];
                    tax.TaxRate = decimal.Parse(columns[2]);
                    taxes.Add(tax);
                }
            }
            return taxes;
        }

        public void SaveTax(Tax tax)
        {
            taxes.Add(tax);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("StateAbbreviation,StateName,TaxRate");
                while (sw != null)
                {
                    foreach (var _tax in taxes)
                    {
                        sw.WriteLine(_tax.StateAbbreviation + "," + _tax.StateName + "," + _tax.TaxRate);
                    }

                }
            }
        }
    }
}
    
            
