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

        public DbSet<Contact> Contact { get; set; }
        public DbSet<BlockedUser> BlockedUser { get; set; }

        public DbSet<ChangedName> AllChanges { get; set; }

        

        public SingularityContext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Singularity;Trusted_Connection=True;");
        }
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
                .HasMany(u => u.Contacts)
                .WithOne().HasForeignKey(u => u.OwnerId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BlackList)
                .WithOne().HasForeignKey(u => u.BlockerId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Changes)
                .WithOne(c => c.Changer);

            modelBuilder.Entity<Message>()
                .HasMany(m => m.ImageLinks)
                .WithOne(i => i.message);
            base.OnModelCreating(modelBuilder);

           
        }
    }
}
