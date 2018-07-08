using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;
using sportex.api.web.DTO;

namespace sportex.api.web.Controllers
{
    [Produces("application/json")]
    [Route("api/standardProfile")]
    public class StandardProfileController : BaseController
    {
        // GET: api/<controller>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
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
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/<controller>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                StandardProfileManager spm = new StandardProfileManager();
                StandardProfile profile = spm.GetProfileById(id);
                if (profile != null)
                {
                    return Ok(new StandardProfileDTO(profile));
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/<controller>/5
        [Authorize]
        //[HttpGet("{id}")]
        [HttpGet]
        [Route("account/{id}")]
        public IActionResult GetByAccount(int id)
        {
            try
            {
                StandardProfileManager spm = new StandardProfileManager();
                StandardProfile profile = spm.GetProfileByAccountId(id);
                if (profile != null)
                {
                    return Ok(new StandardProfileDTO(profile));
                }
                else
                {
                    //Profile not found, returning a bad request.
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
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
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]StandardProfileDTO profileDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StandardProfileManager spm = new StandardProfileManager();
                    StandardProfile profile = spm.GetProfileById(id);
                    if (profile != null)
                    {
                        spm.UpdateProfile(profile, profileDTO.MapFromDTO());
                        return Ok(profile);
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                StandardProfileManager spm = new StandardProfileManager();
                spm.DeleteProfile(id);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }
    }
}
