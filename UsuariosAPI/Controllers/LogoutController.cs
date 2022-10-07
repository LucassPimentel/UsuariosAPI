using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {

        private readonly LogoutService _logoutService;
        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public IActionResult LogoutUser()
        {
            Result result = _logoutService.LogoutUser();
            return result.IsSuccess ? (Ok(result.Successes)) : (Unauthorized(result.Errors));
        }
    }
}
