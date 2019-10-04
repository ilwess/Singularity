using BLL.DTOs;
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
        Task DeleteUserAsync(UserDTO user);
        Task ChangeNameOfContact(
            UserDTO changer, string name, UserDTO changable);
        Task UpdateUser(UserDTO user);
        Task BlockUser(UserDTO blocker, UserDTO blockable);
        Task UnblockUser(UserDTO blocker, UserDTO blocked);
        Task AddToContact(UserDTO user, UserDTO newContact);
        Task DeleteFromContact(
            UserDTO user, UserDTO contactToDelete);
        Task SetAvaAsync(
            UserDTO userDTO, ImageDTO ava);

    }
}
