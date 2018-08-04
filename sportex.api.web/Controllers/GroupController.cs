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
    [Route("api/group")]
    public class GroupController : BaseController
    {
        // GET: api/Group
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                GroupManager gm = new GroupManager();
                List<Group> listGroups = gm.GetAllGroups();
                List<GroupDTO> listDTOs = new List<GroupDTO>();
                GroupDTO dto;
                foreach (Group grp in listGroups)
                {
                    dto = new GroupDTO(grp);
                    LoadMembers(dto, gm);
                    listDTOs.Add(dto);
                }
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // GET: api/Group/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
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
                    return Ok(dto);
                }
                else
                {
                    //mostrar error
                    //return null;
                    //throw ex;
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("Joined/{idProfile}")]
        public IActionResult GetGroupsJoined(int idProfile)
        {
            try
            {
                GroupManager gm = new GroupManager();
                List<Group> listGroups = gm.GetGroupsJoinedByProfile(idProfile);
                List<GroupDTO> listDTOs = new List<GroupDTO>();
                GroupDTO dto;
                foreach (Group grp in listGroups)
                {
                    dto = new GroupDTO(grp);
                    LoadMembers(dto, gm);
                    listDTOs.Add(dto);
                }
                return Ok(listDTOs);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
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
                        gm.JoinGroup(grp.StandardProfileID, grp.ID);
                        return Ok(grp.ID);
                        //return StatusCode(200);
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

        // PUT: api/Group/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]GroupDTO groupDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (groupDTO != null)
                    {
                        GroupManager gm = new GroupManager();
                        Group updated = gm.GetGroupById(id);
                        if (updated != null)
                        {
                            Group grp = groupDTO.MapFromDTO();
                            gm.UpdateGroup(updated, grp);
                            return StatusCode(200);
                        }
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                GroupManager gm = new GroupManager();
                gm.DeleteGroup(id);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Route("updateImage/{id}")]
        public async Task<IActionResult> PostImage(int id, [FromBody] string image)
        {
            try
            {
                GroupManager gm = new GroupManager();
                Group updated = gm.GetGroupById(id);

                if (updated != null)
                {
                    await gm.ProcessImageAsync(updated, image, id);
                    return Ok(updated);
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

        #region JOIN/LEAVE

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
                //throw ex;
                return StatusCode(500);
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
                //throw ex;
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("InsertManyMembers")]
        public IActionResult InsertManyMembers([FromBody]GroupRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GroupManager gm = new GroupManager();
                    gm.InsertManyMembers(request.listIdProfiles, request.idGroup);
                    return StatusCode(200);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(500);
            }
        }
        #endregion

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
            public List<int> listIdProfiles { get; set; }
        }
    }
}
