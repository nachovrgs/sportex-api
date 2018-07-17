using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace sportex.api.domain
{
    public class Account
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public string Token { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime LastAccess { get; set; }

        #endregion

        #region CONSTRUCTORS
        public Account(string uname, string pass, int stat, string tok, DateTime created, DateTime updated, DateTime access)
        {
            this.Username = uname;
            this.Password = pass;
            this.Status = stat;
            this.Token = tok;
            this.CreatedOn = created;
            this.LastUpdate = updated;
            this.LastAccess = access;
        }
        public Account()
        {
            this.Username = "undefined";
            this.Password = "";
            this.Status = 0;
            this.Token = "";
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = DateTime.Now;
            this.LastAccess = DateTime.Now;
        }
        public Account(string uname, string pass)
        {
            this.Username = uname;
            this.Password = pass;
            this.Status = 1;
            this.Token = "";
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = DateTime.Now;
            this.LastAccess = DateTime.Now;
        }

        #endregion
    }
}
