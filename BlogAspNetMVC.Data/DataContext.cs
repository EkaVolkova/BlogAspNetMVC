using BlogAspNetMVC.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// Dataset для статей
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// Dataset для комментариев
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// Dataset для комментариев
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Dataset для пользователей
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Dataset для ролей пользователя
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Создание базы данных, если ее нет
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        /// <summary>
        /// Создание таблиц в базе данных на основе моделей 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().ToTable("Articles");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Tag>().ToTable("Tags");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");

            Guid adminGuid = Guid.NewGuid();
            Guid userGuid = Guid.NewGuid();
            Guid moderatorGuid = Guid.NewGuid();

            //Создадим роли по умолчанию
            var roles = new List<Role>
            {
                new Role
                {
                    Name = "admin",
                    Id = adminGuid
                },

                new Role
                {
                    Name = "user",
                    Id = userGuid
                },
                new Role
                {
                    Name = "moderator",
                    Id = moderatorGuid
                }
            };
            modelBuilder.Entity<Role>().HasData(roles);

            //создадим пользователей по умолчанию
            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    RegistrationDate = DateTime.UtcNow,
                    UserName = "admin",
                    Password = "admin",
                    Email = "admin@yandex.ru",
                    RoleId = adminGuid
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    RegistrationDate = DateTime.UtcNow,
                    UserName = "user1",
                    Password = "user1",
                    Email = "user1@yandex.ru",
                    RoleId = userGuid
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    RegistrationDate = DateTime.UtcNow,
                    UserName = "moderator",
                    Password = "moderator",
                    Email = "moderator@yandex.ru",
                    RoleId = moderatorGuid
                }
            };
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
