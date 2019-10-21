using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepo { get; }

        IRepository<Message> MsgRepo { get; }

        IRepository<Image> ImgRepo { get; }

        IRepository<Audio> AudioRepo { get; }

        IRepository<Video> VideoRepo { get; }
        
        IRepository<ChangedName> AllChanges { get; }

        IRepository<Contact> ContactsRepos { get; }

        IRepository<BlockedUser> BlockedUsers { get; }
        Task CommitAsync();
    }
}
