using sportex.api.domain;
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
                return events;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventParticipant> GetParticipants(int idEvent)
        {
            try
            {
                return repoParticipants.SearchFor(p => p.EventID == idEvent);
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
                return repoParticipants.SearchFor(p => p.EventID == idEvent && p.Type == (int)type);
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
                return repoEvents.GetById(id);
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
        private void UpdateEvent(Event eve)
        {
            try
            {
                if (eve != null)
                {
                    repoEvents.Update(eve);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateParticipant(EventParticipant participant)
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
        #endregion

        #region JOIN EVENT
        public string JoinEvent(int idProfile, int idEvent)
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
                    else return JoinEvent(profile, eve);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string JoinEvent(StandardProfile profile, Event eve)
        {
            try
            {
                if (eve.FullSubs()) return "El evento " + eve.EventName + " ya llegó a su máxima capacidad de participantes.";
                else
                {
                    if (eve.FullStarters())
                    {
                        //Ingresa como suplente
                        EventParticipant newParticipant = new EventParticipant(profile.ID, eve.ID, EventParticipant.ParticipationType.Substitute, eve.CountSubs + 1);
                        InsertParticipant(newParticipant);
                        eve.CountSubs += 1;
                        UpdateEvent(eve);
                        return profile.FirstName + " " + profile.LastName + " ha ingresado al evento " + eve.EventName + " como suplente.";
                    }
                    else
                    {
                        //Ingresa como titular
                        EventParticipant newParticipant = new EventParticipant(profile.ID, eve.ID, EventParticipant.ParticipationType.Starting, eve.CountStarters + 1);
                        InsertParticipant(newParticipant);
                        eve.CountStarters += 1;
                        UpdateEvent(eve);
                        return profile.FirstName + " " + profile.LastName + " ha ingresado al evento " + eve.EventName + " como titular.";
                    }
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
                            //NOTIFICAR AL PRIMER SUPLENTE
                        }
                        else
                        {
                            //No hay suplentes
                            eve.CountStarters -= 1;
                            UpdateEvent(eve);
                        }
                    }
                    //NOTIFICAR AL EVENTO
                    return profile.FirstName + " " + profile.LastName + " se ha salido del evento " + eve.EventName + ".";
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

        public EventParticipant GetFirstSubstitute(int eventId)
        {
            try
            {
                return repoParticipants.SearchFor(p => p.EventID == eventId && p.Type == (int)EventParticipant.ParticipationType.Substitute && p.Order == 1).FirstOrDefault<EventParticipant>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
