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
        public void Post([FromBody]Notification notification)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NotificationManager notificationManager = new NotificationManager();
                    notificationManager.InsertNotification(notification);
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

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
