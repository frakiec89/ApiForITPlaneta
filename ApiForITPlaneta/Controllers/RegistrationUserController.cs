using ApiForITPlaneta.Models;
using ForITPlaneta.Core;
using ForITPlaneta.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiForITPlaneta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationUserController : ControllerBase
    {

        IUserService _userService; 

        public RegistrationUserController(IUserService service  )
        {
            _userService = service;  
        }

        [HttpPost ("registration")]
        public ActionResult AddUser (Models.NewUserRequst requst)
        {
            if (requst == null)
                return BadRequest();
            if (string.IsNullOrEmpty(requst.Password) ||
                string.IsNullOrWhiteSpace(requst.Password))
                return BadRequest("пустой параметр " + nameof(requst.Password));

            if (string.IsNullOrEmpty(requst.Login) ||
               string.IsNullOrWhiteSpace(requst.Login))
                return BadRequest("пустой параметр " + nameof(requst.Login));

            if (string.IsNullOrEmpty(requst.UserName) ||
              string.IsNullOrWhiteSpace(requst.Login))
                return BadRequest("пустой параметр " + nameof(requst.UserName));

            if (string.IsNullOrEmpty(requst.Email) ||
            string.IsNullOrWhiteSpace(requst.Login))
                return BadRequest("пустой параметр " + nameof(requst.Email));

            try
            {
                if (_userService.AddUser(GetNewUserCore(requst)) == true)
                    return Ok();
                else
                   return NotFound("Error");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
           
        }

        private User GetNewUserCore(NewUserRequst requst)
        {
           return new User ()
           { 
               Email = requst.Email, Password = requst.Password,
               Login= requst.Login , UserName = requst.UserName 
           };
        }
    }
}
