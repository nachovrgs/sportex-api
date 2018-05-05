using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace sportex.api.domain
{
    public class GroupMember
    {
        #region PROPERTIES
        public int StandardProfileID { get; set; }
        [ForeignKey("StandardProfileID")]
        public StandardProfile ProfileMember { get; set; }
        public int GroupID { get; set; }
        [ForeignKey("GroupID")]
        public Group GroupIntegrates { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        #endregion

        #region CONSTRUCTORS
        public GroupMember()
        {
            this.StandardProfileID = 0;
            this.ProfileMember = null;
            this.GroupID = 0;
            this.GroupIntegrates = null;
            this.Type = 0;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }

        public GroupMember(int profileId, int groupId, int memType)
        {
            this.StandardProfileID = profileId;
            this.ProfileMember = null;
            this.GroupID = groupId;
            this.GroupIntegrates = null;
            this.Type = memType;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        #endregion

    }
}
