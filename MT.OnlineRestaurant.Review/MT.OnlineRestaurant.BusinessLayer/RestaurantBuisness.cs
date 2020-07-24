using AutoMapper;
using MT.OnlineRestaurant.BuisnessEntities;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using MT.OnlineRestaurant.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class RestaurantBuisness : IRestaurantBuisness
    {
        IReviewRespository review_Repository;
        IMapper _mapper;
        private readonly string connectionstring;
        public RestaurantBuisness(IReviewRespository _reviewRepository, IMapper mapper)
        {
            review_Repository = _reviewRepository;
            _mapper = mapper;
        }
        public IQueryable<RatingandReviewDetails> GetRestaurantRating(string restaurantName)
        {
            IQueryable<RatingDetails> ratingDetails;
            List<RatingandReviewDetails> ratingandreviews = new List<RatingandReviewDetails>();
            try
            {
                ratingDetails = review_Repository.GetRestaurantRating(restaurantName);
                foreach (var rating in ratingDetails)
                {
                    RatingandReviewDetails ratingandreview = new RatingandReviewDetails
                    {
                        Rating = rating.Rating,
                        Reviews = rating.Reviews,
                        RestaurantName = rating.RestaurantName

                    };
                    ratingandreviews.Add(ratingandreview);
                }
                return ratingandreviews.AsQueryable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool PostRatingandReviews(RatingandReviewDetails ratingEntity)
        {
            try
            {
                TblRatingandReviews tblRatingandReviews;
                if (ratingEntity!=null)
                {
                    tblRatingandReviews = new TblRatingandReviews();
                    tblRatingandReviews.TblRestaurantId = ratingEntity.TblRestaurantId;
                    tblRatingandReviews.TblCustomerId = ratingEntity.TblCustomerId;
                    tblRatingandReviews.Rating = ratingEntity.Rating;
                    tblRatingandReviews.Reviews = ratingEntity.Reviews;
                    tblRatingandReviews.RecordTimeStampCreated = DateTime.Now;
                    return review_Repository.PostRatingandReviews(tblRatingandReviews);

                }
                return false;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool UpdateRatingandReviews(RatingandReviewDetails ratingEntity)
        {
            try
            {
                TblRatingandReviews tblRatingandReviews;
                if (ratingEntity != null)
                {
                    tblRatingandReviews = new TblRatingandReviews();
                    tblRatingandReviews.TblRestaurantId = ratingEntity.TblRestaurantId;
                    tblRatingandReviews.TblCustomerId = ratingEntity.TblCustomerId;
                    tblRatingandReviews.Rating = ratingEntity.Rating;
                    tblRatingandReviews.Reviews = ratingEntity.Reviews;
                    tblRatingandReviews.RecordTimeStampCreated = DateTime.Now;
                    tblRatingandReviews.Id = ratingEntity.Id;
                    return review_Repository.UpdateRatingandReviews(tblRatingandReviews);

                }
                return false;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
