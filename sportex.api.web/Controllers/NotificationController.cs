using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.domain.notification;
using sportex.api.logic;

namespace sportex.api.web.Controllers
{
    [Authorize]
    [Route("api/notification")]
    public class NotificationController : BaseController
    {
        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Route("profilenotifications/{id}")]
        public IActionResult GetAll(int id)
        {
            try
            {
                NotificationManager notificationManager = new NotificationManager();
                return Ok(notificationManager.GetAllNotifications(id));
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Route("profilenotificationsunseen/{id}")]
        public IActionResult GetUnseen(int id)
        {
            try
            {
                NotificationManager notificationManager = new NotificationManager();
                return Ok(notificationManager.GetUnseenNotifications(id));
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Notification notification)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NotificationManager notificationManager = new NotificationManager();
                    notificationManager.InsertNotification(notification);
                    return StatusCode(200);
                }
                return StatusCode(400);
                
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Notification notification)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (notification != null)
                    {
                        NotificationManager nm = new NotificationManager();
                        Notification updated = nm.GetNotificationById(id);
                        if (updated != null)
                        {
                            nm.UpdateNotification(updated, notification);
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

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Route("setseen/{id}")]
        public IActionResult SeeNotification(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NotificationManager nm = new NotificationManager();
                    Notification updated = nm.GetNotificationById(id);
                    if (updated != null)
                    {
                        nm.SeenStatus(updated);
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

        // PUT api/<controller>/5
        [HttpPut("{idProfile}")]
        [Route("setallseen/{idProfile}")]
        public IActionResult SeeAllNotifications(int idProfile)
        {
            try
            {
                NotificationManager notificationManager = new NotificationManager();
                notificationManager.SeeAllNotifications(idProfile);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return StatusCode(403);
        }
    }
}
