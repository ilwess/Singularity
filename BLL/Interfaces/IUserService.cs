using BLL.DTOs;
using BLL.Models;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserDTO user);
        Task CommitAsync();
        IEnumerable<UserDTO> GetAllUsers();
        IEnumerable<UserDTO> GetUsers(
            Expression<Func<User, bool>> predicate);
        Task<UserDTO> GetUserByIdAsync(int id);
        IEnumerable<UserDTO> GetUsersByIds(params int[] ids);
        Task DeleteUserAsync(UserDTO user);
        Task ChangeNameOfContact(
            int changerId, string name, int changableId);
        Task UpdateUser(UserDTO user);
        Task BlockUser(int blockerId, int blockableId);
        Task UnblockUser(int blockerId, int blockedId);
        Task AddToContact(int userId, int newContactId);
        Task DeleteFromContact(
            int userId, int contactToDeleteId);
        Task SetAvaAsync(
            int userDTOId, ImageDTO ava);
        Task SetNewToken(string userName, string token);
        Task ChangeName(int userId, string newName);
        Task ChangeLogin(int userId, string newLogin);
    }
}
