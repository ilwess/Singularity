using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Singularity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService,
            IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost, Route("token")]
        public async Task<ActionResult> Get(
            [FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            string token = string.Empty;
            if(_authService.IsAuthenticate(request, out token))
            {
                await _userService
                    .SetNewToken(request.UserName, token);
                UserDTO user = _userService.GetUsers(
                    p => p.Email == request.UserName ||
                    p.Phone == request.UserName)
                    .FirstOrDefault();
                return Ok(user);
            }

            return Ok();
        }

        [Authorize]
        [HttpGet, Route("test")]
        public string Test()
        {
            return "eee";
        }
    }
}