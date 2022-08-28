using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
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
                var result = notesBL.CreateNotes(notesModel, userId);
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

        [Authorize]
        [HttpGet("Read")]
        public IActionResult ReadNotes(long noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.ReadNotes(noteid);
                if (result != null)
                {
                    return Ok(new { success = true, message = "NOTES RECIEVED", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "NOTES RECIEVED FAILED" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("Update")]
        public IActionResult updateNotes(NotesModel addnote, long noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.UpdateNotes(addnote, noteid, userId);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Note Updated Successfully",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to Update note"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete("Delete")]

        public IActionResult DeleteNotes(long noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (notesBL.DeleteNote(noteid))
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Note Deleted Successfully"
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to delete note"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        [Authorize]
        [HttpPut("Pin")]
        public IActionResult pinnedornot(long noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.PinnedORNot(noteid, userId);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        message = "Notes Pinned ",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        message = "Notes Unpinned "
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("Archive")]
        public IActionResult Archive(long noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.Archive(noteid,userId);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        message = "Note Archived Successfully ",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        message = "Note Unarchived"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [Authorize]
        [HttpPut("Trash")]
        public IActionResult Trash(long noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.Trash(noteid, userId);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        message = "Note Trash Successfully ",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        message = "Note UnTrashed"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("UploadImage")]
        public IActionResult UploadImage(long noteid, IFormFile img)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                var result = notesBL.UploadImage(noteid, img, userID);
                if (result != null)
                {
                    return this.Ok(new { message = "uploaded ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Not uploaded" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("Color")]
        public ActionResult ChoiceColor(long NotesID, string Color)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.All(x => x.Type == "UserId"));
                var result = notesBL.ChoiceColor(NotesID, Color);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Color Changed Successfully", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Unable to Change color" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


    }

}
