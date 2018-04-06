using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace sportex.api.domain.notification
{
    public class Notification
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public BaseProfile Profile { get; set; }
        public NotificationStatus Status { get; set; }
        public String Message { get; set; }
        public DateTime CreatedOn { get; set; }

        #endregion

        #region CONSTRUCTORS
        public Notification(BaseProfile profile, NotificationStatus status, String message)
        {
            this.Profile = profile;
            this.Status = status;
            this.Message = message;
            this.CreatedOn = DateTime.Now;
        }
    
        public Notification(BaseProfile profile, String message)
        {
            this.Profile = profile;
            this.Message = message;
            this.CreatedOn = DateTime.Now;
            this.Status = NotificationStatus.NEW;
        }

        #endregion
    }
}
