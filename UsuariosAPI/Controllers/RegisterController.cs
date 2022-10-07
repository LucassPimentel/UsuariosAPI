using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase    
    {
        private readonly RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public IActionResult RegisterUser(CreateUserDto createDto)
        {
            Result result = _registerService.RegisterUser(createDto);
            return result.IsSuccess ? (Ok(result.Successes[0])) : (StatusCode(500));
        }

        [HttpGet("/Email/Activation")]
        public IActionResult ActivateUserEmail([FromQuery] ActivateUserEmailRequest activateUserEmailRequest)
        {
            Result result = _registerService.ActiveUserEmail(activateUserEmailRequest);
            return result.IsSuccess ? (Ok(result.Successes)) : (StatusCode(500));
        }    
    }
}
