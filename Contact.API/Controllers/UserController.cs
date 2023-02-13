using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contact.BLL.Interfaces;
using Contact.VM;

namespace Contact.API.Controllers
{
    public class UserController : ApiBaseController
    {
        public UserController(IRepositoryWrapper repo) : base(repo) { }
        
        [HttpPost("Register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public APIResult<UserInfoViewModel> Register([FromBody]UserViewModel userVM)
        {
            return _repo.user.Register(userVM);
        }

        [HttpPost("Login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public APIResult<UserInfoViewModel> Login([FromBody]LoginViewModel logVM)
        {
            return SetLoggedUser(_repo.user.Login(logVM));
        }

        [HttpPut("UpdateUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(AuthorizationFilterAttribute))]
        public APIResult<UserInfoViewModel> UpdateUser([FromBody]UserInfoViewModel userVM)
        {
            return _repo.user.UpdateUser(userVM);
        }

        [HttpGet("Logout")]
        public APIResult<Boolean> Logout()
        {
            return _repo.user.Logout();
        }
    }
}
