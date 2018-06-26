using sportex.api.domain;
using sportex.api.persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace sportex.api.logic
{
    public class LocationManager
    {
        IRepository<Location> repo;
        public LocationManager()
        {
            repo = new Repository<Location>();
        }
        public List<Location> GetAllLocations()
        {
            try
            {
                List<Location> locations = new List<Location>();
                locations = repo.GetAll();
                return locations;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Location GetLocationById(int id)
        {
            try
            {
                return repo.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertLocation(Location location)
        {
            try
            {
                repo.Insert(location);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteLocation(int id)
        {
            try
            {
                repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateLocation(Location location)
        {
            try
            {
                if (location != null)
                {
                    repo.Update(location);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateLocation(Location locationUpdated, Location newData)
        {
            try
            {
                if (locationUpdated != null && newData != null)
                {
                    locationUpdated.Name = newData.Name;
                    locationUpdated.Description = newData.Description;
                    locationUpdated.Latitude = newData.Latitude;
                    locationUpdated.Longitude = newData.Longitude;
                    locationUpdated.Status = newData.Status;
                    locationUpdated.LastUpdate = DateTime.Now;
                    repo.Update(locationUpdated);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
