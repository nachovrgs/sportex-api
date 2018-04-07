using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.domain.notification;
using sportex.api.logic;

namespace sportex.api.web.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : BaseController
    {
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IEnumerable<Notification> Get(int id)
        {
            try
            {
                NotificationManager notificationManager = new NotificationManager();
                return notificationManager.GetAllNotifications(id);
            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return StatusCode(403);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return StatusCode(403);
        }
    }
}
