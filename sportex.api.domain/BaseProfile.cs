using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace sportex.api.domain
{
    public class BaseProfile
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public int AccountID { get; set; }
        [ForeignKey("AccountID")]
        public Account Account { get; set; }
        public string MailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PicturePath { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }

        #endregion

        #region CONSTRUCTORS
        public BaseProfile(int idAcc, Account account, string mail, string firstn, string lastn, string pic, int stat, DateTime created, DateTime update)
        {
            this.AccountID = idAcc;
            this.Account = account;
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
            this.AccountID = 0;
            this.Account = null;
            this.MailAddress = "";
            this.FirstName = "";
            this.LastName = "";
            this.PicturePath = "";
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        public BaseProfile(int idAcc, Account account, string mail, string firstn, string lastn, string pic)
        {
            this.AccountID = idAcc;
            this.Account = account;
            this.MailAddress = mail;
            this.FirstName = firstn;
            this.LastName = lastn;
            this.PicturePath = pic;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }

        #endregion
    }
}
