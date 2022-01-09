using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreModel
{
    public class ReviewsModel
    {
        public int ReviewId { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string CreatedAt { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
