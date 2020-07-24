using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.Repository
{
    public interface IReviewRespository
    {
        IQueryable<RatingDetails> GetRestaurantRating(string RestaurantName);
        //int PostRatingandReviews(TblRatingandReviews ratingandreviews);
        bool PostRatingandReviews(TblRatingandReviews ratingandreviews);

        bool UpdateRatingandReviews(TblRatingandReviews ratingandreviews);

    }
}
