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
        [InverseProperty("Profile1")]
        public ICollection<Relationship> Relationships1 { get; set; }
        [InverseProperty("Profile2")]
        public ICollection<Relationship> Relationships2 { get; set; }
        [InverseProperty("ProfileParticipant")]
        public ICollection<EventParticipant> ProfileParticipant { get; set; }


        #endregion

        #region CONSTRUCTORS

        public StandardProfile(int idAcc, Account account, string mail, string firstn, string lastn, string pic, DateTime birth, int sex) : base(idAcc, account, mail, firstn, lastn, pic)
        {
            this.DateOfBirth = birth;
            this.Sex = sex;
            Relationships1 = new List<Relationship>();
            Relationships2 = new List<Relationship>();
        }

        public StandardProfile() : base()
        {
            this.DateOfBirth = DateTime.Now;
            this.Sex = 1;
            Relationships1 = new List<Relationship>();
            Relationships2 = new List<Relationship>();
        }

        #endregion
    }
}
