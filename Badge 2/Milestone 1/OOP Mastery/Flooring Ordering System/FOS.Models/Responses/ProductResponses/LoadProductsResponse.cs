using FOS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Models.Responses
{
    public class LoadProductsResponse : Response
    {
        public List<Product> Product { get; set; }
    }
}
