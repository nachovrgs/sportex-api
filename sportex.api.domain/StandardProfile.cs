using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace sportex.api.domain
{
    public class StandardProfile : BaseProfile
    {
        #region PROPERTIES

        public DateTime DateOfBirth { get; set; }
        public int Sex { get; set; }

        #region MAPPING
        [InverseProperty("Profile1")]
        public ICollection<Relationship> Relationships1 { get; set; }
        [InverseProperty("Profile2")]
        public ICollection<Relationship> Relationships2 { get; set; }
        [InverseProperty("ProfileParticipant")]
        public ICollection<EventParticipant> ProfileParticipant { get; set; }
        [InverseProperty("ProfileInvites")]
        public ICollection<EventInvitation> ProfileInvites { get; set; }
        [InverseProperty("ProfileInvited")]
        public ICollection<EventInvitation> ProfileInvited { get; set; }
        [InverseProperty("ProfileMember")]
        public ICollection<GroupMember> ProfileMember { get; set; }
        [InverseProperty("ProfileReviews")]
        public ICollection<PlayerReview> ProfileReviews { get; set; }
        [InverseProperty("ProfileReviewed")]
        public ICollection<PlayerReview> ProfileReviewed { get; set; }

        #endregion


        #endregion

        #region CONSTRUCTORS

        public StandardProfile(int idAcc, Account account, string mail, string firstn, string lastn, string pic, DateTime birth, int sex) : base(idAcc, account, mail, firstn, lastn, pic)
        {
            this.DateOfBirth = birth;
            this.Sex = sex;
            Relationships1 = new List<Relationship>();
            Relationships2 = new List<Relationship>();
            ProfileParticipant = new List<EventParticipant>();
            ProfileInvites = new List<EventInvitation>();
            ProfileInvited = new List<EventInvitation>();
            ProfileReviews = new List<PlayerReview>();
            ProfileReviewed = new List<PlayerReview>();
        }

        public StandardProfile() : base()
        {
            this.DateOfBirth = DateTime.Now;
            this.Sex = 1;
            Relationships1 = new List<Relationship>();
            Relationships2 = new List<Relationship>();
            ProfileParticipant = new List<EventParticipant>();
            ProfileInvites = new List<EventInvitation>();
            ProfileInvited = new List<EventInvitation>();
            ProfileReviews = new List<PlayerReview>();
            ProfileReviewed = new List<PlayerReview>();
        }

        #endregion
    }
}
