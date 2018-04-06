using System;
using System.Collections.Generic;
using System.Text;
using sportex.api.persistence;
using sportex.api.domain;
using System.Linq;
using sportex.api.domain.notification;

namespace sportex.api.logic
{
    public class NotificationManager
    {
        IRepository<Notification> notificationRepo;
        StandardProfileManager profileManager;
        public NotificationManager()
        {
            notificationRepo = new Repository<Notification>();
            profileManager = new StandardProfileManager();
        }
        public List<Notification> GetAllNotifications(int profileId)
        {
            try
            {
                StandardProfile profile = profileManager.GetProfileById(profileId);
                if(profile == null)
                {
                    throw new Exception("Profile could not be found for that ID.");
                }
                List<Notification> notifications = new List<Notification>();
                notifications = notificationRepo.GetAll();
                return notifications;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertNotification(Notification notification)
        {
            try
            {
                notificationRepo.Insert(notification);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
