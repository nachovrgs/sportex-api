using sportex.api.domain;
using sportex.api.domain.notification;
using sportex.api.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sportex.api.logic
{
    public class PlayerReviewManager
    {
        #region PROPERTIES
        private IRepository<PlayerReview> repoReviews;
        public PlayerReviewManager()
        {
            repoReviews = new Repository<PlayerReview>();
        }
        #endregion

        #region GETS
        public List<PlayerReview> GetReviewsReceived(int idProfile)
        {
            try
            {
                List<PlayerReview> reviews = new List<PlayerReview>();
                reviews = repoReviews.SearchFor(i => i.IdProfileReviewed == idProfile);

                StandardProfileManager spm = new StandardProfileManager();
                EventManager em = new EventManager();
                foreach (PlayerReview rev in reviews)
                {
                    rev.ProfileReviews = spm.GetProfileById(rev.IdProfileReviews);
                    rev.ProfileReviewed = spm.GetProfileById(rev.IdProfileReviewed);
                    rev.EventReviewed = em.GetEventById(rev.EventID);
                }

                return reviews;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlayerReview> GetReviewsSent(int idProfile)
        {
            try
            {
                List<PlayerReview> reviews = new List<PlayerReview>();
                reviews = repoReviews.SearchFor(i => i.IdProfileReviews == idProfile);

                StandardProfileManager spm = new StandardProfileManager();
                EventManager em = new EventManager();
                foreach (PlayerReview rev in reviews)
                {
                    rev.ProfileReviews = spm.GetProfileById(rev.IdProfileReviews);
                    rev.ProfileReviewed = spm.GetProfileById(rev.IdProfileReviewed);
                    rev.EventReviewed = em.GetEventById(rev.EventID);
                }

                return reviews;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlayerReview GetPlayerReviewById(int id)
        {
            try
            {
                PlayerReview review = repoReviews.GetById(id);

                if (review != null)
                {
                    StandardProfileManager spm = new StandardProfileManager();
                    EventManager em = new EventManager();
                    review.ProfileReviews = spm.GetProfileById(review.IdProfileReviews);
                    review.ProfileReviewed = spm.GetProfileById(review.IdProfileReviewed);
                    review.EventReviewed = em.GetEventById(review.EventID);
                }
                return review;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void InsertPlayerReview(PlayerReview rev)
        {
            try
            {
                rev.Status = 1;
                rev.CreatedOn = DateTime.Now;
                rev.LastUpdate = rev.CreatedOn;
                repoReviews.Insert(rev);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ReviewAllEventParticipants(int idEvent, int rate, string message, int idProfileReviews)
        {
            try
            {
                EventManager em = new EventManager();
                Event eve = em.GetEventById(idEvent);
                if (eve != null)
                {
                    PlayerReview review;
                    List<StandardProfile> profiles = em.GetStartersProfiles(idEvent);
                    foreach (StandardProfile profile in profiles)
                    {
                        if (profile.ID != idProfileReviews)
                        {
                            review = repoReviews.SearchFor(r => r.EventID == idEvent && r.IdProfileReviewed == profile.ID && r.IdProfileReviews == idProfileReviews).FirstOrDefault<PlayerReview>();
                            if (review == null)
                            {
                                //No existia una review para ese jugador en ese evento de parte de este usuario
                                review = new PlayerReview(rate, message, idProfileReviews, profile.ID, idEvent);
                                InsertPlayerReview(review);

                                //Notificar al calificado
                                NotificationManager nm = new NotificationManager();
                                nm.GenerateNotification("Has recibido una nueva calificación por el evento " + eve.EventName + ".", NotificationStatus.NEW, NotificationType.EVENT_PARTICIPANT_JOINED, idProfileReviews);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double GetProfileAverageRating(int idProfile)
        {
            try
            {
                IRepository<StandardProfile> repoProfile = new Repository<StandardProfile>();
                StandardProfile profile = repoProfile.GetById(idProfile);
                if (profile != null)
                {
                    double sum = 0;
                    double countReviews = 0;
                    List<PlayerReview> profileReviews = repoReviews.SearchFor(r => r.IdProfileReviewed == idProfile);
                    if (profileReviews.Count > 0)
                    {
                        foreach (PlayerReview review in profileReviews)
                        {
                            countReviews++;
                            sum += review.Rate;
                        }
                        return sum / countReviews;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
