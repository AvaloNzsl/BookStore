using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.DataTransferObject
{
    public class ShippingDetailsDTO
    {
        public string Name { get; set; }
        public string Addres_1 { get; set; }
        public string Addres_2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
