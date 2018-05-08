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
        public double TotalRate { get; set; }
        public double CountReviews { get; set; }

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

        public StandardProfile(int idAcc, Account account, string mail, string firstn, string lastn, string pic, DateTime birth, int sex, double rate, double reviews) : base(idAcc, account, mail, firstn, lastn, pic)
        {
            this.DateOfBirth = birth;
            this.Sex = sex;
            this.TotalRate = rate;
            this.CountReviews = reviews;
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
            this.TotalRate = 0;
            this.CountReviews = 0;
            Relationships1 = new List<Relationship>();
            Relationships2 = new List<Relationship>();
            ProfileParticipant = new List<EventParticipant>();
            ProfileInvites = new List<EventInvitation>();
            ProfileInvited = new List<EventInvitation>();
            ProfileReviews = new List<PlayerReview>();
            ProfileReviewed = new List<PlayerReview>();
        }

        #endregion

        public double AverageRating()
        {
            return CountReviews > 0 ? TotalRate / CountReviews : 0;
        }
    }
}
