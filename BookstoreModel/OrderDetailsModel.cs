using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreModel
{
    public class OrderDetailsModel
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public long Mobile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public string Date { get; set; }
    }
}
