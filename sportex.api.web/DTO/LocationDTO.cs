using sportex.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sportex.api.web.DTO
{
    public class LocationDTO
    {
        #region PROPERTIES
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        #endregion

        public LocationDTO()
        {
            try
            {
                this.ID = 0;
                this.Name = "";
                this.Description = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LocationDTO(Location location)
        {
            try
            {
                this.ID = location.ID;
                this.Name = location.Name;
                this.Description = location.Description;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Location MapFromDTO()
        {
            try
            {
                return new Location(this.Name, this.Description);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
