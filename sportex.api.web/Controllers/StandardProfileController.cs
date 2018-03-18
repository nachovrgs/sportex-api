using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;

namespace sportex.api.web.Controllers
{
    [Produces("application/json")]
    [Route("api/StandardProfile")]
    public class StandardProfileController : BaseController
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<StandardProfile> Get()
        {
            //return new string[] { "value1", "value2" };
            try
            {
                StandardProfileManager spm = new StandardProfileManager();
                return spm.GetAllProfiles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public StandardProfile Get(int id)
        {
            try
            {
                StandardProfileManager spm = new StandardProfileManager();
                return spm.GetProfileById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]StandardProfile profile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (profile != null)
                    {
                        StandardProfileManager spm = new StandardProfileManager();
                        spm.InsertProfile(profile);
                    }
                }
                else
                {
                    //error
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/StandardProfile/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}