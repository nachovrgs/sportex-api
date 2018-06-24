using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace sportex.api.domain
{
    public class EventInvitation
    {

        #region PROPERTIES

        public int Type { get; set; }
        public string Message { get; set; }
        public int IdProfileInvites { get; set; }
        [ForeignKey("IdProfileInvites")]
        public StandardProfile ProfileInvites { get; set; }
        public int IdProfileInvited { get; set; }
        [ForeignKey("IdProfileInvited")]
        public StandardProfile ProfileInvited { get; set; }
        public int EventID { get; set; }
        [ForeignKey("EventID")]
        public Event EventInvited { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        #endregion

        #region CONSTRUCTORS
        public EventInvitation()
        {
            this.Type = 0;
            this.Message = "";
            this.IdProfileInvites = 0;
            this.IdProfileInvited = 0;
            this.ProfileInvites = null;
            this.ProfileInvited = null;
            this.EventID = 0;
            this.EventInvited = null;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }

        public EventInvitation(InvitationType type, string msg, int idInvites, int idInvited, int eventId)
        {
            this.Type = (int)type;
            this.Message = msg;
            this.IdProfileInvites = idInvites;
            this.IdProfileInvited = idInvited;
            this.ProfileInvites = null;
            this.ProfileInvited = null;
            this.EventID = eventId;
            this.EventInvited = null;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }

        public EventInvitation(int type, string msg, int idInvites, int idInvited, int eventId)
        {
            this.Type = type;
            this.Message = msg;
            this.IdProfileInvites = idInvites;
            this.IdProfileInvited = idInvited;
            this.ProfileInvites = null;
            this.ProfileInvited = null;
            this.EventID = eventId;
            this.EventInvited = null;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        #endregion

        public enum InvitationType
        {
            Default
        };

    }
}
