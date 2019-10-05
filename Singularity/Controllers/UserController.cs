using System;
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

        [HttpGet, Route("all")]
        public IActionResult Get()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPatch, Route("contact/add")]
        public async Task<IActionResult> AddToContacts(
            UserDTO user, UserDTO newContact)
        {
            await _userService.AddToContact(user, newContact);
            return Ok();
        }

        [HttpPatch, Route("contact/del")]
        public async Task<IActionResult> DelFromCotacts(
            UserDTO user, UserDTO contactToDel)
        {
            await _userService.DeleteFromContact(
                user, contactToDel);
            return Ok();
        }

        [HttpPatch, Route("blacklist/add")]
        public async Task<IActionResult> AddToBlacklist(
            UserDTO user, UserDTO blockable)
        {
            await _userService.BlockUser(
                user, blockable);
            return Ok();
        }

        [HttpPatch, Route("blacklist/del")]
        public async Task<IActionResult> DelFromBlacklist(
            UserDTO user, UserDTO blocked)
        {
            await _userService.UnblockUser(
                user, blocked);
            return Ok();
        }

        [HttpPatch, Route("change")]
        public async Task<IActionResult> ChangeContactName
            (UserDTO user, string newName, UserDTO contact)
        {
            await _userService.ChangeNameOfContact(
                user, newName, contact);
            return Ok();
        }


    }
}