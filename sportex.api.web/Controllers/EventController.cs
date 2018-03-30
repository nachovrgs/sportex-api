using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;

namespace sportex.api.web.Controllers
{
    [Produces("application/json")]
    [Route("api/Event")]
    public class EventController : BaseController
    {
        // GET: api/Event
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            try
            {
                EventManager em = new EventManager();
                return em.GetAllEvents();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Event/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet("{id}")]
        public Event Get(int id)
        {
            try
            {
                EventManager em = new EventManager();
                return em.GetEventById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        // POST: api/Event
        [HttpPost]
        public void Post([FromBody]Event eve)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (eve != null)
                    {
                        EventManager em = new EventManager();
                        em.InsertEvent(eve);
                    }
                }
                else
                {
                    //error
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        // PUT: api/Event/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("JoinEvent")]
        public string JoinEvent([FromBody]EventRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EventManager em = new EventManager();
                    return em.JoinEvent(request.idProfile, request.idEvent);
                }
                return "Los datos ingresados no son correctos";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("LeaveEvent")]
        public string LeaveEvent([FromBody]EventRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EventManager em = new EventManager();
                    return em.LeaveEvent(request.idProfile, request.idEvent);
                }
                return "Los datos ingresados no son correctos";
            }
            catch (Exception ex)
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
