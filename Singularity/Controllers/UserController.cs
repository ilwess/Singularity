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
            int userId, int newContactId)
        {
            await _userService.AddToContact(
                userId,
                newContactId);
            return Ok();
        }

        [HttpPatch, Route("contact/del")]
        public async Task<IActionResult> DelFromCotacts(
            int userId, int contactToDelId)
        {
            await _userService.DeleteFromContact(
                userId, contactToDelId);
            return Ok();
        }

        [HttpPatch, Route("blacklist/add")]
        public async Task<IActionResult> AddToBlacklist(
            int userId, int blockableId)
        {
            await _userService.BlockUser(
                userId, blockableId);
            return Ok();
        }

        [HttpPatch, Route("blacklist/del")]
        public async Task<IActionResult> DelFromBlacklist(
            int userId, int blockedId)
        {
            await _userService.UnblockUser(
                userId, blockedId);
            return Ok();
        }

        [HttpPatch, Route("change")]
        public async Task<IActionResult> ChangeContactName
            (int userId, string newName, int contactId)
        {
            await _userService.ChangeNameOfContact(
                userId, newName, contactId);
            return Ok();
        }


    }
}