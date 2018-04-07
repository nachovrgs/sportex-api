using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;
using sportex.api.web.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sportex.api.web.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<AccountDTO> Get()
        {
            try
            {
                AccountManager am = new AccountManager();
                List<Account> listAccounts = am.GetAllAccounts();
                List<AccountDTO> listDTOs = new List<AccountDTO>();
                foreach(Account account in listAccounts)
                {
                    listDTOs.Add(new AccountDTO(account));
                }
                return listDTOs;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public AccountDTO Get(int id)
        {
            try
            {
                AccountManager am = new AccountManager();
                Account account = am.GetAccountById(id);
                if(account!=null)
                {
                    return new AccountDTO(account);
                }
                else
                {
                    //mostrar error
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
