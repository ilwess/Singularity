using Domain.EFContext;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
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

        public async Task CommitAsync()
        {
           await _db.SaveChangesAsync();
        }
    }
}
