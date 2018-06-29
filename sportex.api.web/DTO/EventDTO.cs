using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public class EventDTO
    {
        #region PROPERTIES
        public int ID { get; set; }
        public int StandardProfileID { get; set; }
        public StandardProfileDTO CreatorProfile { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public int EventType { get; set; }
        public DateTime? StartingTime { get; set; }
        public int LocationID { get; set; }
        public LocationDTO Location { get; set; }
        public bool IsPublic { get; set; }
        public int MaxStarters { get; set; }
        public int MaxSubs { get; set; }
        public int CountStarters { get; set; }
        public int CountSubs { get; set; }
        public List<EventParticipantDTO> ListStarters { get; set; }
        public List<EventParticipantDTO> ListSubstitutes { get; set; }

        #endregion

        public EventDTO()
        {
            try
            {
                this.ID = 0;
                this.StandardProfileID = 0;
                this.CreatorProfile = null;
                this.EventName = "";
                this.Description = "";
                this.EventType = 0;
                this.StartingTime = DateTime.Now;
                this.CountStarters = 0;
                this.CountSubs = 0;
                this.LocationID = 0;
                this.Location = null;
                this.IsPublic = false;
                this.MaxStarters = 10;
                this.MaxSubs = 10;
                this.ListStarters = new List<EventParticipantDTO>();
                this.ListSubstitutes = new List<EventParticipantDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EventDTO(Event eve)
        {
            try
            {
                this.ID = eve.ID;
                this.StandardProfileID = eve.StandardProfileID;
                this.CreatorProfile = new StandardProfileDTO(eve.CreatorProfile);
                this.EventName = eve.EventName;
                this.Description = eve.Description;
                this.EventType = eve.EventType;
                this.StartingTime = eve.StartingTime;
                this.CountStarters = eve.CountStarters;
                this.CountSubs = eve.CountSubs;
                this.LocationID = eve.LocationID;
                this.Location = new LocationDTO(eve.Location);
                this.IsPublic = eve.IsPublic;
                this.MaxStarters = eve.MaxStarters;
                this.MaxSubs = eve.MaxSubs;
                this.ListStarters = new List<EventParticipantDTO>();
                this.ListSubstitutes = new List<EventParticipantDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Event MapFromDTO()
        {
            try
            {
                return new Event(this.StandardProfileID, this.EventName, this.Description, this.EventType, this.StartingTime, this.LocationID, this.IsPublic, this.MaxStarters, this.MaxSubs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
