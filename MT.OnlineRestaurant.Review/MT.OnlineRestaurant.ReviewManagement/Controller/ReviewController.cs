using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

using MT.OnlineRestaurant.BuisnessEntities;
using MT.OnlineRestaurant.BusinessLayer;

namespace MT.OnlineRestaurant.ReviewManagement.Controller
{
    [Produces("application/json")]
    [Route("api")]
    public class ReviewController : ControllerBase
    {
        private readonly IRestaurantBuisness business_Repo;
        public ReviewController(IRestaurantBuisness _business_Repo)
        {
            business_Repo = _business_Repo;
        }

        [HttpGet]
        [Route("RestaurantRatings")]
        public IActionResult GetResturantRatingDetail([FromQuery] string RestaurantName)
        {
            IQueryable<RatingandReviewDetails> restaurantratingDetails;
            restaurantratingDetails = business_Repo.GetRestaurantRating(RestaurantName);
            if (restaurantratingDetails != null)
            {
                return this.Ok(restaurantratingDetails);
            }
            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);
        }

        [HttpPost]
        [Route("PostRatingandReviews")]
        public async Task<IActionResult> Post([FromBody] RatingandReviewDetails ratingandReviewDetails)
        {
            if (Request.Headers.ContainsKey("CustomerId"))
            {
                var userId = HttpContext.Request.Headers["CustomerId"].ToString();
                ratingandReviewDetails.TblCustomerId = int.Parse(userId);
            }

           
                if (await Task<bool>.Run(() => business_Repo.PostRatingandReviews(ratingandReviewDetails)))
                {
                    return this.Ok("Reviews Recorded");
                }
                else
                {
                    return this.BadRequest("Failed to record reviews, please try again after sometime");
                }

            return this.Ok(false);
            
        }


        [HttpPut]
        [Route("UpdateRatingandReviews")]
        public async Task<IActionResult> Put([FromBody] RatingandReviewDetails ratingandReviewDetails)
        {
            if (Request.Headers.ContainsKey(""))
            {
                var userId = HttpContext.Request.Headers["CustomerId"].ToString();
                ratingandReviewDetails.TblCustomerId = int.Parse(userId);
            }

            
                if (await Task<bool>.Run(() => business_Repo.UpdateRatingandReviews(ratingandReviewDetails)))
                {
                    return this.Ok("Updated");
                }

                return this.BadRequest("Failed to update , please try again later");       



            return this.Ok(false);
        }
    }
}
