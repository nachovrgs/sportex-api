using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;
using sportex.api.web.DTO;

namespace sportex.api.web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/event")]
    public class EventController : BaseController
    {
        // GET: api/event
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                EventManager em = new EventManager();
                List<Event> listEvents = em.GetAllEvents();
                List<EventDTO> listDTOs = new List<EventDTO>();
                EventDTO dto;
                foreach (Event eve in listEvents)
                {
                    dto = new EventDTO(eve);
                    //cargar los starters y subtitutes
                    LoadStartersAndSubs(dto, em);
                    listDTOs.Add(dto);
                }
                //foreach (Event eve in listEvents)
                //{
                //    listDTOs.Add(new EventDTO(eve));
                //}
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // GET: api/Event/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                EventManager em = new EventManager();
                Event eve = em.GetEventById(id);
                if (eve != null)
                {
                    EventDTO dto = new EventDTO(eve);
                    //cargar los starters y subtitutes
                    LoadStartersAndSubs(dto, em);
                    return Ok(dto);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // GET: api/event/time/{timestamp}
        [HttpGet("{timestamp}")]
        [Route("time/{timestamp}")]
        public IActionResult GetEventsByDate(long timestamp)
        {
            try
            {
                EventManager em = new EventManager();
                List<Event> listEvents = em.GetEventByTimestamp(timestamp);
                List<EventDTO> listDTOs = new List<EventDTO>();
                EventDTO dto;
                foreach (Event eve in listEvents)
                {
                    dto = new EventDTO(eve);
                    //cargar los starters y subtitutes
                    LoadStartersAndSubs(dto, em);
                    listDTOs.Add(dto);
                }
                //foreach (Event eve in listEvents)
                //{
                //    listDTOs.Add(new EventDTO(eve));
                //}
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        #region EventsForProfile

        // GET: api/event/mine/profileId
        [HttpGet("{profileId}")]
        [Route("mine/{profileId}")]
        public IActionResult GetMyEvents(int profileId)
        {
            try
            {
                EventManager em = new EventManager();
                List<Event> listEvents = em.GetEventByProfileId(profileId);
                List<EventDTO> listDTOs = new List<EventDTO>();
                EventDTO dto;
                foreach (Event eve in listEvents)
                {
                    dto = new EventDTO(eve);
                    //cargar los starters y subtitutes
                    LoadStartersAndSubs(dto, em);
                    listDTOs.Add(dto);
                }
                //foreach (Event eve in listEvents)
                //{
                //    listDTOs.Add(new EventDTO(eve));
                //}
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // GET: api/event/joined/profileId
        [HttpGet("{profileId}")]
        [Route("joined/{profileId}")]
        public IActionResult GetEventsJoined(int profileId)
        {
            try
            {
                EventManager em = new EventManager();
                List<Event> listEvents = em.GetEventsJoinedByProfile(profileId);
                List<EventDTO> listDTOs = new List<EventDTO>();
                EventDTO dto;
                foreach (Event eve in listEvents)
                {
                    dto = new EventDTO(eve);
                    //cargar los starters y subtitutes
                    LoadStartersAndSubs(dto, em);
                    listDTOs.Add(dto);
                }
                //foreach (Event eve in listEvents)
                //{
                //    listDTOs.Add(new EventDTO(eve));
                //}
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }
        // GET: api/event/joined/profileId
        [HttpGet("{profileId}")]
        [Route("past/{profileId}")]
        public IActionResult GetPastEventsJoined(int profileId)
        {
            try
            {
                EventManager em = new EventManager();
                List<Event> listEvents = em.GetPastEventsJoinedByProfile(profileId);
                List<EventDTO> listDTOs = new List<EventDTO>();
                EventDTO dto;
                foreach (Event eve in listEvents)
                {
                    dto = new EventDTO(eve);
                    //cargar los starters y subtitutes
                    LoadStartersAndSubs(dto, em);
                    listDTOs.Add(dto);
                }
                //foreach (Event eve in listEvents)
                //{
                //    listDTOs.Add(new EventDTO(eve));
                //}
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }
        // GET: api/event/avaiable/profileId
        [HttpGet("{profileId}")]
        [Route("avaiable/{profileId}")]
        public IActionResult GetEventsAvaiable(int profileId)
        {
            try
            {
                EventManager em = new EventManager();
                List<Event> listEvents = em.GetEventsAvaiableForProfile(profileId);
                List<EventDTO> listDTOs = new List<EventDTO>();
                EventDTO dto;
                foreach (Event eve in listEvents)
                {
                    dto = new EventDTO(eve);
                    //cargar los starters y subtitutes
                    LoadStartersAndSubs(dto, em);
                    listDTOs.Add(dto);
                }
                //foreach (Event eve in listEvents)
                //{
                //    listDTOs.Add(new EventDTO(eve));
                //}
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500); ;
            }
        }

        #endregion

        // POST: api/Event
        [HttpPost]
        public IActionResult Post([FromBody]EventDTO eventDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (eventDTO != null)
                    {
                        Event eve = eventDTO.MapFromDTO();
                        EventManager em = new EventManager();
                        em.InsertEvent(eve);
                        em.JoinEvent(eve.StandardProfileID, eve.ID);
                        return Ok(eve.ID);
                        //return StatusCode(200);
                    }
                    return StatusCode(400);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        // PUT: api/Event/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]EventDTO eventDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (eventDTO != null)
                    {                        
                        EventManager em = new EventManager();
                        Event updated = em.GetEventById(id);
                        if (updated != null)
                        {
                            Event eve = eventDTO.MapFromDTO();
                            em.UpdateEvent(updated, eve);
                            return StatusCode(200);
                        }                       
                    }
                    return StatusCode(400);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region OPEN AND CLOSE TO PUBLIC

        // PUT: api/Event/opentopublic/5
        [Route("opentopublic/{id}")]
        public IActionResult OpenEventToPublic(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EventManager em = new EventManager();
                    Event updated = em.GetEventById(id);
                    if (updated != null)
                    {
                        em.OpenEventToPublic(updated);
                        return StatusCode(200);
                    }
                    return StatusCode(400);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/Event/closetopublic/5
        [Route("closetopublic/{id}")]
        public IActionResult CloseEventToPublic(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EventManager em = new EventManager();
                    Event updated = em.GetEventById(id);
                    if (updated != null)
                    {
                        em.CloseEventToPublic(updated);
                        return StatusCode(200);
                    }
                    return StatusCode(400);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                EventManager em = new EventManager();
                em.DeleteEvent(id);
                return StatusCode(200);
                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("JoinEvent")]
        public IActionResult JoinEvent([FromBody]EventRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EventManager em = new EventManager();
                    em.JoinEvent(request.idProfile, request.idEvent);
                    return StatusCode(200);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("LeaveEvent")]
        public IActionResult LeaveEvent([FromBody]EventRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EventManager em = new EventManager();
                    em.LeaveEvent(request.idProfile, request.idEvent);
                    return StatusCode(200);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadStartersAndSubs(EventDTO dto, EventManager em)
        {
            try
            {
                List<EventParticipant> participants = em.GetParticipantsWithType(dto.ID, EventParticipant.ParticipationType.Starting);
                foreach (EventParticipant participant in participants.OrderBy(par=>par.Order))
                {
                    dto.ListStarters.Add(new EventParticipantDTO(participant));
                }
                participants = em.GetParticipantsWithType(dto.ID, EventParticipant.ParticipationType.Substitute);
                foreach (EventParticipant participant in participants.OrderBy(par => par.Order))
                {
                    dto.ListSubstitutes.Add(new EventParticipantDTO(participant));
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public class EventRequest
        {
            public int idProfile { get; set; }
            public int idEvent { get; set; }
        }
    }
}
