using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreModel
{
    public class AddressModel
    {
        public string Name { get; set; }
        public long Mobile { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
    }
}
