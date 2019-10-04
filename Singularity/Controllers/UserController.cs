﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Singularity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> CreateUser(UserDTO newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            await _userService.CreateUserAsync(newUser);
            
            UserDTO user = _userService
                .GetUsers(u => u.Login == newUser.Login)
                .FirstOrDefault();
            Uri userUri = new Uri("https://localhost:44336/api/user/getById/" + user.Id);
            return Created(userUri, user);
        }

        [HttpGet, Route("getById")]
        public async Task<IActionResult> Get(int id)
        {
            UserDTO user = await 
                _userService.GetUserByIdAsync(id);
            return Ok(user);
        }
    }
}