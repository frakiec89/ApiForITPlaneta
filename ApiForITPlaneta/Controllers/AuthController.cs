using ApiForITPlaneta.Models;
using ForITPlaneta.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiForITPlaneta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        IAuthServices _authServices;
        public AuthController(IAuthServices services)
        {
            _authServices = services;
        }


        [HttpGet("Hello")]
        public string Hello (string name)
        {
            return "Привет " + name +" я  апи  для  подготовки  к  олимпиаде ";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requst"></param>
        /// <returns>Возвращает  уникальный  токкен</returns>
        [HttpPost("aut")]
        public ActionResult<string> Aut(Models.UserAuthorizationRequst requst)
        {
            if (requst == null)
                return BadRequest();
            if (string.IsNullOrEmpty(requst.Password) ||
                string.IsNullOrWhiteSpace(requst.Password))
                return BadRequest("пустой параметр " + nameof(requst.Password));

            if (string.IsNullOrEmpty(requst.Login) ||
               string.IsNullOrWhiteSpace(requst.Login))
                return BadRequest("пустой параметр " + nameof(requst.Login));

            try
            {
               if (_authServices.Login(requst.Login, requst.Password)==true)
               {
                    return Ok(_authServices.GetTokken(requst.Login, requst.Password));
               }
               else
               {
                  return Unauthorized("Пользователь не найден");
               }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("ChekTokken")]
        public ActionResult<bool> ChekTokken([FromHeader] string tokken)
        {
            if(string.IsNullOrEmpty(tokken))
                return BadRequest ();

            try
            {
                return _authServices.IsChekedTokken(tokken);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
             
            }
        }
    }
}
