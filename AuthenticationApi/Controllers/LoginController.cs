using AuthenticationApi.Model;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LogInDb _myDbContext;
        private readonly IConfiguration _configuration;
        private IPublishEndpoint _publishEndpoint;
        public LoginController( LogInDb myDbContext, IConfiguration configuration , IPublishEndpoint publishEndpoint)
        {
            _myDbContext = myDbContext;
            _configuration = configuration;
            _publishEndpoint = publishEndpoint;
        }
        
        [HttpPost]
        [Route("sendtoadmin")]
        public async Task<IActionResult> SendToAdmin(Common common)
        {
            await _publishEndpoint.Publish<Common>(common);
            return Ok("This data is for all users.");
        }
        [HttpPost]
        [Route("LogInUser")]
        public async Task<IActionResult> LogInUser(LoginDto loginDto)
        {
            var user = _myDbContext.Users.FirstOrDefault(u => u.Name == loginDto.Username && u.Password == loginDto.Password);
            if (user != null)
            {
                var role = user.Role;
                if (role != null)
                {
                    var token = JwtBearer.GenerateToken(user.Name, role, _configuration);

                    return Ok(new LoginResponseDto
                    {

                        Token = token,
                        Role = role
                    });
                }
                return Ok(User);
            }
            return BadRequest(new
            {
                Message = "No User"
            });

        }
    }
}
