using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;
using sportex.api.web.DTO;

namespace sportex.api.web.Controllers
{
    [Produces("application/json")]
    [Route("api/Location")]
    public class LocationController : BaseController
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                LocationManager lm = new LocationManager();
                List<Location> listLocations = lm.GetAllLocations();
                List<LocationDTO> listDTOs = new List<LocationDTO>();
                foreach (Location location in listLocations)
                {
                    listDTOs.Add(new LocationDTO(location));
                }
                return Ok(listDTOs);
            }
            catch (Exception ex)
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
                LocationManager lm = new LocationManager();
                Location location = lm.GetLocationById(id);
                if (location != null)
                {
                    return Ok(new LocationDTO(location));
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
        public IActionResult Put(int id, [FromBody]LocationDTO locationDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LocationManager lm = new LocationManager();
                    Location location = lm.GetLocationById(id);
                    if (location != null)
                    {
                        lm.UpdateLocation(location, locationDTO.MapFromDTO());
                        return Ok(location);
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
                LocationManager lm = new LocationManager();
                lm.DeleteLocation(id);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]LocationDTO locationDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (locationDTO != null)
                    {
                        Location location = locationDTO.MapFromDTO();
                        LocationManager lm = new LocationManager();
                        lm.InsertLocation(location);
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
