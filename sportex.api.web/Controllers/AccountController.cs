using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;
using sportex.api.web.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sportex.api.web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
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
                return Ok(listDTOs);
            }
            catch(Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                AccountManager am = new AccountManager();
                Account account = am.GetAccountById(id);
                if(account!=null)
                {
                    return Ok(new AccountDTO(account));
                }
                else
                {
                    //mostrar error
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AccountDTO accountDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AccountManager am = new AccountManager();
                    Account account = am.GetAccountById(id);
                    if (account != null)
                    {
                        am.UpdateAccount(account, accountDTO.MapFromDTO());
                        return Ok(account);
                    }
                    else
                    {
                        //mostrar error
                        return StatusCode(400);
                    }
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                AccountManager am = new AccountManager();
                am.DeleteAccount(id);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]AccountDTO accountDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (accountDTO != null)
                    {
                        Account account = accountDTO.MapFromDTO();
                        AccountManager am = new AccountManager();
                        am.InsertAccount(account);
                        return StatusCode(201);
                    }
                    return StatusCode(400);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }
    }
}
