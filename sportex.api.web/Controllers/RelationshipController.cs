using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;

namespace sportex.api.web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Relationship")]
    public class RelationshipController : BaseController
    {
        // GET: api/Relationship
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Relationship/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Relationship
        [HttpPost]
        public IActionResult Post([FromBody]Relationship relationship)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    RelationshipManager rm = new RelationshipManager();
                    rm.InsertRelationship(relationship);
                    return StatusCode(201);
                }
                return StatusCode(400);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        // PUT: api/Relationship/5
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

        [HttpPost]
        [Route("SendFriendRequest")]
        public IActionResult SendFriendRequest([FromBody]FriendRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RelationshipManager rm = new RelationshipManager();
                    rm.SendFriendRequest(request.idSends, request.idReceives);
                    return StatusCode(200);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("AcceptFriendRequest")]
        public IActionResult AcceptFriendRequest([FromBody]FriendRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RelationshipManager rm = new RelationshipManager();
                    rm.AcceptFriendRequest(request.idSends, request.idReceives);
                    return StatusCode(200);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public class FriendRequest
        {
            public int idSends { get; set; }
            public int idReceives { get; set; }
        }
    }
}
