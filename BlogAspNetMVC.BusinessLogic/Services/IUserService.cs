using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using Microsoft.AspNetCore.Mvc;
using System;
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
        Task<IActionResult> SignIn(SignInRequest signInRequest);

        /// <summary>
        /// Зарегестрироваться в системе
        /// </summary>
        /// <param name="signUpRequest">Модель запроса на регистрацию в системе</param>
        /// <returns></returns>
        Task<IActionResult> SignUp(SignUpRequest signUpRequest);

        /// <summary>
        /// Изменить роль
        /// </summary>
        /// <param name="changeUserRoleRequest">Модель запроса на изменение роли</param>
        /// <returns></returns>
        Task<IActionResult> ChangeRole(ChangeUserRoleRequest changeUserRoleRequest);

        /// <summary>
        /// Изменить пароль пользователя
        /// </summary>
        /// <param name="changePasswordRequest">Модель запроса на обновление пароля пользователя</param>
        /// <returns></returns>
        Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest);

        /// <summary>
        /// Изменить UserName пользователя
        /// </summary>
        /// <param name="changeUserNameRequest">Модель запроса на обновление UserName пользователя</param>
        /// <returns></returns>
        Task<IActionResult> ChangeUserName(ChangeUserNameRequest changeUserNameRequest);

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> GetAll();

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<IActionResult> GetById(Guid guid);

        /// <summary>
        /// Получить пользователя по UserName
        /// </summary>
        /// <param name="name">UserName пользователя</param>
        /// <returns></returns>
        Task<IActionResult> GetByUserName(string userName);
    }
}
