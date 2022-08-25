using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL; // Object
        //private long userId;

        public NotesController(INotesBL notesBL) // Constructor
        {
            this.notesBL = notesBL;
        }
        [Authorize]
        [HttpPost] // For Custom Route
        [Route("Create")]
        public ActionResult CreationNotes(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.CreateNotes(notesModel,userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Created Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Notes creation failed" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
