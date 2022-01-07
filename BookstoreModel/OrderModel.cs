using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreModel
{
    public class OrderModel
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public int AddressId { get; set; }
    }
}
