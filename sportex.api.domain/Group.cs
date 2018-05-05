using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace sportex.api.domain
{
    public class Group
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public int StandardProfileID { get; set; }
        [ForeignKey("StandardProfileID")]
        public StandardProfile CreatorProfile { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string PicturePath { get; set; }
        public int MemberCount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }

        #region MAPPING
        [InverseProperty("GroupIntegrates")]
        public ICollection<GroupMember> GroupIntegrates { get; set; }
        #endregion

        #endregion

        #region CONSTRUCTORS
        public Group()
        {
            this.StandardProfileID = 0;
            this.CreatorProfile = null;
            this.GroupName = "";
            this.GroupDescription = "";
            this.PicturePath = "";
            this.MemberCount = 0;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }

        public Group(int profileId, string name, string desc, string path)
        {
            this.StandardProfileID = profileId;
            this.CreatorProfile = null;
            this.GroupName = name;
            this.GroupDescription = desc;
            this.PicturePath = path;
            this.MemberCount = 0;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }

        #endregion
    }
}
