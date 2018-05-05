using sportex.api.domain;
using sportex.api.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sportex.api.logic
{
    public class EventInvitationManager
    {
        #region PROPERTIES
        private IRepository<EventInvitation> repoInvitations;
        public EventInvitationManager()
        {
            repoInvitations = new Repository<EventInvitation>();
        }
        #endregion

        #region GETS
        public List<EventInvitation> GetInvitationsReceived(int idProfile)
        {
            try
            {
                List<EventInvitation> invites = new List<EventInvitation>();
                invites = repoInvitations.SearchFor(i => i.IdProfileInvited == idProfile);

                StandardProfileManager spm = new StandardProfileManager();
                EventManager em = new EventManager();
                foreach (EventInvitation inv in invites)
                {
                    inv.ProfileInvites = spm.GetProfileById(inv.IdProfileInvites);
                    inv.ProfileInvited = spm.GetProfileById(inv.IdProfileInvited);
                    inv.EventInvited = em.GetEventById(inv.EventID);
                }

                return invites;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventInvitation> GetInvitationsSent(int idProfile)
        {
            try
            {
                List<EventInvitation> invites = new List<EventInvitation>();
                invites = repoInvitations.SearchFor(i => i.IdProfileInvites == idProfile);

                StandardProfileManager spm = new StandardProfileManager();
                EventManager em = new EventManager();
                foreach (EventInvitation inv in invites)
                {
                    inv.ProfileInvites = spm.GetProfileById(inv.IdProfileInvites);
                    inv.ProfileInvited = spm.GetProfileById(inv.IdProfileInvited);
                    inv.EventInvited = em.GetEventById(inv.EventID);
                }

                return invites;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EventInvitation GetEventInvitationById(int id)
        {
            try
            {
                EventInvitation invitation = repoInvitations.GetById(id);

                if (invitation != null)
                {
                    StandardProfileManager spm = new StandardProfileManager();
                    EventManager em = new EventManager();
                    invitation.ProfileInvites = spm.GetProfileById(invitation.IdProfileInvites);
                    invitation.ProfileInvited = spm.GetProfileById(invitation.IdProfileInvited);
                    invitation.EventInvited = em.GetEventById(invitation.EventID);
                }
                return invitation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void InsertEventInvitation(EventInvitation inv)
        {
            try
            {
                inv.Status = 1;
                inv.CreatedOn = DateTime.Now;
                inv.LastUpdate = inv.CreatedOn;
                repoInvitations.Insert(inv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ACCEPT EVENT INVITATION
        public string AcceptEventInvitation(int idEvent, int idProfileAccepts, int idProfileSent)
        {
            try
            {

                /*
                IRepository<StandardProfile> profileRepo = new Repository<StandardProfile>();
                IRepository<Event> eventRepo = new Repository<Event>();
                StandardProfile sends = profileRepo.GetById(idProfileAccepts);
                StandardProfile receives = profileRepo.GetById(idProfileSent);
                Event eve = eventRepo.GetById(idEvent);
                if (sends != null && receives != null && eve !=null)
                {
                */

                EventInvitation invitation = repoInvitations.SearchFor(i => i.EventID == idEvent && i.IdProfileInvites == idProfileSent && i.IdProfileInvited == idProfileAccepts).FirstOrDefault<EventInvitation>();
                if (invitation != null)
                {
                    //Existe una invitacion
                    //Chequear si el jugador ya ingreso al evento
                    EventManager em = new EventManager();
                    if(!em.ProfileIsParticipating(idEvent, idProfileAccepts))
                    {
                        //El usuario no esta participando, se une
                        em.JoinEvent(idProfileAccepts, idEvent);
                        return "Se ha ingresado al evento";
                    }
                    return "El usuario ya habia ingresado al evento";
                }
                return "Los datos ingresados no son correctos";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
