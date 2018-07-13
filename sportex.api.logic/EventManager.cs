using sportex.api.domain;
using sportex.api.domain.EventClasses;
using sportex.api.domain.notification;
using sportex.api.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sportex.api.logic
{
    public class EventManager
    {
        #region PROPERTIES
        private IRepository<Event> repoEvents;
        private IRepository<EventParticipant> repoParticipants;
        public EventManager()
        {
            repoEvents = new Repository<Event>();
            repoParticipants = new Repository<EventParticipant>();
        }
        #endregion

        #region GETS
        public List<Event> GetAllEvents()
        {
            try
            {
                List<Event> events = new List<Event>();
                events = repoEvents.GetAll();

                //IRepository<StandardProfile> repoStandardProfile = new Repository<StandardProfile>();
                StandardProfileManager spm = new StandardProfileManager();
                LocationManager lm = new LocationManager();
                foreach (Event eve in events)
                {   
                    eve.CreatorProfile = spm.GetProfileById(eve.StandardProfileID);
                    //eve.CreatorProfile = repoStandardProfile.GetById(eve.StandardProfileID);
                    eve.Location = lm.GetLocationById(eve.LocationID);
                }

                return events;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Event GetEventById(int id)
        {
            try
            {
                Event eve = repoEvents.GetById(id);
                //IRepository<StandardProfile> repoStandardProfile = new Repository<StandardProfile>();
                //eve.CreatorProfile = repoStandardProfile.GetById(eve.StandardProfileID);
                if (eve != null)
                {
                    StandardProfileManager spm = new StandardProfileManager();
                    eve.CreatorProfile = spm.GetProfileById(eve.StandardProfileID);
                    LocationManager lm = new LocationManager();
                    eve.Location = lm.GetLocationById(eve.LocationID);
                }
                return eve;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Event> GetEventByProfileId(int profileId)
        {
            try
            {
                return repoEvents.SearchFor(ev => ev.StandardProfileID == profileId, new string[] {
                    "CreatorProfile",
                    "CreatorProfile.Account",
                    "Location"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Event> GetEventByTimestamp(long timestamp)
        {
            try
            {
                return repoEvents.SearchFor(ev => ev.StartingTime.HasValue && ((DateTimeOffset)ev.StartingTime.Value).ToUnixTimeSeconds() == timestamp, new string[] {
                    "CreatorProfile",
                    "CreatorProfile.Account",
                    "Location"
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Event> GetEventsJoinedByProfile(int id)
        {
            try
            {
                List<Event> events = new List<Event>();
                List<EventParticipant> participations = GetProfileParticipations(id);
                Event even;
                foreach (EventParticipant participation in participations)
                {
                    even = repoEvents.GetById(participation.EventID);
                    if(even.Status == 1)
                    {
                        events.Add(even);
                    }     
                }
                StandardProfileManager spm = new StandardProfileManager();
                LocationManager lm = new LocationManager();
                foreach (Event eve in events)
                {
                    eve.CreatorProfile = spm.GetProfileById(eve.StandardProfileID);
                    eve.Location = lm.GetLocationById(eve.LocationID);
                }

                return events;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Event> GetPastEventsJoinedByProfile(int id)
        {
            try
            {
                List<Event> events = new List<Event>();
                List<EventParticipant> participations = GetProfileParticipations(id);
                foreach (EventParticipant participation in participations)
                {
                    Event eve = repoEvents.GetById(participation.EventID);
                    if (eve.Status != 1)
                    {
                        events.Add(eve);
                    }
                }
                StandardProfileManager spm = new StandardProfileManager();
                LocationManager lm = new LocationManager();
                foreach (Event eve in events)
                {
                    eve.CreatorProfile = spm.GetProfileById(eve.StandardProfileID);
                    eve.Location = lm.GetLocationById(eve.LocationID);
                }

                return events;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EventParticipant> GetProfileParticipations(int id)
        {
            try
            {
                return repoParticipants.SearchFor(p => p.StandardProfileID == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Event> GetEventsInvited(int idProfile)
        {
            try
            {
                EventInvitationManager eim = new EventInvitationManager();
                List<EventInvitation> invitations = eim.GetInvitationsReceived(idProfile);
                List<Event> eventsInvited = new List<Event>();
                Event even;
                foreach(EventInvitation invitation in invitations)
                {
                    even = repoEvents.GetById(invitation.EventID);
                    if (even.Status == 1)
                    {
                        eventsInvited.Add(even);
                    }
                }
                StandardProfileManager spm = new StandardProfileManager();
                LocationManager lm = new LocationManager();
                foreach (Event eve in eventsInvited)
                {
                    eve.CreatorProfile = spm.GetProfileById(eve.StandardProfileID);
                    eve.Location = lm.GetLocationById(eve.LocationID);
                }

                return eventsInvited;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Event> GetAllPublicEvents()
        {
            try
            {
                List<Event> events = new List<Event>();
                events = repoEvents.SearchFor(ev => ev.IsPublic == true && ev.Status==1);
                StandardProfileManager spm = new StandardProfileManager();
                LocationManager lm = new LocationManager();
                foreach (Event eve in events)
                {
                    eve.CreatorProfile = spm.GetProfileById(eve.StandardProfileID);
                    eve.Location = lm.GetLocationById(eve.LocationID);
                }

                return events;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Event> GetAllPublicNonJoinedEvents(int idProfile)
        {
            try
            {
                List<Event> publicEvents = GetAllPublicEvents();
                List<Event> joinedEvents = GetEventsJoinedByProfile(idProfile);
                publicEvents.RemoveAll(a => joinedEvents.Exists(b => a.ID == b.ID));
                return publicEvents;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Event> GetEventsAvaiableForProfile(int id)
        {
            try
            {
                List<Event> invitedEvents = GetEventsInvited(id);
                List<Event> publicEvents = GetAllPublicNonJoinedEvents(id);
                //List<Event> union = joinedEvents.Union<Event>(publicEvents).ToList();
                //List<Event> distinct = union.GroupBy(eve => eve.ID).Select(e => e.First()).ToList();
                //List<Event> ordered = distinct.OrderByDescending(eve => eve.StartingTime).ToList();
                List<Event> avaiableEvents = invitedEvents.Union(publicEvents).GroupBy(eve => eve.ID).Select(e => e.First()).OrderBy(eve => eve.StartingTime).ToList();

                return avaiableEvents;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region INSERTS
        public void InsertEvent(Event eve)
        {
            try
            {
                eve.Status = 1;
                eve.CreatedOn = DateTime.Now;
                eve.LastUpdate = eve.CreatedOn;
                repoEvents.Insert(eve);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertParticipant(EventParticipant participant)
        {
            try
            {
                participant.Status = 1;
                participant.CreatedOn = DateTime.Now;
                participant.LastUpdate = participant.CreatedOn;
                repoParticipants.Insert(participant);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UPDATES
        public void UpdateEvent(Event eventUpdated, Event newData)
        {
            try
            {
                if (eventUpdated != null && newData != null)
                {
                    eventUpdated.EventName = newData.EventName;
                    eventUpdated.Description = newData.Description;
                    eventUpdated.EventType = newData.EventType;
                    eventUpdated.MaxStarters = newData.MaxStarters;
                    eventUpdated.MaxSubs = newData.MaxSubs;
                    eventUpdated.IsPublic = newData.IsPublic;
                    eventUpdated.StartingTime = newData.StartingTime;
                    eventUpdated.StandardProfileID = newData.StandardProfileID;
                    eventUpdated.LocationID = newData.LocationID;
                    eventUpdated.Status = newData.Status;
                    UpdateEvent(eventUpdated);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateEvent(Event eve)
        {
            try
            {
                if (eve != null)
                {
                    eve.LastUpdate = DateTime.Now;
                    repoEvents.Update(eve);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateParticipant(EventParticipant participant)
        {
            try
            {
                if (participant != null)
                {
                    repoParticipants.Update(participant);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void OpenEventToPublic(Event eventUpdated)
        {
            try
            {
                if (eventUpdated != null)
                {
                    eventUpdated.IsPublic = true;
                    UpdateEvent(eventUpdated);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseEventToPublic(Event eventUpdated)
        {
            try
            {
                if (eventUpdated != null)
                {
                    eventUpdated.IsPublic = false;
                    UpdateEvent(eventUpdated);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DELETES
        public void DeleteEvent(int id)
        {
            try
            {
                foreach (EventParticipant participant in GetParticipants(id))
                {
                    repoParticipants.Delete(participant);
                }
                repoEvents.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        #region JOIN EVENT
        public EventResult JoinEvent(int idProfile, int idEvent)
        {
            try
            {
                if (ProfileIsParticipating(idEvent, idProfile))
                {
                    return new EventResult(3, "Ya participa del evento.");
                    //return "Ya participa del evento.";
                }
                else
                {
                    IRepository<StandardProfile> repoProfile = new Repository<StandardProfile>();
                    StandardProfile profile = repoProfile.GetById(idProfile);
                    if (profile == null) return new EventResult(3, "No existe un perfil con ese ID.");//return "No existe un perfil con ese ID.";
                    else
                    {
                        Event eve = repoEvents.GetById(idEvent);
                        if (eve == null) return new EventResult(3, "No existe un evento con ese ID.");//return "No existe un evento con ese ID.";
                        else return JoinEvent(profile, eve);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EventResult JoinEvent(StandardProfile profile, Event eve)
        {
            try
            {
                if (eve.FullSubs()) return new EventResult(3, "El evento " + eve.EventName + " ya llegó a su máxima capacidad de participantes.");//return "El evento " + eve.EventName + " ya llegó a su máxima capacidad de participantes.";
                else
                {
                    string resultMessage;
                    if (eve.FullStarters())
                    {
                        //Ingresa como suplente                      
                        resultMessage = profile.FullName() + " ha ingresado al evento " + eve.EventName + " como suplente.";

                        //Notifica a todos los participantes del evento (se realiza antes de actualizar el evento en la BD)
                        NotifyAllParticipants(resultMessage, NotificationStatus.NEW, NotificationType.EVENT_PARTICIPANT_JOINED, eve.ID);

                        EventParticipant newParticipant = new EventParticipant(profile.ID, eve.ID, EventParticipant.ParticipationType.Substitute, eve.CountSubs + 1);
                        InsertParticipant(newParticipant);
                        eve.CountSubs += 1;
                        UpdateEvent(eve);
                    }
                    else
                    {
                        //Ingresa como titular
                        resultMessage = profile.FullName() + " ha ingresado al evento " + eve.EventName + " como titular.";

                        //Notifica a todos los participantes del evento (se realiza antes de actualizar el evento en la BD)
                        NotifyAllParticipants(resultMessage, NotificationStatus.NEW, NotificationType.EVENT_PARTICIPANT_JOINED, eve.ID);

                        EventParticipant newParticipant = new EventParticipant(profile.ID, eve.ID, EventParticipant.ParticipationType.Starting, eve.CountStarters + 1);
                        InsertParticipant(newParticipant);
                        eve.CountStarters += 1;
                        UpdateEvent(eve);
                    }

                    //return resultMessage;
                    return new EventResult(1, resultMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region LEAVE EVENT
        public string LeaveEvent(int idProfile, int idEvent)
        {
            try
            {
                IRepository<StandardProfile> repoProfile = new Repository<StandardProfile>();
                StandardProfile profile = repoProfile.GetById(idProfile);
                if (profile == null) return "No existe un perfil con ese ID";
                else
                {
                    Event eve = repoEvents.GetById(idEvent);
                    if (eve == null) return "No existe un evento con ese ID";
                    else return LeaveEvent(profile, eve);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string LeaveEvent(StandardProfile profile, Event eve)
        {
            try
            {
                EventParticipant participant = repoParticipants.SearchFor(p => p.EventID == eve.ID && p.StandardProfileID == profile.ID).FirstOrDefault<EventParticipant>();
                if (participant == null) return "El participante no pertenece al evento.";
                else
                {
                    //Quito al participante del evento
                    repoParticipants.Delete(participant);
                    string resultMessage;
                    if (participant.Type == (int)EventParticipant.ParticipationType.Substitute)
                    {
                        //Era suplente
                        eve.CountSubs -= 1;
                        UpdateOrder(participant, EventParticipant.ParticipationType.Substitute); //Actualiza el orden de los suplentes
                        UpdateEvent(eve);
                        
                    }
                    else if (participant.Type == (int)EventParticipant.ParticipationType.Starting)
                    {
                        //Era titular
                        UpdateOrder(participant, EventParticipant.ParticipationType.Starting); //Actualiza el orden de los titulares
                        if (eve.CountSubs > 0)
                        {
                            //Hay suplentes, el primer suplente toma su lugar         
                            EventParticipant firstSub = GetFirstSubstitute(eve.ID);
                            UpdateOrder(firstSub, EventParticipant.ParticipationType.Substitute); //Actualiza el orden de los suplentes
                            firstSub.Type = (int)EventParticipant.ParticipationType.Starting;
                            firstSub.Order = eve.CountStarters;
                            UpdateParticipant(firstSub);
                            eve.CountSubs -= 1;
                            UpdateEvent(eve);

                            //Notifica al primer suplente
                            resultMessage = "¡Buenas noticias! Ahora estás dentro de la cancha en el evento " + eve.EventName + ".";
                            GenerateNotification(resultMessage, NotificationStatus.NEW, NotificationType.PLAYER_STARTER, firstSub.StandardProfileID);
                        }
                        else
                        {
                            //No hay suplentes
                            eve.CountStarters -= 1;
                            UpdateEvent(eve);
                        }
                    }
                    //Notifica a todos los participantes del evento
                    resultMessage = profile.FullName() + " ha dejado el evento" + eve.EventName + ".";
                    NotifyAllParticipants(resultMessage, NotificationStatus.NEW, NotificationType.EVENT_PARTICIPANT_DROPED, eve.ID);

                    return resultMessage;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void UpdateOrder(EventParticipant leavingParticipant, EventParticipant.ParticipationType type)
        {
            try
            {
                List<EventParticipant> participantsLowerOrder = repoParticipants.SearchFor(p => p.EventID == leavingParticipant.EventID && p.Type == (int)type && p.Order > leavingParticipant.Order);
                foreach(EventParticipant participant in participantsLowerOrder)
                {
                    participant.Order -= 1;
                    UpdateParticipant(participant);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EventParticipant GetFirstSubstitute(int idEvent)
        {
            try
            {
                return repoParticipants.SearchFor(p => p.EventID == idEvent && p.Type == (int)EventParticipant.ParticipationType.Substitute && p.Order == 1).FirstOrDefault<EventParticipant>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region PARTICIPANTS METHODS

        public List<EventParticipant> GetParticipants(int idEvent)
        {
            try
            {
                List<EventParticipant> participants = repoParticipants.SearchFor(p => p.EventID == idEvent);
                StandardProfileManager spm = new StandardProfileManager();
                foreach (EventParticipant participant in participants)
                {
                    participant.ProfileParticipant = spm.GetProfileById(participant.StandardProfileID);
                }
                return participants;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventParticipant> GetParticipantsWithType(int idEvent, EventParticipant.ParticipationType type)
        {
            try
            {
                List<EventParticipant> participants = repoParticipants.SearchFor(p => p.EventID == idEvent && p.Type == (int)type);
                StandardProfileManager spm = new StandardProfileManager();
                foreach(EventParticipant participant in participants)
                {
                    participant.ProfileParticipant = spm.GetProfileById(participant.StandardProfileID);
                }
                return participants;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StandardProfile> GetStartersProfiles(int idEvent)
        {
            try
            {
                List<StandardProfile> starters = new List<StandardProfile>();
                List<EventParticipant> participants = GetParticipantsWithType(idEvent, EventParticipant.ParticipationType.Starting);
                StandardProfileManager spm = new StandardProfileManager();
                foreach(EventParticipant participant in participants)
                {
                    starters.Add(spm.GetProfileById(participant.StandardProfileID));
                }
                return starters;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<StandardProfile> GetSubstitutesProfiles(int idEvent)
        {
            try
            {
                List<StandardProfile> subs = new List<StandardProfile>();
                List<EventParticipant> participants = GetParticipantsWithType(idEvent, EventParticipant.ParticipationType.Substitute);
                StandardProfileManager spm = new StandardProfileManager();
                foreach (EventParticipant participant in participants)
                {
                    subs.Add(spm.GetProfileById(participant.StandardProfileID));
                }
                return subs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ProfileIsParticipating(int idEvent, int idProfile)
        {
            try
            {
                EventParticipant participant = repoParticipants.SearchFor(p => p.EventID == idEvent && p.StandardProfileID == idProfile && p.Type > 0).FirstOrDefault<EventParticipant>();
                return participant != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region NOTIFICATIONS

        public void GenerateNotification(string message, NotificationStatus status, NotificationType type, int idProfile)
        {
            try
            {
                NotificationManager nm = new NotificationManager();
                nm.GenerateNotification(message, status, type, idProfile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void NotifyAllParticipants(string message, NotificationStatus status, NotificationType type, int idEvent)
        {
            try
            {
                List<EventParticipant> participants = GetParticipants(idEvent);
                foreach (EventParticipant participant in participants)
                {
                    GenerateNotification(message, status, type, participant.StandardProfileID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DATE CONTROLS

        public void EventCompleted(Event eve)
        {
            try
            {
                eve.Status = 2; //completed
                //Send review request to participants
                repoEvents.Update(eve);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CheckCompletedEvents(DateTime timeChecked)
        {
            try
            {
                List<Event> pastTimeEvents = repoEvents.SearchFor(ev => ev.StartingTime <= timeChecked && ev.Status == 1).ToList();
                foreach(Event eve in pastTimeEvents)
                {
                    EventCompleted(eve);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CheckUpcomingEvents(DateTime timeChecked)
        {
            try
            {
                List<Event> pastTimeEvents = repoEvents.SearchFor(ev => HoursBetweenDates(timeChecked, ev.StartingTime) <= 1 && ev.Status == 1).ToList();
                foreach (Event eve in pastTimeEvents)
                {
                    //notify participants
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private double HoursBetweenDates(DateTime? date1, DateTime? date2)
        {
            try
            {
                if (date1 != null && date2 != null)
                {
                    return ((DateTime)date1 - (DateTime)date2).TotalHours;
                }
                return 1000;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public void LogTest(DateTime date)
        {
            try
            {
                Log log = new Log(9, "Log de prueba de webjob", "Se ejecuta con fecha " + date.ToShortDateString() + " " + date.ToShortTimeString());
                IRepository<Log> repoLog = new Repository<Log>();
                repoLog.Insert(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

    }
}
