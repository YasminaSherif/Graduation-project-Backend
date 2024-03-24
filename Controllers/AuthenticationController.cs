using Graduation_project.Models;
using Graduation_project.Repository.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _Rebo;

        public AuthenticationController(IAuthentication repo)
        {
           _Rebo =repo ;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterRequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _Rebo.RegisterAsync(requestDTO);

            if (result.message is not "Registerd")
                return BadRequest(result.message);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto requestDto)
        {
           
            var result = await _Rebo.LoginAsync(requestDto);

            if (result.message is not "Logged in")
                return BadRequest(result.message);

            return Ok(result);


        }

    }
}

