using Domain.EFContext;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SingularityContext _db;

        private IRepository<User> userRepo;

        private IRepository<Message> msgRepo;

        private IRepository<Image> imgRepo;

        private IRepository<Audio> audioRepo;

        private IRepository<Video> videoRepo;

        private IRepository<ChangedName> allChanges;

        private IRepository<Contact> contactRepos;

        private IRepository<BlockedUser> blockedUserRepos;

        public UnitOfWork(SingularityContext db)
        {
            _db = db;
        }
        public IRepository<User> UserRepo {
            get
            {
                return userRepo ??
                    new Repository<User>(_db);
            } }
        public IRepository<Message> MsgRepo
        {
            get
            {
                return msgRepo ??
                    new Repository<Message>(_db);
            }
        }
        public IRepository<Image> ImgRepo
        {
            get
            {
                return imgRepo ??
                    new Repository<Image>(_db);
            }
        }
        public IRepository<Audio> AudioRepo
        {
            get
            {
                return audioRepo ??
                    new Repository<Audio>(_db);
            }
        }
        public IRepository<Video> VideoRepo
        {
            get
            {
                return videoRepo ??
                    new Repository<Video>(_db);
            }
        }

        public IRepository<ChangedName> AllChanges
        {
            get
            {
                return allChanges ??
                    new Repository<ChangedName>(_db);
            }
        }

        public IRepository<Contact> ContactsRepos
        {
            get
            {
                return contactRepos ??
                    new Repository<Contact>(_db);
            }
        }

        public IRepository<BlockedUser> BlockedUsers
        {
            get
            {
                return blockedUserRepos ??
                    new Repository<BlockedUser>(_db);
            }
        }

        public async Task CommitAsync()
        {
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Users ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT AllChanges ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Audios ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Videos ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Messages ON");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Contact ON");

            await _db.SaveChangesAsync();
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Users OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT AllChanges OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Audios OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Videos OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Messages OFF");
            this._db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Contact OFF");
        }
    }
}
