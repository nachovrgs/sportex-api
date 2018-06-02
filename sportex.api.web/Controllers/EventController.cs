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
    [Route("api/Event")]
    public class EventController : BaseController
    {
        // GET: api/Event
        [HttpGet]
        public IEnumerable<EventDTO> Get()
        {
            try
            {
                EventManager em = new EventManager();
                List<Event> listEvents = em.GetAllEvents();
                List<EventDTO> listDTOs = new List<EventDTO>();
                foreach (Event eve in listEvents)
                {
                    listDTOs.Add(new EventDTO(eve));
                }
                return listDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Event/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet("{id}")]
        public EventDTO Get(int id)
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
                    return dto;
                }
                else
                {
                    //mostrar error
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
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
        
        // PUT: api/Event/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return StatusCode(403);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return StatusCode(403);
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
                foreach (EventParticipant participant in participants)
                {
                    dto.ListStarters.Add(new EventParticipantDTO(participant));
                }
                participants = em.GetParticipantsWithType(dto.ID, EventParticipant.ParticipationType.Substitute);
                foreach (EventParticipant participant in participants)
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
