using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using ULearn.API.Attributes;
using ULearn.Core.Manager.Interfaces;
using ULearn.ModelView.ModelView;

namespace ULearn.API.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    public class UserController : ApiBaseController
    {
        private IUserManager _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger,
                              IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [Route("api/v{version:apiVersion}/signUp")]
        [HttpPost]
        [AllowAnonymous]
        [MapToApiVersion("1")]
        public IActionResult SignUp([FromBody] UserRegistrationModel userReg)
        {
            var res = _userManager.SignUp(userReg);
            return Ok(res);
        }

        [Route("api/v{version:apiVersion}/login")]
        [HttpPost]
        [AllowAnonymous]
        [MapToApiVersion("1")]
        public IActionResult Login([FromBody] LoginModelView userReg)
        {
            var res = _userManager.Login(userReg);
            return Ok(res);
        }

        [Route("api/v{version:apiVersion}/fileretrive/profilepic")]
        [HttpGet]
        [MapToApiVersion("1")]
        public IActionResult Retrive(string filename)
        {
            var folderPath = Directory.GetCurrentDirectory();
            folderPath = $@"{folderPath}\{filename}";
            var byteArray = System.IO.File.ReadAllBytes(folderPath);
            return File(byteArray, "image/jpeg", filename);
        }

        [Route("api/v{version:apiVersion}/myProfile")]
        [HttpPut]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "user_edit")]
        public IActionResult UpdateMyProfile(UserModel request)
        {
            var user = _userManager.UpdateProfile(LoggedInUser , request);
            return Ok(user);
        }

        [HttpDelete]
        [Route("api/v{version:apiVersion}/{id}")]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "user_delete")]
        public IActionResult Delete(int id)
        {
            _userManager.DeleteUser(LoggedInUser, id);
            return Ok();
        }

        [Route("api/v{version:apiVersion}/Confirmation")]
        [HttpPost]
        [MapToApiVersion("1")]
        public IActionResult Confirmation(string confirmationLink)
        {
            var result = _userManager.Confirmation(confirmationLink);
            return Ok(result);
        }
    }
}
