using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace sportex.api.domain
{

    public class Relationship
    {
        #region PROPERTIES
        //[Key]
        //public int ID { get; set; }
        public int Status { get; set; } //1-Amigos, 2-Solicitud Enviada, 3-Solicitud Recibida, 4-Solicitud Rechazada, 5-Bloqueado
        public int IdProfile1 { get; set; }
        [ForeignKey("IdProfile1")]
        public StandardProfile Profile1 { get; set; }       
        public int IdProfile2 { get; set; }
        [ForeignKey("IdProfile2")]
        public StandardProfile Profile2 { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        #endregion

        #region CONSTRUCTORS
        public Relationship(int stat, StandardProfile profile1, StandardProfile profile2, DateTime created, DateTime updated)
        {
            this.Status = stat;
            this.IdProfile1 = profile1.ID;
            this.IdProfile2 = profile2.ID;
            this.Profile1 = profile1;
            this.Profile2 = Profile2;
            this.CreatedOn = created;
            this.LastUpdate = updated;
        }
        public Relationship()
        {
            this.Status = 0;
            this.IdProfile1 = 0;
            this.IdProfile2 = 0;
            this.Profile1 = null;
            this.Profile2 = null;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        public Relationship(int stat, StandardProfile profile1, StandardProfile profile2)
        {
            this.Status = stat;
            this.IdProfile1 = profile1.ID;
            this.IdProfile2 = profile2.ID;
            this.Profile1 = profile1;
            this.Profile2 = profile2;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        public Relationship(RelationshipStatus stat, StandardProfile profile1, StandardProfile profile2)
        {
            this.Status = (int)stat;
            this.IdProfile1 = profile1.ID;
            this.IdProfile2 = profile2.ID;
            this.Profile1 = profile1;
            this.Profile2 = profile2;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        public Relationship(RelationshipStatus stat, int profile1, int profile2)
        {
            this.Status = (int)stat;
            this.IdProfile1 = profile1;
            this.IdProfile2 = profile2;
            this.Profile1 = null;
            this.Profile2 = null;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        #endregion
        public enum RelationshipStatus
        {
            Default,
            Friends,
            RequestSent,
            RequestReceived,
            RequestDeclined,
            Blocked
        };

        



    }
}
