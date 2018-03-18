using sportex.api.domain;
using sportex.api.persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace sportex.api.logic
{
    public class AdminProfileManager
    {
        private IRepository<AdminProfile> repo;
        public AdminProfileManager()
        {
            repo = new Repository<AdminProfile>();
        }
        public List<AdminProfile> GetAllProfiles()
        {
            try
            {
                List<AdminProfile> users = new List<AdminProfile>();
                users = repo.GetAll();
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AdminProfile GetProfileById(int id)
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
        public void InsertProfile(AdminProfile profile)
        {
            try
            {
                profile.Status = 1;
                profile.CreatedOn = DateTime.Now;
                profile.LastUpdate = profile.CreatedOn;
                repo.Insert(profile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
