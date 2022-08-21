using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("Register")] // For Custom Route

        public ActionResult Registration(UserRegestrationModel userRegestrationModel)
        {
            try
            {
                var result = userBL.Register(userRegestrationModel);
                if(result != null)
                {
                    return Ok(new {success=true,message="Regestration Successfull",data=result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Regestration not Successfull"});
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost("Login")] // For Custom Route

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
    }
}
