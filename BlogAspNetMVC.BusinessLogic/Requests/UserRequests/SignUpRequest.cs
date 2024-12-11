using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Requests.UserRequests
{
    public class SignUpRequest
    {
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

    }
}
