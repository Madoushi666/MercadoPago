using Isopoh.Cryptography.Argon2;
using MercadoPago.CheckoutAPI.Application.Dtos.Commons.Response;
using MercadoPago.CheckoutAPI.Application.Dtos.Users.Request;
using MercadoPago.CheckoutAPI.Application.Interfaces;
using MercadoPago.CheckoutAPI.Application.Settings;
using MercadoPago.CheckoutAPI.Domain.Entities;
using MercadoPago.CheckoutAPI.Infrastructure.Persistences.Interfaces;
using MercadoPago.CheckoutAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MercadoPago.CheckoutAPI.Application.Services
{
    public class AuthApplication : IAuthApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AuthApplication(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings)
        {
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettings;
        }

        public async Task<BaseResponse<string>> Login(TokenRequestDto bodyRequest)
        {
            var response = new BaseResponse<string>();

            var user = await _unitOfWork.Usuario.GetUserByEmailAsync(bodyRequest.Email);

            if (user is not null && !string.IsNullOrWhiteSpace(bodyRequest.Password) && Argon2.Verify(user.Password, bodyRequest.Password))
            {
                response.Data = GenerateToken(user);
                response.Message = ReplyMessages.TokenGeneratedSuccessfully;
                response.StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                response.Message = ReplyMessages.InvalidUserPassword;
                response.StatusCode = StatusCodes.Status400BadRequest;
            }

            return response;
        }

        private string GenerateToken(Usuario user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.SecretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var userClaims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IdRolNavigation.Descripcion)
            };

            var handler = new JwtSecurityTokenHandler();

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtSettings.Value.Issuer,
                Audience = _jwtSettings.Value.Issuer,
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.Value.Expires),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = credentials
            };

            var token = handler.CreateToken(securityTokenDescriptor);

            //var token = new JwtSecurityToken(
            //    issuer: _jwtSettings.Value.Issuer,
            //    audience: _jwtSettings.Value.Issuer,
            //    claims: userClaims,
            //    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Value.Expires),
            //    notBefore: DateTime.UtcNow,
            //    signingCredentials: credentials);

            return handler.WriteToken(token);
        }
    }
}
