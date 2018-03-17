using System;
using System.Collections.Generic;
using System.Text;

namespace sportex.api.domain
{
    public class AdminProfile : BaseProfile
    {
        #region PROPERTIES
        public List<AdminRole> Roles { get; set; }

        #endregion

        #region CONSTRUCTORS

        public AdminProfile(Account account, string mail, string firstn, string lastn, string pic) : base(account, mail, firstn, lastn, pic)
        {
            this.Roles = new List<AdminRole>();
        }

        public AdminProfile() : base()
        {
            this.Roles = new List<AdminRole>();
        }

        #endregion
    }
}
