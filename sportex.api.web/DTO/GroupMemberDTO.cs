using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public class GroupMemberDTO
    {
        #region PROPERTIES
        public int StandardProfileID { get; set; }
        public StandardProfileDTO ProfileMember { get; set; }
        public int GroupID { get; set; }
        //public GroupDTO GroupIntegrates { get; set; }
        public int Type { get; set; }
        #endregion

        #region CONSTRUCTORS
        public GroupMemberDTO()
        {
            try
            {
                this.StandardProfileID = 0;
                this.ProfileMember = null;
                this.GroupID = 0;
                //this.GroupIntegrates = null;
                this.Type = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GroupMemberDTO(GroupMember gmember)
        {
            try
            {
                this.StandardProfileID = gmember.StandardProfileID;
                this.ProfileMember = new StandardProfileDTO(gmember.ProfileMember);
                this.GroupID = gmember.GroupID;
                //this.GroupIntegrates = new GroupDTO(gmember.GroupIntegrates);
                this.Type = gmember.Type;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public GroupMember MapFromDTO()
        {
            try
            {
                return new GroupMember(this.StandardProfileID, this.GroupID, this.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
