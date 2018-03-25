using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;

namespace sportex.api.web.Controllers
{
    [Produces("application/json")]
    [Route("api/Relationship")]
    public class RelationshipController : Controller
    {
        // GET: api/Relationship
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Relationship/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Relationship
        [HttpPost]
        public void Post([FromBody]Relationship relationship)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    RelationshipManager rm = new RelationshipManager();
                    rm.InsertRelationship(relationship);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        // PUT: api/Relationship/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("SendFriendRequest")]
        public string SendFriendRequest([FromBody]FriendRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RelationshipManager rm = new RelationshipManager();
                    return rm.SendFriendRequest(request.idSends, request.idReceives);
                }
                return "Los datos ingresados no son correctos";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("AcceptFriendRequest")]
        public string AcceptFriendRequest([FromBody]FriendRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RelationshipManager rm = new RelationshipManager();
                    return rm.AcceptFriendRequest(request.idSends, request.idReceives);
                }
                return "Los datos ingresados no son correctos";
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
