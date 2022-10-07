using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly TokenService _tokenService;
        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LoginUser(LoginRequest request)
        {
            //tentando fazer uma autenticaçao por senha
            var identityResult = _signInManager.
                PasswordSignInAsync(request.Username, request.Password, false, false);

            if (identityResult.Result.Succeeded)
            {
                // recuperando um identityuser pois o createToken precisa de um como parâmetro
                var identityUser = _signInManager.UserManager
                    .Users.FirstOrDefault(user => user.NormalizedUserName == request.Username.ToUpper());
                Token token = _tokenService.CreateToken(identityUser);

                // retornando o token para o controlller atraves de  
                // sucessos relacionados (WithSucess)
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Erro ao logar.");
        }

    }
}
