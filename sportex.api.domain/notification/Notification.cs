using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace sportex.api.domain.notification
{
    public class Notification
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public int StandardProfileID { get; set; }
        [ForeignKey("StandardProfileID")]
        public StandardProfile Profile { get; set; }
        public NotificationStatus Status { get; set; }
        public NotificationType Type { get; set; }
        public String Message { get; set; }
        public DateTime CreatedOn { get; set; }

        #endregion

        #region CONSTRUCTORS
        public Notification(StandardProfile profile, NotificationStatus status, NotificationType type, String message)
        {
            this.StandardProfileID = profile.ID;
            this.Profile = profile;
            this.Status = status;
            this.Type = type;
            this.Message = message;
            this.CreatedOn = DateTime.Now;
        }
    
        public Notification(StandardProfile profile, String message)
        {
            this.StandardProfileID = profile.ID;
            this.Profile = profile;
            this.Message = message;
            this.CreatedOn = DateTime.Now;
            this.Status = NotificationStatus.NEW;
            this.Type = NotificationType.DEFAULT;
        }

        public Notification(int idProfile, NotificationStatus status, NotificationType type, String message)
        {
            this.StandardProfileID = idProfile;
            this.Profile = null;
            this.Status = status;
            this.Type = type;
            this.Message = message;
            this.CreatedOn = DateTime.Now;
        }

        #endregion
    }
}
