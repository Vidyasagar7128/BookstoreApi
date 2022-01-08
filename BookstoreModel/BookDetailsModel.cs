using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreModel
{
    public class BookDetailsModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<ImageModel> Images { get; set; }
        public string Author { get; set; }
        public double Rating { get; set; }
        public int Reviews { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
    }
}
