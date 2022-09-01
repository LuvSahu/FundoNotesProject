using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
       private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddLabel(long noteid, string labelname)
        {
            try
            {
                long userid = Convert.ToInt64(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.AddLabel(userid, noteid, labelname);
                if (result != null)
                {
                    return Ok(new
                    {
                        Success = true,
                        message = "Label Added Sucessfully",
                        data = result
                    });
                }
                else
                {
                    return BadRequest(new
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
        [HttpGet("Read")]
        public IActionResult ReadLabel(long labelId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.ReadLabel(labelId, UserId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "LABEL RECIEVED", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "LABEL RECIEVED FAILED" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateLabel(long labelId, string labelname)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.UpdateLabel(labelId, labelname);
                if (result != null)
                {
                    return Ok(new { success = true, message = "LABEL UPDATE SUCCESSFULLY", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "LABEL UPDATE FAILED" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public ActionResult DeleteLabel(long labelId)
        {

            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = labelBL.DeleteLabel(labelId);
            if (result != null)
            {
                return Ok(new { success = true, message = "Label deleted", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Label not deleted" });
            }


        }

    }
}
