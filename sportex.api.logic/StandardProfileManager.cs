﻿using sportex.api.domain;
using sportex.api.persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace sportex.api.logic
{
    public class StandardProfileManager
    {
        private IRepository<StandardProfile> repo;
        public StandardProfileManager()
        {
            repo = new Repository<StandardProfile>();
        }
        public List<StandardProfile> GetAllProfiles()
        {
            try
            {
                List<StandardProfile> profiles = new List<StandardProfile>();
                profiles = repo.GetAll();

                IRepository<Account> repoAccount = new Repository<Account>();
                foreach (StandardProfile prof in profiles)
                {
                    prof.Account = repoAccount.GetById(prof.AccountID);
                }

                return profiles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StandardProfile GetProfileById(int id)
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
        public void InsertProfile(StandardProfile profile)
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