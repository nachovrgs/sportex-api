using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public class PlayerReviewDTO
    {
        #region PROPERTIES
        public int Rate { get; set; }
        public string Message { get; set; }
        public int IdProfileReviews { get; set; }
        public StandardProfileDTO ProfileReviews { get; set; }
        public int IdProfileReviewed { get; set; }
        public StandardProfileDTO ProfileReviewed { get; set; }
        public int EventID { get; set; }
        public EventDTO EventReviewed { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        #endregion

        #region CONSTRUCTORS
        public PlayerReviewDTO()
        {
            try
            {
                this.Rate = 0;
                this.Message = "";
                this.IdProfileReviews = 0;
                this.IdProfileReviewed = 0;
                this.ProfileReviews = null;
                this.ProfileReviewed = null;
                this.EventID = 0;
                this.EventReviewed = null;
                this.Status = 1;
                this.CreatedOn = DateTime.Now;
                this.LastUpdate = this.CreatedOn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PlayerReviewDTO(PlayerReview review)
        {
            try
            {
                this.Rate = review.Rate;
                this.Message = review.Message;
                this.IdProfileReviews = review.IdProfileReviews;
                this.IdProfileReviewed = review.IdProfileReviewed;
                this.ProfileReviews = new StandardProfileDTO(review.ProfileReviews);
                this.ProfileReviewed = new StandardProfileDTO(review.ProfileReviewed);
                this.EventID = review.EventID;
                this.EventReviewed = new EventDTO(review.EventReviewed);
                this.Status = 1;
                this.CreatedOn = DateTime.Now;
                this.LastUpdate = this.CreatedOn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public PlayerReview MapFromDTO()
        {
            try
            {
                return new PlayerReview(this.Rate, this.Message, this.IdProfileReviews, this.IdProfileReviewed, this.EventID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
