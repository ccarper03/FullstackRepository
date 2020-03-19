using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Models.Responses
{
    public class DisplayOrdersResponse : Response
    {
        public List<Order> Order { get; set; }
    }
}
