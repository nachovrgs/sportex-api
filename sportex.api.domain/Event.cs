using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace sportex.api.domain
{
    public class Event
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public int StandardProfileID { get; set; }
        [ForeignKey("StandardProfileID")]
        public StandardProfile CreatorProfile { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public int EventType { get; set; }
        public DateTime? StartingTime { get; set; }
        public int LocationID { get; set; }
        [ForeignKey("LocationID")]
        public Location Location { get; set; }
        public bool IsPublic { get; set; }
        public int MaxStarters { get; set; }
        public int MaxSubs { get; set; }
        public int CountStarters { get; set; }
        public int CountSubs { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        [InverseProperty("EventParticipates")]
        public ICollection<EventParticipant> EventParticipates { get; set; }

        #endregion

        #region CONSTRUCTORS
        public Event()
        {
            this.StandardProfileID = 0;
            this.CreatorProfile = null;
            this.EventName = "";
            this.Description = "";
            this.EventType = 0;
            this.StartingTime = DateTime.Now;
            this.MaxStarters = 10;
            this.MaxSubs = 10;
            this.CountStarters = 0;
            this.CountSubs = 0;
            this.LocationID = 0;
            this.Location = null;
            this.IsPublic = false;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        #endregion

        public bool FullStarters()
        {
            return CountStarters == MaxStarters;
        }
        public bool FullSubs()
        {
            return CountSubs == MaxSubs;
        }
    }
}
