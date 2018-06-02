using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;
using sportex.api.web.DTO;

namespace sportex.api.web.Controllers
{
    [Authorize]
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
        public IActionResult Post([FromBody]StandardProfileDTO profileDTO)
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
                throw ex;
            }
        }

        // PUT: api/StandardProfile/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return StatusCode(403);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return StatusCode(403);
        }
    }
}