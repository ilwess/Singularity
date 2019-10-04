using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public UserService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _db = uow;
            _mapper = mapper;
        }
        public async Task AddToContact(UserDTO userDTO, UserDTO newContactDTO)
        {
            User user = await
                _db.UserRepo.GetById(userDTO.Id);
            User newContact = await
                _db.UserRepo.GetById(newContactDTO.Id);

            if (user != null &&
               newContact != null &&
               !user.Contacts.Contains(newContact))
            {
                user.BlackList.Add(newContact);
                await _db.UserRepo.Update(user);
            }

            await CommitAsync();
        }

        public async Task BlockUser(UserDTO blockerDTO, UserDTO blockableDTO)
        {
            User blocker = await 
                _db.UserRepo.GetById(blockerDTO.Id);
            User blockable = await 
                _db.UserRepo.GetById(blockableDTO.Id);

            if(blocker != null &&
               blockable != null &&
               !blocker.BlackList.Contains(blockable))
            {
                blocker.BlackList.Add(blockable);
                await _db.UserRepo.Update(blocker);
            }

            await CommitAsync();
        }

        public async Task ChangeNameOfContact(UserDTO changerDTO, string name, UserDTO changableDTO)
        {
            User changer = _mapper.Map<User>(changerDTO);
            ChangedName change = changer
                .Changes
                .FirstOrDefault(c => c.Changable.Id == changableDTO.Id);
            if (change != null)
            {
                change.NewName = name;
            } else
            {
                change = new ChangedName()
                {
                    Changer = changer,
                    NewName = name,
                    Changable = await _db.UserRepo.GetById(changableDTO.Id)
                };
                changer.Changes.Add(change);
                await _db.AllChanges.Create(change);
                await _db.UserRepo.Update(changer);
            }

            await CommitAsync();
        }

        public async Task CommitAsync()
        {
            await _db.CommitAsync();
        }

        public async Task CreateUserAsync(UserDTO user)
        {
            User newUser = _mapper.Map<UserDTO, User>(user);
            await _db.UserRepo.Create(newUser);
            await CommitAsync();
        }

        public async Task DeleteFromContact(UserDTO userDTO, UserDTO contactToDelete)
        {
            User user = await _db.UserRepo.GetById(userDTO.Id);
            User contactToDel =
                await _db.UserRepo.GetById(contactToDelete.Id);

            if (user.Contacts.Contains(contactToDel))
            {
                user.Contacts.Remove(contactToDel);
                await _db.UserRepo.Update(user);
            }
            await CommitAsync();
        }

        public async Task DeleteUserAsync(UserDTO user)
        {
            await _db.UserRepo.Delete(user.Id);
            await CommitAsync();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = _db.UserRepo.GetAll();
            var usersDTO = _mapper.Map<
                IEnumerable<User>,
                IEnumerable<UserDTO>>(users.ToList());
            return usersDTO;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _db.UserRepo
                .GetById(id);

            if (user != null)
            {
                return _mapper.Map<User, UserDTO>(user);
            }
            else
                return null;
        }

        public IEnumerable<UserDTO> GetUsers(
            Expression<Func<User, bool>> predicate)
        {
            var users = _db.UserRepo
                .Get(predicate)
                .Include(u => u.Changes)
                .Include(u => u.Contacts)
                .Include(u => u.Messages)
                .Include(u => u.BlackList)
                .Include(u => u.Ava);
            return _mapper.Map<
                IEnumerable<User>,
                IEnumerable<UserDTO>>(users.ToList());
        }

        public async Task SetAvaAsync(UserDTO userDTO, ImageDTO ava)
        {
            User user = _mapper.Map<User>(userDTO);
            if(user != null)
            {
                user.Ava =_mapper.Map<Image>(ava);
                await _db.UserRepo.Update(user);
            }
        }

        public async Task UnblockUser(UserDTO blockerDTO, UserDTO blockedDTO)
        {
            User blocker = await 
                _db.UserRepo.GetById(blockerDTO.Id);

            User blocked = await
                _db.UserRepo.GetById(blockerDTO.Id);

            if(blocker != null &&
                blocked != null &&
                blocker.BlackList.Contains(blocked))
            {
                blocker.BlackList.Remove(blocked);
                await _db.UserRepo.Update(blocker);
            }

            await CommitAsync();
        }

        public async Task UpdateUser(UserDTO user)
        {
            User userToUpd = _mapper
                .Map<UserDTO, User>(user);
            await _db.UserRepo.Update(userToUpd);
            await CommitAsync();
        }
    }
}
