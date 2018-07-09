using sportex.api.domain;
using sportex.api.domain.EventClasses;
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
                invites = repoInvitations.SearchFor(i => i.IdProfileInvited == idProfile && i.Status == 1);

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
                invites = repoInvitations.SearchFor(i => i.IdProfileInvites == idProfile && i.Status == 1);

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

        public void UpdateEventInvitation(EventInvitation invitation)
        {
            try
            {
                if (invitation != null)
                {
                    invitation.LastUpdate = DateTime.Now;
                    repoInvitations.Update(invitation);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ACCEPT EVENT INVITATION
        public EventResult AcceptEventInvitation(int idEvent, int idProfileAccepts)
        {
            try
            {
                //EventInvitation invitation = repoInvitations.SearchFor(i => i.EventID == idEvent && i.IdProfileInvites == idProfileSent && i.IdProfileInvited == idProfileAccepts).FirstOrDefault<EventInvitation>();
                EventInvitation invitation = repoInvitations.SearchFor(i => i.EventID == idEvent && i.IdProfileInvited == idProfileAccepts && i.Status == 1).FirstOrDefault<EventInvitation>();
                if (invitation != null)
                {
                    //Existe una invitacion
                    EventManager em = new EventManager();
                    EventResult result = em.JoinEvent(idProfileAccepts, idEvent);
                    if(result.ResultStatus == 1) //Se unio al evento con exito
                    {
                        invitation.Status = 2; //Invitacion aceptada
                        UpdateEventInvitation(invitation);
                    }
                    return result;
                }
                //return "Los datos ingresados no son correctos";
                return new EventResult(3, "Los datos ingresados no son correctos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public void InviteWholeGroup(int idEvent, int idGroup)
        {
            try
            {
                EventManager em = new EventManager();
                GroupManager gm = new GroupManager();
                Event eve = em.GetEventById(idEvent);
                Group grp = gm.GetGroupById(idGroup);
                if (eve != null && grp != null)
                {
                    EventInvitation invitation;
                    List<GroupMember> members = gm.GetMembers(idGroup);
                    foreach (GroupMember member in members)
                    {
                        invitation = repoInvitations.SearchFor(i => i.EventID == idEvent && i.IdProfileInvited == member.StandardProfileID).FirstOrDefault<EventInvitation>();
                        if (invitation == null)
                        {
                            //No tenia invitacion, se crea una nueva
                            string invitationMessage = "Fuiste invitado al evento " + eve.EventName + " por " + eve.CreatorProfile.FullName();
                            invitation = new EventInvitation(EventInvitation.InvitationType.Default, invitationMessage, eve.StandardProfileID, member.StandardProfileID, eve.ID);
                            InsertEventInvitation(invitation);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
