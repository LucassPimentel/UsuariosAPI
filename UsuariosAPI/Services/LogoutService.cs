using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Services
{
    public class LogoutService
    {
        private readonly SignInManager<IdentityUser<int>> _singInManager;

        public LogoutService(SignInManager<IdentityUser<int>> singInManager)
        {
            _singInManager = singInManager;
        }

        public Result LogoutUser()
        {
            var resultIdentity = _singInManager.SignOutAsync();
            return resultIdentity.IsCompletedSuccessfully ? (Result.Ok()) : (Result.Fail("Logout Falhou"));
        }
    }
}
