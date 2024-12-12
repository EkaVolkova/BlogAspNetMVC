using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.ViewModels
{
    public class UserViewModel
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
        public List<Guid> CommentsId { get; set; }

        /// <summary>
        /// Навигационное свойство: список всех статей, написанных пользователем
        /// </summary>
        public List<Guid> ArticlesId { get; set; }

        /// <summary>
        /// Навигационное свойство id роли пользователя
        /// </summary>
        public string RoleName { get; set; }

    }
}
