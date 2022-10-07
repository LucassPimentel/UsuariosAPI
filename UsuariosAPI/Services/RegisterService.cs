using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Web;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class RegisterService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly EmailService _emailService;

        public RegisterService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result RegisterUser(CreateUserDto createDto)
        {
            // para um model a partir de um dto
            User user = _mapper.Map<User>(createDto);

            // para um identityUser a partir de um model
            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);

            // cria de maneira assincrona um usuario
            Task<IdentityResult> identityResult = _userManager.CreateAsync(userIdentity, createDto.Password);
            if (identityResult.Result.Succeeded)
            {
                // gera um token para confirmação do email
                var activateCode = _userManager.GenerateEmailConfirmationTokenAsync(userIdentity).Result;

                var encodedCode = HttpUtility.UrlEncode(activateCode);

                _emailService.SendEmailWithActivateCode(new[] { userIdentity.Email }, "Link de ativação", userIdentity.Id, encodedCode);

                return Result.Ok().WithSuccess(activateCode);
            }
            return Result.Fail("Erro ao cadastrar usuário.");
        }

        public Result ActiveUserEmail(ActivateUserEmailRequest activateUserEmailRequest)
        {
            var identityUser = _userManager.Users.FirstOrDefault(u => u.Id == activateUserEmailRequest.Id);


            // confirmando email...
            var identityResult = _userManager.ConfirmEmailAsync(identityUser, activateUserEmailRequest.ActivateToken).Result;

            return identityResult.Succeeded ? (Result.Ok()) : (Result.Fail("Falha ao ativar conta."));


        }
    }
}
