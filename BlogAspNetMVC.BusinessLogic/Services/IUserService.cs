using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using BlogAspNetMVC.BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public interface IUserService
    {

        /// <summary>
        /// Войти в систему (проверка логина и пароля)
        /// </summary>
        /// <param name="signInRequest">Модель запроса на вход в систему</param>
        /// <returns></returns>
        Task<UserViewModel> SignIn(SignInRequest signInRequest);

        /// <summary>
        /// Зарегестрироваться в системе
        /// </summary>
        /// <param name="signUpRequest">Модель запроса на регистрацию в системе</param>
        /// <returns></returns>
        Task<UserViewModel> SignUp(SignUpRequest signUpRequest);

        /// <summary>
        /// Изменить роль
        /// </summary>
        /// <param name="userViewModel">Модель запроса на изменение роли</param>
        /// <returns></returns>
        Task<UserViewModel> ChangeRole(UserViewModel userViewModel);

        /// <summary>
        /// Изменить пароль пользователя
        /// </summary>
        /// <param name="userViewModel">Модель запроса на обновление пароля пользователя</param>
        /// <returns></returns>
        Task<UserViewModel> ChangePassword(UserViewModel userViewModel);

        /// <summary>
        /// Изменить UserName пользователя
        /// </summary>
        /// <param name="userViewModel">Модель запроса на обновление UserName пользователя</param>
        /// <returns></returns>
        Task<UserViewModel> ChangeUserName(UserViewModel userViewModel);

        /// <summary>
        /// Изменить пользователя
        /// </summary>
        /// <param name="userViewModel">Модель запроса на обновление UserName пользователя</param>
        /// <returns></returns>
        Task<UserViewModel> ChangeUser(UserViewModel userViewModel);

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        Task<List<UserViewModel>> GetAll();

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<UserViewModel> GetById(Guid guid);

        /// <summary>
        /// Получить пользователя по UserName
        /// </summary>
        /// <param name="name">UserName пользователя</param>
        /// <returns></returns>
        Task<UserViewModel> GetByUserName(string userName);
    }
}
