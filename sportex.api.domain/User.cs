using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace sportex.api.domain
{
    public class User
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime LastAccess { get; set; }

        #endregion

        #region CONSTRUCTORS
        public User(string uname, string pass, int stat, DateTime created, DateTime updated, DateTime access)
        {
            this.Username = uname;
            this.Password = pass;
            this.Status = stat;
            this.CreatedOn = created;
            this.LastUpdate = updated;
            this.LastAccess = access;
        }
        public User()
        {
            this.Username = "undefined";
            this.Password = "";
            this.Status = 0;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = DateTime.Now;
            this.LastAccess = DateTime.Now;
        }
        public User(string uname, string pass)
        {
            this.Username = uname;
            this.Password = pass;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = DateTime.Now;
            this.LastAccess = DateTime.Now;
        }

        #endregion
    }
}
