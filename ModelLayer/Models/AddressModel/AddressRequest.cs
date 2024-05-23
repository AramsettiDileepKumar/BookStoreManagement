using ModelLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models.AddressModel
{
    public class AddressRequest
    {
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public AddressType type { get; set; }
    }
}
