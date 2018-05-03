using sportex.api.domain;
using sportex.api.persistence;
using System;
using System.Collections.Generic;
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

                StandardProfileManager spm = new StandardProfileManager();
                EventManager em = new EventManager();
                invitation.ProfileInvites = spm.GetProfileById(invitation.IdProfileInvites);
                invitation.ProfileInvited = spm.GetProfileById(invitation.IdProfileInvited);
                invitation.EventInvited = em.GetEventById(invitation.EventID);
                return invitation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}
