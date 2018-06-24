using System;
using System.Collections.Generic;
using System.Text;
using sportex.api.persistence;
using sportex.api.domain;
using System.Linq;

namespace sportex.api.logic
{
    public class AccountManager
    {
        IRepository<Account> repo;
        public AccountManager()
        {
            repo = new Repository<Account>();
        }
        public List<Account> GetAllAccounts()
        {
            try
            {
                List<Account> accounts = new List<Account>();
                accounts = repo.GetAll();
                return accounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Account GetAccountById(int id)
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
        public void InsertAccount(Account account)
        {
            try
            {
                repo.Insert(account);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public int ValidateAccount(String username, String password)
        {
            try
            {
                Account acc = repo.SearchFor(account => account.Username == username && account.Password == password).First();
                return acc != null ? acc.ID : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
