using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace sportex.api.domain
{
    public class AdminRole
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        #endregion

        #region CONSTRUCTOR
        public AdminRole()
        {
            this.Name = "undefined";
            this.Description = "";
        }
        #endregion
    }
}
