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
                notifications = notificationRepo.SearchFor(n => n.StandardProfileID == profileId);
                return notifications;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Notification> GetUnseenNotifications(int profileId)
        {
            try
            {
                StandardProfile profile = profileManager.GetProfileById(profileId);
                if (profile == null)
                {
                    throw new Exception("Profile could not be found for that ID.");
                }
                List<Notification> notifications = new List<Notification>();
                notifications = notificationRepo.SearchFor(n => n.StandardProfileID == profileId && n.Status == (int)NotificationStatus.NEW);
                return notifications;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Notification GetNotificationById(int id)
        {
            try
            {
                Notification notification = notificationRepo.GetById(id);
                if (notification != null)
                {
                    StandardProfileManager spm = new StandardProfileManager();
                    notification.Profile = spm.GetProfileById(notification.StandardProfileID);
                }
                return notification;
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

        public void GenerateNotification(string message, NotificationStatus status, NotificationType type, int idProfile)
        {
            try
            {
                Notification newnot = new Notification(idProfile, status, type, message);
                InsertNotification(newnot);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region UPDATES
        public void UpdateNotification(Notification notificationntUpdated, Notification newData)
        {
            try
            {
                if (notificationntUpdated != null && newData != null)
                {
                    notificationntUpdated.Message = newData.Message;
                    notificationntUpdated.Type = newData.Type;
                    notificationntUpdated.StandardProfileID = newData.StandardProfileID;
                    notificationntUpdated.Status = newData.Status;
                    UpdateNotification(notificationntUpdated);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNotification(Notification notification)
        {
            try
            {
                if (notification != null)
                {
                    notificationRepo.Update(notification);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SeenStatus(Notification notificationntUpdated)
        {
            try
            {
                if (notificationntUpdated != null)
                {
                    notificationntUpdated.Status = NotificationStatus.SEEN;
                    UpdateNotification(notificationntUpdated);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
