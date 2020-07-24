using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.DataEntity
{
    public class RatingDetails
    {
        
        public string Rating { get; set; }
        public  string Reviews { get; set; }
        public string RestaurantName { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
    }
}
