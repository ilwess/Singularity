using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.EFContext
{
    public class SingularityContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Audio> Audios { get; set; }

        public DbSet<Image> Images { get; set; }
        
        public DbSet<Video> Videos { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Phone)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Contacts);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BlackList);

            modelBuilder.Entity<ChangedName>()
                .HasOne(c => c.Changer)
                .WithMany(u => u.Changes);

            modelBuilder.Entity<Message>()
                .HasMany(m => m.ImageLinks);
            base.OnModelCreating(modelBuilder);
        }
    }
}
