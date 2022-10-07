using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LoginUser(LoginRequest request)
        {
            Result result = _loginService.LoginUser(request);
            // recuperando o token passado no LoginService no withSucess
            return result.IsSuccess ? (Ok(result.Successes[0])) : (Unauthorized(result.Errors[0]));
        }
    }
}
