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
        public async Task AddToContact(int userDTOId, int newContactDTOId)
        {
            User user = await
                _db.UserRepo.GetById(userDTOId);
            User newContact = await
                _db.UserRepo.GetById(newContactDTOId);

            if (user != null &&
               newContact != null)
            {
                if (!user.Contacts
                    .Any(c => c.UserContactId == newContact.Id))
                {
                    Contact contact = new Contact()
                    {
                        OwnerId = user.Id,
                        UserContactId = newContact.Id,
                    };
                    user.Contacts.Add(contact);
                    await _db.ContactsRepos.Create(contact);
                }
            }
            await CommitAsync();
        }

        public async Task BlockUser(int blockerDTOId, int blockableDTOId)
        {
            User blocker = await 
                _db.UserRepo.GetById(blockerDTOId, "Contacts", "BlackList", "Changes", "Ava");
            User blockable = await 
                _db.UserRepo.GetById(blockableDTOId, "Contacts", "BlackList", "Changes", "Ava");

            if(blocker != null &&
               blockable != null)
            {
                if(blocker.BlackList == null)
                {
                    blocker.BlackList 
                        = new List<BlockedUser>();
                }
                if (!blocker.BlackList
                    .Any(u => u.BlockerId == blockable.Id))
                {
                    BlockedUser bu = new BlockedUser()
                    {
                        BlockedId = blockable.Id,
                        BlockerId = blocker.Id,
                    };
                    blocker.BlackList.Add(bu);
                    await _db.BlockedUsers.Create(bu);
                }
            }

            await CommitAsync();
        }

        public async Task ChangeNameOfContact(int changerDTOId, string name, int changableDTOId)
        {
            User changer = await _db.UserRepo.GetById(changerDTOId, "Contacts", "BlackList", "Changes", "Ava");
            ChangedName change = changer
                .Changes
                .FirstOrDefault(c => c.Changable.Id == changableDTOId);
            if (change != null)
            {
                change.NewName = name;
            } else
            {
                change = new ChangedName()
                {
                    Changer = changer,
                    NewName = name,
                    Changable = await _db.UserRepo.GetById(changableDTOId, "Contacts", "BlackList", "Changes", "Ava")
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
            var img = await _db.ImgRepo.GetById(1);
            User newUser = _mapper.Map<UserDTO, User>(user);
            await _db.UserRepo.Create(newUser);
            await CommitAsync();
        }

        public async Task DeleteFromContact(int userDTOId, int contactToDeleteId)
        {
            User user = await _db.UserRepo.GetById(userDTOId, "Contacts", "BlackList", "Changes", "Ava");
            User contactToDel =
                await _db.UserRepo.GetById(contactToDeleteId, "Contacts", "BlackList", "Changes", "Ava");

            if (user != null &&
                contactToDel != null)
            {
                Contact contact = user.Contacts
                    .FirstOrDefault(c => c.UserContactId ==
                    contactToDel.Id);
                if (contact != null)
                {
                    user.Contacts.Remove(contact);
                    await _db.ContactsRepos.Delete(contact.Id);
                }
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
                .GetById(id, "Contacts", "BlackList", "Changes", "Ava");
            
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
                .Include(u => u.BlackList)
                .Include(u => u.Ava);
            return _mapper.Map<
                IEnumerable<User>,
                IEnumerable<UserDTO>>(users.ToList());
        }

        public async Task SetAvaAsync(int userDTOId, ImageDTO ava)
        {
            User user = await _db.UserRepo.GetById(userDTOId, "Contacts", "BlackList", "Changes", "Ava");
            if(user != null)
            {
                user.Ava =_mapper.Map<Image>(ava);
                await _db.UserRepo.Update(user);
            }
        }

        public async Task SetNewToken(string userName, string token)
        {
            var user = GetUsers(
                    u => u.Email == userName ||
                    u.Login == userName ||
                    u.Phone == userName).FirstOrDefault();
            if (user != null)
            {
                user.Token = token;

                await CommitAsync();
            }
        }

        public async Task UnblockUser(int blockerDTOId, int blockedDTOId)
        {
            User blocker = await 
                _db.UserRepo.GetById(blockerDTOId, "BlackList");

            User blocked = await
                _db.UserRepo.GetById(blockedDTOId, "BlackList");

            if(blocker != null &&
                blocked != null &&
                blocker.BlackList
                .Any(b => b.BlockedId == blocked.Id))
            {
                BlockedUser bu = blocker.BlackList.FirstOrDefault(
                    b => b.BlockedId == blocked.Id);
                blocker.BlackList.Remove(bu);
                await _db.BlockedUsers.Delete(bu.Id);
            }
            await CommitAsync();
        }

        public async Task UpdateUser(UserDTO user)
        {
            User userToUpd = await _db.UserRepo.GetById(user.Id);
            await _db.UserRepo.Update(userToUpd);
            await CommitAsync();
        }

        public async Task ChangeName(int userId, string newName)
        {
            User user = await _db.UserRepo.GetById(userId);
            user.Name = newName;
            await _db.UserRepo.Update(user);
        }

        public async Task ChangeLogin(int userId, string newLogin)
        {
            User user = await _db.UserRepo.GetById(userId);
            user.Login = newLogin;
            await _db.UserRepo.Update(user);
        }
    }
}
