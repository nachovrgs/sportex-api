using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public class AccountDTO
    {
        #region PROPERTIES
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        #endregion

        public AccountDTO()
        {
            try
            {
                this.ID = 0;
                this.Username = "";
                this.Password = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AccountDTO(Account account)
        {
            try
            {
                this.ID = account.ID;
                this.Username = account.Username;
                this.Password = account.Password;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Account MapFromDTO()
        {
            try
            {
                return new Account(this.Username, this.Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
