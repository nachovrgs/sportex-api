using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace sportex.api.domain
{
    public class EventParticipant
    {
        #region PROPERTIES
        public int StandardProfileID { get; set; }
        [ForeignKey("StandardProfileID")]
        public StandardProfile ProfileParticipant { get; set; }
        public int EventID { get; set; }
        [ForeignKey("EventID")]
        public Event EventParticipates { get; set; }
        public int Type { get; set; }
        public int Order { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        #endregion

        #region CONSTRUCTORS
        public EventParticipant()
        {
            this.StandardProfileID = 0;
            this.ProfileParticipant = null;
            this.EventID = 0;
            this.EventParticipates = null;
            this.Type = 0;
            this.Order = 0;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }

        public EventParticipant(int profileId, int eventId, ParticipationType parType, int ord)
        {
            this.StandardProfileID = profileId;
            this.ProfileParticipant = null;
            this.EventID = eventId;
            this.EventParticipates = null;
            this.Type = (int)parType;
            this.Order = ord;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        #endregion


        public enum ParticipationType
        {
            Default,
            Starting,
            Substitute,
            Admin,
            Watcher 
        };
    }
}
