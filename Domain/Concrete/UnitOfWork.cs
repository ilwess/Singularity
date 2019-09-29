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

        public IRepository<User> userRepo;

        public IRepository<Message> msgRepo;

        public IRepository<Image> imgRepo;

        public IRepository<Audio> audioRepo;

        public IRepository<Video> videoRepo;

        public UnitOfWork(SingularityContext db)
        {
            _db = db;
        }
        public IRepository<User> UserRepo {
            get
            {
                return userRepo ?? new Repository<User>(_db);
            } }
        public IRepository<Message> MsgRepo
        {
            get
            {
                return msgRepo ?? new Repository<Message>(_db);
            }
        }
        public IRepository<Image> ImgRepo
        {
            get
            {
                return imgRepo ?? new Repository<Image>(_db);
            }
        }
        public IRepository<Audio> AudioRepo
        {
            get
            {
                return audioRepo ?? new Repository<Audio>(_db);
            }
        }
        public IRepository<Video> VideoRepo
        {
            get
            {
                return videoRepo ?? new Repository<Video>(_db);
            }
        }

        public async Task CommitAsync()
        {
           await _db.SaveChangesAsync();
        }
    }
}
