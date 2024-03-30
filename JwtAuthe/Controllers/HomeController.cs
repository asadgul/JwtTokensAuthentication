using JwtAuthe.CustomJwt;
using JwtAuthe.IJwtServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthe.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

     public class HomeController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly IJwtCustomManager jwtCustomManager;
        public HomeController(IJwtAuthenticationManager jwtAuthenticationManager,IJwtCustomManager jwtCustomManager)
        {
            this.jwtCustomManager = jwtCustomManager;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        [HttpGet("Get")]
        public IEnumerable<string> Get()
        {
            return new[] { "Pakistan", "Australia" };
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult JwtBuiltinAuthenticate() {

            var validatetoken = jwtAuthenticationManager.AuthenticateUser(new Models.Users()
            {
                UserName= "test1",
                Password= "asadgul"
            });
            return Ok(validatetoken);
        }
        [AllowAnonymous]
        [HttpPost("CustomeAuthenticate")]
        public IActionResult JwtCustomeAuthenticate()
        {
            var validatetoken = jwtCustomManager.AuthenticateUser(new Models.Users()
            {
                UserName = "test1",
                Password = "asadgul"
            });
            return Ok(validatetoken);
        }

    }
}
