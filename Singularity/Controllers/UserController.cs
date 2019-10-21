using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Singularity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IOnlineService _online;

        public UserController(
            IUserService userService,
            IMapper mapper,
            IMessageService messageService,
            IOnlineService online)
        {
            _userService = userService;
            _messageService = messageService;
            _mapper = mapper;
            _online = online;
        }
        [AllowAnonymous]
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

        [HttpGet, Route("online")]
        public async Task<IActionResult> IfUserOnline(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            if (_online.IfUserOnline(id))
            {
                return Ok(true);
            } else
            {
                return Ok(false);
            }
        }

        [HttpGet, Route("getById")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            UserDTO user = await 
                _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpGet, Route("getByIds")]
        public async Task <IActionResult> Get(params int[] ids)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }
            IEnumerable<UserDTO> users = _userService
                .GetUsersByIds(ids);
            return Ok(users);
        }

        [HttpGet, Route("messages")]
        public IActionResult GetUserMessages(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            List<MessageDTO> messages =
                _messageService.GetMessagesOfUser(id).ToList();
            return Ok(messages);
        }

        [HttpGet, Route("dialog")]
        public IActionResult GetDialog(int id1, int id2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            List<MessageDTO> messages =
                _messageService.GetDialog(id1, id2).ToList();
            return Ok(messages);
        }

        [HttpGet, Route("all")]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPatch, Route("contactAdd")]
        public async Task<IActionResult> AddToContacts(
            int userId, int newContactId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            await _userService.AddToContact(
                userId,
                newContactId);
            return Ok();
        }

        [HttpPatch, Route("contactDel")]
        public async Task<IActionResult> DelFromCotacts(
            int userId, int contactToDelId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            await _userService.DeleteFromContact(
                userId, contactToDelId);
            return Ok();
        }

        [HttpPatch, Route("blacklistAdd")]
        public async Task<IActionResult> AddToBlacklist(
            int userId, int blockableId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            await _userService.BlockUser(
                userId, blockableId);
            return Ok();
        }

        [HttpPatch, Route("blacklistDel")]
        public async Task<IActionResult> DelFromBlacklist(
            int userId, int blockedId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            await _userService.UnblockUser(
                userId, blockedId);
            return Ok();
        }

        [HttpPatch, Route("change")]
        public async Task<IActionResult> ChangeContactName
            (int userId, string newName, int contactId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            await _userService.ChangeNameOfContact(
                userId, newName, contactId);
            return Ok();
        }

        [HttpPatch, Route("changeName")]
        public async Task<IActionResult> ChangeName
            (int id, string newName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            await _userService.ChangeName(id, newName);
            return Ok();
        }

        [HttpPatch, Route("changeLogin")]
        public async Task<IActionResult> ChangeLogin
            (int id, string newLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            await _userService.ChangeLogin(id, newLogin);
            return Ok();
        }


    }
}