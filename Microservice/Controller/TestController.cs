using MassTransit;
using MassTransit.Testing;
using Microservice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        
        [HttpGet]
        [Route("GetAdminData")]
        [Authorize (Roles ="Admin")]
        public IActionResult GetAdminData()
        {
            var token = Request.Headers["Authorization"].ToString();
            
            Console.WriteLine("Received Token: " + token);
            return Ok("This data is for Admins only.");
        }

        // This method is accessible by users with either 'Admin' or 'User' role
        [HttpGet]
        [Route("GetUserData")]
        [Authorize(Roles = "Admin,Test")]
        public IActionResult GetUserData()
        {
            return Ok("This data is for Admins or Users.");
        }
        
    }
}
