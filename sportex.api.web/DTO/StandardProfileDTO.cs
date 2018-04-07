using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public class StandardProfileDTO : BaseProfileDTO
    {
        #region PROPERTIES
        public DateTime DateOfBirth { get; set; }
        public int Sex { get; set; }


        #endregion

        public StandardProfileDTO()
        {
            try
            {
                this.ID = 0;
                this.AccountID = 0;
                this.Account = null;
                this.MailAddress = "";
                this.FirstName = "";
                this.LastName = "";
                this.PicturePath = "";
                this.DateOfBirth = new DateTime();
                this.Sex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StandardProfileDTO(StandardProfile profile)
        {
            try
            {
                this.ID = profile.ID;
                this.AccountID = profile.AccountID;
                this.Account = new AccountDTO(profile.Account);
                this.MailAddress = profile.MailAddress;
                this.FirstName = profile.FirstName;
                this.LastName = profile.LastName;
                this.PicturePath = profile.PicturePath;
                this.DateOfBirth = profile.DateOfBirth;
                this.Sex = profile.Sex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StandardProfile MapFromDTO()
        {
            try
            {
                return new StandardProfile(this.AccountID, this.Account.MapFromDTO(), this.MailAddress, this.FirstName, this.LastName, this.PicturePath, this.DateOfBirth, this.Sex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
