using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserAPI.Domain
{
    public class BaseProfile
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public int ProfileId { get; set; }
        public User User { get; set; }
        public string MailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PicturePath { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }

        #endregion

        #region CONSTRUCTORS
        public BaseProfile(User user, string mail, string firstn, string lastn, string pic, int stat, DateTime created, DateTime update)
        {
            this.User = user;
            this.MailAddress = mail;
            this.FirstName = firstn;
            this.LastName = lastn;
            this.PicturePath = pic;
            this.Status = stat;
            this.CreatedOn = created;
            this.LastUpdate = update;
        }
        public BaseProfile()
        {
            this.User = null;
            this.MailAddress = "";
            this.FirstName = "";
            this.LastName = "";
            this.PicturePath = "";
            this.Status = 0;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = DateTime.Now;
        }
        public BaseProfile(User user, string mail, string firstn, string lastn, string pic)
        {
            this.User = user;
            this.MailAddress = mail;
            this.FirstName = firstn;
            this.LastName = lastn;
            this.PicturePath = pic;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = DateTime.Now;
        }

        #endregion
    }
}
