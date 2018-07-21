using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.domain.EventClasses;
using sportex.api.logic;
using sportex.api.web.DTO;

namespace sportex.api.web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/eventInvitation")]
    public class EventInvitationController : BaseController
    {
        [HttpGet]
        [Route("GetEventInvitationsSent/{idProfile}")]
        public IEnumerable<EventInvitationDTO> GetEventInvitationsSent(int idProfile)
        {
            try
            {
                EventInvitationManager eim = new EventInvitationManager();
                List<EventInvitation> listInvitations = eim.GetInvitationsSent(idProfile);
                List<EventInvitationDTO> listDTOs = new List<EventInvitationDTO>();
                foreach (EventInvitation inv in listInvitations)
                {
                    listDTOs.Add(new EventInvitationDTO(inv));
                }
                return listDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetEventInvitationsReceived/{idProfile}")]
        public IEnumerable<EventInvitationDTO> GetEventInvitationsReceived(int idProfile)
        {
            try
            {
                EventInvitationManager eim = new EventInvitationManager();
                List<EventInvitation> listInvitations = eim.GetInvitationsReceived(idProfile);
                List<EventInvitationDTO> listDTOs = new List<EventInvitationDTO>();
                foreach (EventInvitation inv in listInvitations)
                {
                    listDTOs.Add(new EventInvitationDTO(inv));
                }
                return listDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public EventInvitationDTO Get(int id)
        {
            try
            {
                EventInvitationManager eim = new EventInvitationManager();
                EventInvitation invitation = eim.GetEventInvitationById(id);
                return new EventInvitationDTO(invitation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: api/EventInvitation
        [HttpPost]
        public IActionResult Post([FromBody]EventInvitationDTO invitationDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (invitationDTO != null)
                    {
                        EventInvitation inv = invitationDTO.MapFromDTO();
                        EventInvitationManager eim = new EventInvitationManager();
                        eim.InsertEventInvitation(inv);
                        eim.GenerateInvitationNotification(inv.IdProfileInvited, inv.IdProfileInvites, inv.EventID);
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

        [HttpPost]
        [Route("AcceptEventInvitation")]
        public IActionResult AcceptEventInvitation([FromBody]EventInvitationRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EventInvitationManager eim = new EventInvitationManager();
                    EventResult result = eim.AcceptEventInvitation(request.idEvent, request.idProfileReceived);
                    return Ok(result);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("InviteWholeGroup")]
        public IActionResult InviteWholeGroup([FromBody]EventInvitationRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EventInvitationManager eim = new EventInvitationManager();
                    eim.InviteWholeGroup(request.idEvent, request.idGroup);
                    return StatusCode(200);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/EventInvitation/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class EventInvitationRequest
    {
        public int idProfileReceived { get; set; }
        public int idProfileSent { get; set; }
        public int idEvent { get; set; }
        public int idGroup { get; set; }
    }
}
