using System;
using System.Collections.Generic;
using System.Text;
using sportex.api.persistence;
using sportex.api.domain;
using System.Linq;

namespace sportex.api.logic
{
    public class UserManager
    {
        IRepository<User> repo;
        public UserManager()
        {
            repo = new Repository<User>();
        }
        public List<User> GetAllUsers()
        {
            try
            {
                List<User> users = new List<User>();
                users = repo.GetAll();
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertUser(User user)
        {
            try
            {
                repo.Insert(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
