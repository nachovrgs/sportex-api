using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sportex.api.domain;
using sportex.api.logic;
using sportex.api.web.DTO;

namespace sportex.api.web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Group")]
    public class GroupController : BaseController
    {
        // GET: api/Group
        [HttpGet]
        public IEnumerable<GroupDTO> Get()
        {
            try
            {
                GroupManager gm = new GroupManager();
                List<Group> listGroups = gm.GetAllGroups();
                List<GroupDTO> listDTOs = new List<GroupDTO>();
                foreach (Group grp in listGroups)
                {
                    listDTOs.Add(new GroupDTO(grp));
                }
                return listDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/Group/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet("{id}")]
        public GroupDTO Get(int id)
        {
            try
            {
                GroupManager gm = new GroupManager();
                Group grp = gm.GetGroupById(id);
                if (grp != null)
                {
                    GroupDTO dto = new GroupDTO(grp);
                    //cargar los miembros
                    LoadMembers(dto, gm);
                    return dto;
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

        // POST: api/Group
        [HttpPost]
        public IActionResult Post([FromBody]GroupDTO GroupDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (GroupDTO != null)
                    {
                        Group grp = GroupDTO.MapFromDTO();
                        GroupManager gm = new GroupManager();
                        gm.InsertGroup(grp);
                        return StatusCode(200);
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

        // PUT: api/Group/5
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
        [Route("JoinGroup")]
        public IActionResult JoinGroup([FromBody]GroupRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GroupManager gm = new GroupManager();
                    gm.JoinGroup(request.idProfile, request.idGroup);
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
        [Route("LeaveGroup")]
        public IActionResult LeaveGroup([FromBody]GroupRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GroupManager gm = new GroupManager();
                    gm.LeaveGroup(request.idProfile, request.idGroup);
                    return StatusCode(200);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadMembers(GroupDTO dto, GroupManager gm)
        {
            try
            {
                List<GroupMember> members = gm.GetMembersWithType(dto.ID, 1);
                foreach (GroupMember member in members)
                {
                    dto.ListMembers.Add(new GroupMemberDTO(member));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public class GroupRequest
        {
            public int idProfile { get; set; }
            public int idGroup { get; set; }
        }
    }
}
