using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sportex.api.web.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            //return new string[] { "value1", "value2" };
            try
            {
                AccountManager am = new AccountManager();
                return am.GetAllAccounts();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Account account)
        {
            try
            {
                AccountManager am = new AccountManager();
                am.InsertAccount(account);
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
