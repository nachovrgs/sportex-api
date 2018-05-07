using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace sportex.api.domain
{
    public class PlayerReview
    {
        #region PROPERTIES
        public int Rate { get; set; }
        public string Message { get; set; }
        public int IdProfileReviews { get; set; }
        [ForeignKey("IdProfileReviews")]
        public StandardProfile ProfileReviews { get; set; }
        public int IdProfileReviewed { get; set; }
        [ForeignKey("IdProfileReviewed")]
        public StandardProfile ProfileReviewed{ get; set; }
        public int EventID { get; set; }
        [ForeignKey("EventID")]
        public Event EventReviewed { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        #endregion

        #region CONSTRUCTORS
        public PlayerReview()
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
        public PlayerReview(int rate, string msg, int idReviews, int idReviewed, int eventId)
        {
            this.Rate = rate;
            this.Message = msg;
            this.IdProfileReviews = idReviews;
            this.IdProfileReviewed = idReviewed;
            this.ProfileReviews = null;
            this.ProfileReviewed = null;
            this.EventID = eventId;
            this.EventReviewed = null;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        #endregion
    }
}
