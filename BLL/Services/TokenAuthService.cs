using BLL.Interfaces;
using BLL.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services
{
    public class TokenAuthService : IAuthService
    {
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;

        public TokenAuthService(
            IUserManagementService userManagementService,
            IOptions<TokenManagement> tokenManagement)
        {
            _userManagementService = userManagementService;
            _tokenManagement = tokenManagement.Value;
        }
        public bool IsAuthenticate(TokenRequest request, out string token)
        {
            token = string.Empty;
            if(!_userManagementService
                .IsValidUser(request.UserName, request.Password))
            {
                return false;
            }

            var claim = new[]
            {
                new Claim(ClaimTypes.Name, request.UserName),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }
    }
}
