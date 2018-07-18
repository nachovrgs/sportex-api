using sportex.api.domain;
using sportex.api.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sportex.api.logic
{
    public class GroupManager
    {
        #region PROPERTIES
        private IRepository<Group> repoGroups;
        private IRepository<GroupMember> repoMembers;
        public GroupManager()
        {
            repoGroups = new Repository<Group>();
            repoMembers = new Repository<GroupMember>();
        }
        #endregion

        #region GETS
        public List<Group> GetAllGroups()
        {
            try
            {
                List<Group> groups = new List<Group>();
                groups = repoGroups.GetAll();
                StandardProfileManager spm = new StandardProfileManager();
                foreach (Group grp in groups)
                {
                    grp.CreatorProfile = spm.GetProfileById(grp.StandardProfileID);
                }

                return groups;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Group GetGroupById(int id)
        {
            try
            {
                Group grp = repoGroups.GetById(id);
                if (grp != null)
                {
                    StandardProfileManager spm = new StandardProfileManager();
                    grp.CreatorProfile = spm.GetProfileById(grp.StandardProfileID);
                }
                return grp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Group> GetGroupsJoinedByProfile(int id)
        {
            try
            {
                List<Group> groups = new List<Group>();
                List<GroupMember> members = GetGroupMemberships(id);
                Group grp;
                foreach (GroupMember member in members)
                {
                    grp = repoGroups.GetById(member.GroupID);
                    if (grp.Status == 1)
                    {
                        groups.Add(grp);
                    }
                }
                StandardProfileManager spm = new StandardProfileManager();
                foreach (Group grp2 in groups)
                {
                    grp2.CreatorProfile = spm.GetProfileById(grp2.StandardProfileID);
                }
                return groups;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GroupMember> GetGroupMemberships(int id)
        {
            try
            {
                return repoMembers.SearchFor(p => p.StandardProfileID == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region INSERTS
        public void InsertGroup(Group grp)
        {
            try
            {
                grp.Status = 1;
                grp.CreatedOn = DateTime.Now;
                grp.LastUpdate = grp.CreatedOn;
                repoGroups.Insert(grp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertMember(GroupMember member)
        {
            try
            {
                member.Status = 1;
                member.CreatedOn = DateTime.Now;
                member.LastUpdate = member.CreatedOn;
                repoMembers.Insert(member);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertManyMembers(List<int> idMembers, int idGroup)
        {
            try
            {
                foreach(int id in idMembers)
                {
                    JoinGroup(id, idGroup);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UPDATES
        public void UpdateGroup(Group groupUpdated, Group newData)
        {
            try
            {
                if (groupUpdated != null && newData != null)
                {
                    groupUpdated.GroupName = newData.GroupName;
                    groupUpdated.GroupDescription = newData.GroupDescription;
                    groupUpdated.PicturePath = newData.PicturePath;
                    groupUpdated.MemberCount = newData.MemberCount;
                    groupUpdated.StandardProfileID = newData.StandardProfileID;
                    groupUpdated.Status = newData.Status;
                    UpdateGroup(groupUpdated);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateGroup(Group grp)
        {
            try
            {
                if (grp != null)
                {
                    grp.LastUpdate = DateTime.Now;
                    repoGroups.Update(grp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateMember(GroupMember member)
        {
            try
            {
                if (member != null)
                {
                    repoMembers.Update(member);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DELETES
        public void DeleteGroup(int id)
        {
            try
            {
                foreach (GroupMember member in GetMembers(id))
                {
                    repoMembers.Delete(member);
                }
                repoGroups.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region JOIN Group
        public string JoinGroup(int idProfile, int idGroup)
        {
            try
            {
                IRepository<StandardProfile> repoProfile = new Repository<StandardProfile>();
                StandardProfile profile = repoProfile.GetById(idProfile);
                if (profile == null) return "No existe un perfil con ese ID";
                else
                {
                    Group grp = repoGroups.GetById(idGroup);
                    if (grp == null) return "No existe un grupo con ese ID";
                    else if (ProfileIsMember(idGroup, idProfile)) return profile.FirstName + " " + profile.LastName + " ya pertenece al grupo " + grp.GroupName + ".";
                    else
                    {
                        //Ingresa al grupo
                        GroupMember newMember = new GroupMember(profile.ID, grp.ID, 1);
                        InsertMember(newMember);
                        grp.MemberCount += 1;
                        UpdateGroup(grp);
                        return profile.FirstName + " " + profile.LastName + " ha ingresado al grupo " + grp.GroupName + ".";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region LEAVE Group
        public string LeaveGroup(int idProfile, int idGroup)
        {
            try
            {
                IRepository<StandardProfile> repoProfile = new Repository<StandardProfile>();
                StandardProfile profile = repoProfile.GetById(idProfile);
                if (profile == null) return "No existe un perfil con ese ID";
                else
                {
                    Group grp = repoGroups.GetById(idGroup);
                    if (grp == null) return "No existe un grupo con ese ID";
                    else
                    {
                        GroupMember member = repoMembers.SearchFor(m => m.GroupID == grp.ID && m.StandardProfileID == profile.ID).FirstOrDefault<GroupMember>();
                        if (member == null) return "El perfil no pertenece al grupo.";
                        else
                        {
                            //Quito al perfil del grupo
                            repoMembers.Delete(member);
                            grp.MemberCount -= 1;
                            UpdateGroup(grp);

                            //------
                            //NOTIFICAR AL Grupo
                            //------
                            return profile.FirstName + " " + profile.LastName + " se ha salido del Grupo " + grp.GroupName + ".";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region MemberS METHODS

        public List<GroupMember> GetMembers(int idGroup)
        {
            try
            {
                List<GroupMember> members = repoMembers.SearchFor(m => m.GroupID == idGroup);
                StandardProfileManager spm = new StandardProfileManager();
                foreach (GroupMember member in members)
                {
                    member.ProfileMember = spm.GetProfileById(member.StandardProfileID);
                }
                return members;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GroupMember> GetMembersWithType(int idGroup, int type)
        {
            try
            {
                List<GroupMember> members = repoMembers.SearchFor(m => m.GroupID == idGroup && m.Type == type);
                StandardProfileManager spm = new StandardProfileManager();
                //GroupManager gm = new GroupManager();
                foreach (GroupMember member in members)
                {
                    member.ProfileMember = spm.GetProfileById(member.StandardProfileID);
                    //member.GroupIntegrates = gm.GetGroupById(member.GroupID);
                }
                return members;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StandardProfile> GetMembersProfiles(int idGroup)
        {
            try
            {
                List<StandardProfile> profiles = new List<StandardProfile>();
                List<GroupMember> members = GetMembersWithType(idGroup, 1);
                StandardProfileManager spm = new StandardProfileManager();
                foreach (GroupMember member in members)
                {
                    profiles.Add(spm.GetProfileById(member.StandardProfileID));
                }
                return profiles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ProfileIsMember(int idGroup, int idProfile)
        {
            try
            {
                GroupMember member = repoMembers.SearchFor(m => m.GroupID == idGroup && m.StandardProfileID == idProfile && m.Type > 0).FirstOrDefault<GroupMember>();
                return member != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
