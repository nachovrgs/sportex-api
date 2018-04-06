using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public abstract class BaseProfileDTO
    {
        #region PROPERTIES
        public int ID { get; set; }
        public int AccountID { get; set; }
        public AccountDTO Account { get; set; }
        public string MailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PicturePath { get; set; }

        #endregion

    }
}
