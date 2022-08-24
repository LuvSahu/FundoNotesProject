using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")] // route is api/user/register
    [ApiController]
    public class UserController : ControllerBase // Interface
    {
        private readonly IUserBL userBL; // Object

        public UserController(IUserBL userBL) // Constructor
        {
            this.userBL = userBL;
        }
        [HttpPost] // For Custom Route
        [Route("Register")]
        public ActionResult Registration(UserRegestrationModel userRegestrationModel)
        {
            try
            {
                var result = userBL.Register(userRegestrationModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Regestration Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Regestration not Successfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost] // For Custom Route
        [Route("Login")]

        public ActionResult Login(UserLoginModel userLoginModel)
        {
            try
            {
                var result = userBL.Login(userLoginModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login is  Succecsfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login is not Successfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost] // For Custom Route
        [Route("ForgotPassword")]
        public ActionResult FogotPassword(string Email)
        {
            try
            {
                var result = userBL.FogotPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset link sent Succecsfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset link sending failed" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetLink")]
        public ActionResult ResetLink(string password, string confirmPassword)
        {

            try
            {

                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();

                var result = userBL.ResetLink(Email, password, confirmPassword);

                if (result != null)
                {
                    return Ok(new { success = true, message = "REST LINK SEND SUCCESSFULL" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "REST LINK SEND FAILED" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }

        }



    }

}
