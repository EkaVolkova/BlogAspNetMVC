using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Models
{
    [Table("Users")]
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата регистрации пользователя
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Электронный адрес пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Навигационное свойство: список всех комментариев пользователя
        /// </summary>
        public List<Comment> Comments { get; set; }

        /// <summary>
        /// Навигационное свойство: список всех статей, написанных пользователем
        /// </summary>
        public List<Article> Articles { get; set; }

        /// <summary>
        /// Навигационное свойство id роли пользователя
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Навигационное свойство: Роль пользователя
        /// </summary>
        public Role Role { get; set; }
    }
}
