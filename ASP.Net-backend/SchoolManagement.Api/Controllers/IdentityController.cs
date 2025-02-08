using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Identity;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("register")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateUser(RegisterDto registerDto)
        {

            var result = await _identityService.CreateUserAsync(registerDto);
            //return Ok(result.Item1);
            return Ok(new
            {
                userId = result.Item2,
                token = result.Item1
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogUser(LoginDto loginDto)
        {
            var user = await _identityService.CheckCredentialsAsync(loginDto);

            return Ok(new
            {
                verification = user.Item1,
                role = user.Item3,
                token = user.Item2
            });
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _identityService.ListUsersAsync();
            return Ok(users);
        }

    }
}