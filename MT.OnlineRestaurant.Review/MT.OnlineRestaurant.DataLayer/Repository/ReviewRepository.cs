using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.Repository
{
    public class ReviewRepository : IReviewRespository
    {
        private readonly RestaurantManagementContext db;
        public ReviewRepository(RestaurantManagementContext connection)
        {
            db = connection;
        }
        public IQueryable<RatingDetails> GetRestaurantRating(string restaurantname)
        {
            List<RatingDetails> ratingdetails = new List<RatingDetails>();
            try
            {

                if (db != null)
                {

                    var ratingandreviews = (from rating in db.TblRatingandReviews
                                            join restaurant in db.TblRestaurant
                                            on rating.TblRestaurantId equals restaurant.Id
                                            where restaurant.Name == restaurantname
                                            select new RatingDetails
                                            {
                                                Rating = rating.Rating,
                                                Reviews = rating.Reviews,
                                                RestaurantName = restaurant.Name
                                            }
                                            ).ToList();
                    foreach (var rating in ratingandreviews)
                    {
                        RatingDetails ratingdetail = new RatingDetails
                        {
                            Rating = rating.Rating,
                            Reviews = rating.Reviews,
                            RestaurantName = rating.RestaurantName,

                        };
                        ratingdetails.Add(ratingdetail);
                    }

                }
                return ratingdetails.AsQueryable();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public bool PostRatingandReviews(TblRatingandReviews ratingandreviews)
        {
            try
            {
                if (ratingandreviews!=null)
                {
                    ratingandreviews.UserCreated = ratingandreviews.TblCustomerId.HasValue ? ratingandreviews.TblCustomerId.Value : 1;
                    ratingandreviews.UserModified = ratingandreviews.TblCustomerId.HasValue ? ratingandreviews.TblCustomerId.Value : 1;
                    ratingandreviews.RecordTimeStamp = DateTime.Now;
                    db.Set<TblRatingandReviews>().Add(ratingandreviews);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

      public bool UpdateRatingandReviews(TblRatingandReviews ratingandreviews)
        {
            TblRatingandReviews ratingandreviewentity = null;
                //db.Set<TblRatingandReviews>().Where(tto => tto.Id == ratingandreviews.Id).FirstOrDefault();

            if (ratingandreviews != null)
            {
                ratingandreviewentity = ratingandreviews;
                ratingandreviews.UserCreated = ratingandreviews.TblCustomerId.HasValue ? ratingandreviews.TblCustomerId.Value : 1;
                ratingandreviews.UserModified = ratingandreviews.TblCustomerId.HasValue ? ratingandreviews.TblCustomerId.Value : 1;
                ratingandreviewentity.RecordTimeStamp = DateTime.Now;

                db.TblRatingandReviews.Update(ratingandreviewentity);
                db.SaveChanges();

                return true;
            }

            return false;
        }

    }
}
