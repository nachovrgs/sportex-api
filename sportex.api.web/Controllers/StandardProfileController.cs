using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;
using sportex.api.web.DTO;

namespace sportex.api.web.Controllers
{
    [Produces("application/json")]
    [Route("api/StandardProfile")]
    public class StandardProfileController : BaseController
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<StandardProfileDTO> Get()
        {
            try
            {
                StandardProfileManager spm = new StandardProfileManager();
                List<StandardProfile> listProfiles = spm.GetAllProfiles();
                List<StandardProfileDTO> listDTOs = new List<StandardProfileDTO>();
                foreach (StandardProfile profile in listProfiles)
                {
                    listDTOs.Add(new StandardProfileDTO(profile));
                }
                return listDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public StandardProfileDTO Get(int id)
        {
            try
            {
                StandardProfileManager spm = new StandardProfileManager();
                StandardProfile profile = spm.GetProfileById(id);
                if (profile != null)
                {
                    return new StandardProfileDTO(profile);
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

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]StandardProfileDTO profileDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (profileDTO != null)
                    {
                        StandardProfile profile = profileDTO.MapFromDTO();
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