using sportex.api.domain;
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

                //IRepository<Account> repoAccount = new Repository<Account>();
                foreach (StandardProfile prof in profiles)
                {
                    //prof.Account = repoAccount.GetById(prof.AccountID);
                    AccountManager am = new AccountManager();
                    prof.Account = am.GetAccountById(prof.AccountID);
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
                StandardProfile profile = repo.GetById(id);
                //IRepository<Account> repoAccount = new Repository<Account>();
                //profile.Account = repoAccount.GetById(profile.AccountID);
                if (profile != null)
                {
                    AccountManager am = new AccountManager();
                    profile.Account = am.GetAccountById(profile.AccountID);
                }
                return profile;
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
        public void UpdateProfile(StandardProfile profile)
        {
            try
            {
                if (profile != null)
                {
                    repo.Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RateProfile(StandardProfile profile, int newRate)
        {
            try
            {
                if (profile != null)
                {
                    profile.CountReviews++;
                    profile.TotalRate += newRate;
                    UpdateProfile(profile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
