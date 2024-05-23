using ModelLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models.AddressModel
{
    public class AddressWithUserDetails
    {
        public int addressId { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public AddressType type { get; set; }
        public int userId { get; set; }
        public string UserPhone { get; set; }
        public string Name { get; set; }
    }
}
