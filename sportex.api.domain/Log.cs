using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace sportex.api.domain
{
    public class Log
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public int Type { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public DateTime CreatedOn { get; set; }
        #endregion

        public Log(int type, string message, string details)
        {
            this.Type = type;
            this.Message = message;
            this.Details = details;
            this.CreatedOn = DateTime.Now;
        }
        public Log()
        {
            this.Type = 0;
            this.Message = "";
            this.Details = "";
            this.CreatedOn = DateTime.Now;
        }

    }
}
