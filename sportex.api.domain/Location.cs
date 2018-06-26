using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace sportex.api.domain
{
    public class Location
    {
        #region PROPERTIES
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Latitude { get; set; }
        public int? Longitude { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }

        #endregion

        #region CONSTRUCTORS
        public Location(string name, string desc, int? lat, int? lon)
        {
            this.Name = name;
            this.Description = desc;
            this.Latitude = lat;
            this.Longitude = lon;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        public Location()
        {
            this.Name = "";
            this.Description = "";
            this.Latitude = null;
            this.Longitude = null;
            this.Status = 1;
            this.CreatedOn = DateTime.Now;
            this.LastUpdate = this.CreatedOn;
        }
        #endregion
    }
}
