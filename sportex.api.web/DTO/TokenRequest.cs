using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public class TokenRequest
    {
        #region PROPERTIES
        public string Username { get; set; }
        public string Password { get; set; }
        #endregion

        public TokenRequest()
        {
            try
            {
                this.Username = "";
                this.Password = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
