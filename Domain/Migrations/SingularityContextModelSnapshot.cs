﻿// <auto-generated />
using System;
using Domain.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(SingularityContext))]
    partial class SingularityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Audio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Link");

                    b.Property<int?>("messageId");

                    b.HasKey("Id");

                    b.HasIndex("messageId");

                    b.ToTable("Audios");
                });

            modelBuilder.Entity("Domain.Models.BlockedUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlockedId");

                    b.Property<int>("BlockerId");

                    b.HasKey("Id");

                    b.HasIndex("BlockerId");

                    b.ToTable("BlockedUser");
                });

            modelBuilder.Entity("Domain.Models.ChangedName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChangableId");

                    b.Property<int?>("ChangerId");

                    b.Property<string>("NewName");

                    b.HasKey("Id");

                    b.HasIndex("ChangableId");

                    b.HasIndex("ChangerId");

                    b.ToTable("AllChanges");
                });

            modelBuilder.Entity("Domain.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OwnerId");

                    b.Property<int>("UserContactId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("Domain.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Link");

                    b.Property<int?>("messageId");

                    b.HasKey("Id");

                    b.HasIndex("messageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domain.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfCreation");

                    b.Property<int?>("RecieverId");

                    b.Property<int?>("SenderId");

                    b.Property<int?>("SharedMessageId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("RecieverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("SharedMessageId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AvaId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.HasIndex("AvaId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Link");

                    b.Property<int?>("messageId");

                    b.HasKey("Id");

                    b.HasIndex("messageId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Domain.Models.Audio", b =>
                {
                    b.HasOne("Domain.Models.Message", "message")
                        .WithMany("AudioLink")
                        .HasForeignKey("messageId");
                });

            modelBuilder.Entity("Domain.Models.BlockedUser", b =>
                {
                    b.HasOne("Domain.Models.User")
                        .WithMany("BlackList")
                        .HasForeignKey("BlockerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Models.ChangedName", b =>
                {
                    b.HasOne("Domain.Models.User", "Changable")
                        .WithMany()
                        .HasForeignKey("ChangableId");

                    b.HasOne("Domain.Models.User", "Changer")
                        .WithMany("Changes")
                        .HasForeignKey("ChangerId");
                });

            modelBuilder.Entity("Domain.Models.Contact", b =>
                {
                    b.HasOne("Domain.Models.User")
                        .WithMany("Contacts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Models.Image", b =>
                {
                    b.HasOne("Domain.Models.Message", "message")
                        .WithMany("ImageLinks")
                        .HasForeignKey("messageId");
                });

            modelBuilder.Entity("Domain.Models.Message", b =>
                {
                    b.HasOne("Domain.Models.User", "Reciever")
                        .WithMany()
                        .HasForeignKey("RecieverId");

                    b.HasOne("Domain.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");

                    b.HasOne("Domain.Models.Message", "SharedMessage")
                        .WithMany()
                        .HasForeignKey("SharedMessageId");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.HasOne("Domain.Models.Image", "Ava")
                        .WithMany()
                        .HasForeignKey("AvaId");
                });

            modelBuilder.Entity("Domain.Models.Video", b =>
                {
                    b.HasOne("Domain.Models.Message", "message")
                        .WithMany("VideoLink")
                        .HasForeignKey("messageId");
                });
#pragma warning restore 612, 618
        }
    }
}
