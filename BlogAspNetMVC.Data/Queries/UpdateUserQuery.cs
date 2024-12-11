﻿using BlogAspNetMVC.Data.Models;
using System.Collections.Generic;
using System.Net.Mail;

namespace BlogAspNetMVC.Data.Queries
{
    public class UpdateUserQuery
    {
        /// <summary>
        /// Новый UserName пользователя
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        /// Новый пароль пользователя
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Новый Email пользователя
        /// </summary>
        public string NewEmail { get; set; }

        public UpdateUserQuery(string newName = null, string newPassword = null, string newEmail = null)
        {
            NewName = newName;
            NewPassword = newPassword;
            NewEmail = newEmail;
        }
    }

}