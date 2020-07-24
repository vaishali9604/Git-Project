using MT.OnlineRestaurant.BuisnessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public interface IRestaurantBuisness
    {
        IQueryable<RatingandReviewDetails> GetRestaurantRating(string restaurantName);
       // int PostRatingandReviews(RatingandReviewDetails ratingEntity);
        bool PostRatingandReviews(RatingandReviewDetails ratingEntity);

        bool UpdateRatingandReviews(RatingandReviewDetails ratingEntity);
    }
}
