using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public class EventInvitationDTO
    {
        #region PROPERTIES

        public int Type { get; set; }
        public string Message { get; set; }
        public int IdProfileInvites { get; set; }
        public StandardProfileDTO ProfileInvites { get; set; }
        public int IdProfileInvited { get; set; }
        public StandardProfileDTO ProfileInvited { get; set; }
        public int EventID { get; set; }
        public EventDTO EventInvited { get; set; }
        #endregion

        #region CONSTRUCTORS
        public EventInvitationDTO()
        {
            try
            {
                this.Type = 0;
                this.Message = "";
                this.IdProfileInvites = 0;
                this.IdProfileInvited = 0;
                this.ProfileInvites = null;
                this.ProfileInvited = null;
                this.EventID = 0;
                this.EventInvited = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EventInvitationDTO(EventInvitation invitation)
        {
            try
            {
                this.Type = invitation.Type;
                this.Message = invitation.Message;
                this.IdProfileInvites = invitation.IdProfileInvites;
                this.IdProfileInvited = invitation.IdProfileInvited;
                this.ProfileInvites = new StandardProfileDTO(invitation.ProfileInvites);
                this.ProfileInvited = new StandardProfileDTO(invitation.ProfileInvited);
                this.EventID = invitation.EventID;
                this.EventInvited = new EventDTO(invitation.EventInvited);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public EventInvitation MapFromDTO()
        {
            try
            {
                return new EventInvitation(this.Type, this.Message, this.IdProfileInvites, this.IdProfileInvited, this.EventID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
