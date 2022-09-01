using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [Authorize]
        [HttpPost("CreateCollab")]
        public IActionResult AddCollab(long noteid, string email)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "UserId").Value);
                var result = collabBL.AddCollab(noteid, userid, email);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Collaborator Added Successfully",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to add"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult Remove(long noteid, string email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = collabBL.Remove(noteid, email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborater Email deleted", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Collaborater Email not deleted" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("ReadCollab")]
        public IActionResult ReadCollab(long colabId)
        {
            try
            {

                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = collabBL.ReadCollab(colabId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "COLLABRATION EMAIL RECIEVED", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "COLLABRATION RECIEVED FAILED" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
