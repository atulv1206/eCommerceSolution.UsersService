using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public AuthController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        //[HttpPost]
        [HttpPost("register")]
        public async Task<IActionResult> Register(Core.DTO.RegisterRequest registerRequest)
        {
            if (registerRequest == null) {
                return BadRequest("Invalid Registration Data.");
            }
            AuthenticationResponse? authenticationResponse= await _usersService.Register(registerRequest);
            if (authenticationResponse == null || authenticationResponse.Success == false){
                return BadRequest(authenticationResponse);
            }
            return Ok(authenticationResponse);
        }
        //[HttpPost]
        [HttpPost("login")]
        public async Task<IActionResult> Login(Core.DTO.LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid Login Data.");
            }
            AuthenticationResponse authenticationResponse=await _usersService.Login(loginRequest);
            if (authenticationResponse == null || authenticationResponse.Success == false)
            {
                return Unauthorized(authenticationResponse);
            }
            return Ok(authenticationResponse);
        }
    }
}
