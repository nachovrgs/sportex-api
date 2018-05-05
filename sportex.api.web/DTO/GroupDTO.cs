using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public class GroupDTO
    {
        #region PROPERTIES
        public int ID { get; set; }
        public int StandardProfileID { get; set; }
        public StandardProfileDTO CreatorProfile { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string PicturePath { get; set; }
        public int MemberCount { get; set; }
        public List<GroupMemberDTO> ListMembers { get; set; }

        #endregion

        #region CONSTRUCTORS
        public GroupDTO()
        {
            try
            {
                this.StandardProfileID = 0;
                this.CreatorProfile = null;
                this.GroupName = "";
                this.GroupDescription = "";
                this.PicturePath = "";
                this.MemberCount = 0;
                this.ListMembers = new List<GroupMemberDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GroupDTO(Group grp)
        {
            try
            {
                this.ID = grp.ID;
                this.StandardProfileID = grp.StandardProfileID;
                this.CreatorProfile = new StandardProfileDTO(grp.CreatorProfile);
                this.GroupName = grp.GroupName;
                this.GroupDescription = grp.GroupDescription;
                this.PicturePath = grp.PicturePath;
                this.MemberCount = grp.MemberCount;
                this.ListMembers = new List<GroupMemberDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public Group MapFromDTO()
        {
            try
            {
                return new Group(this.StandardProfileID, this.GroupName, this.GroupDescription, this.PicturePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
