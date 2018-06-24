using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public class EventParticipantDTO
    {
        #region PROPERTIES
        public int StandardProfileID { get; set; }
        public StandardProfileDTO ProfileParticipant { get; set; }
        public int EventID { get; set; }
        public int Type { get; set; }
        public int Order { get; set; }
        #endregion

        #region CONSTRUCTORS
        public EventParticipantDTO()
        {
            try
            {
                this.StandardProfileID = 0;
                this.ProfileParticipant = null;
                this.EventID = 0;
                this.Type = 0;
                this.Order = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EventParticipantDTO(EventParticipant participant)
        {
            try
            {
                this.StandardProfileID = participant.StandardProfileID;
                this.ProfileParticipant = new StandardProfileDTO(participant.ProfileParticipant);
                this.EventID = participant.EventID;
                this.Type = participant.Type;
                this.Order = participant.Order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
