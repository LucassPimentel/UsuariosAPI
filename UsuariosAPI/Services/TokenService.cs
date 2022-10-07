using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> identiyUser)
        {
            // claim -> define o que o usuario esta exigindo
            Claim[] userClaims = new Claim[]
            {
                new Claim("username", identiyUser.UserName),
                new Claim("id", identiyUser.Id.ToString())
            };

            // gera chave para criptografar o token
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
                );

            // gera credenciais a partir da chave e de algum algoritmo de criptografia
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: userClaims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)
                );

            // transformando a var token em string para que o token possa ser armazenado
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(tokenString);
        }
    }
}
