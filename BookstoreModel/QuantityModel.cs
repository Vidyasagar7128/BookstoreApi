using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreModel
{
    public class QuantityModel
    {
        public int CartId { get; set; }
        public int @Quantity { get; set; }
        public int @Price { get; set; }
        public int @UserId { get; set; }
    }
}
