﻿using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Exceptions.UserExceptions;
using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using BlogAspNetMVC.BusinessLogic.ViewModels;
using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using BlogAspNetMVC.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Изменить роль
        /// </summary>
        /// <param name="userViewModel">Модель запроса на изменение роли</param>
        /// <returns></returns>
        public async Task<UserViewModel> ChangeRole(UserViewModel userViewModel)
        {
            var user = await _userRepository.GetByUserName(userViewModel.UserName);
            //если пользователь не найден
            if (user is null)
            {
                throw new UserNotFoundException($"Пользователь с именем пользователя {userViewModel.UserName} не найден");
            }

            var query = _mapper.Map<UserViewModel, UpdateUserQuery>(userViewModel);

            await _userRepository.UpdateUser(user, query);

            user = await _userRepository.GetByUserName(userViewModel.UserName);

            var userView = _mapper.Map<User, UserViewModel>(user);

            return userView;
        }

        /// <summary>
        /// Изменить пароль пользователя
        /// </summary>
        /// <param name="userViewModel">Модель запроса на обновление пароля пользователя</param>
        /// <returns></returns>
        public async Task<UserViewModel> ChangePassword(UserViewModel userViewModel)
        {
            var user = await _userRepository.GetByUserName(userViewModel.UserName);
            //если пользователь не найден
            if (user is null)
            {
                throw new UserNotFoundException($"Пользователь с именем пользователя {userViewModel.UserName} не найден");
            }

            var query = _mapper.Map<UserViewModel, UpdateUserQuery>(userViewModel);

            await _userRepository.UpdateUser(user, query);

            user = await _userRepository.GetByUserName(userViewModel.UserName);

            var userView = _mapper.Map<User, UserViewModel>(user);

            return userView;
        }

        /// <summary>
        /// Изменить UserName пользователя
        /// </summary>
        /// <param name="userViewModel">Модель запроса на обновление UserName пользователя</param>
        /// <returns></returns>
        public async Task<UserViewModel> ChangeUserName(UserViewModel userViewModel)
        {
            var user = await _userRepository.GetById(userViewModel.Id);
            //если пользователь не найден
            if (user is null)
            {
                throw new UserNotFoundException($"Пользователь с id {userViewModel.Id} не найден");
            }


            var query = _mapper.Map<UserViewModel, UpdateUserQuery>(userViewModel);
            await _userRepository.UpdateUser(user, query);

            user = await _userRepository.GetById(userViewModel.Id);

            var userView = _mapper.Map<User, UserViewModel>(user);

            return userView;
        }

        /// <summary>
        /// Изменить UserName пользователя
        /// </summary>
        /// <param name="userViewModel">Модель запроса на обновление UserName пользователя</param>
        /// <returns></returns>
        public async Task<UserViewModel> ChangeUser(UserViewModel userViewModel)
        {
            var user = await _userRepository.GetById(userViewModel.Id);
            //если пользователь не найден
            if (user is null)
            {
                throw new UserNotFoundException($"Пользователь с id {userViewModel.Id} не найден");
            }


            var query = _mapper.Map<UserViewModel, UpdateUserQuery>(userViewModel);
            await _userRepository.UpdateUser(user, query);

            user = await _userRepository.GetById(userViewModel.Id);

            var userView = _mapper.Map<User, UserViewModel>(user);

            return userView;
        }
        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserViewModel>> GetAll()
        {
            var users = await _userRepository.GetAllUsers();
            if (users is null)
                return new List<UserViewModel>();

            var usersView = _mapper.Map<List<User>, List<UserViewModel>>(users);

            return usersView;

        }

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор пользователя</param>
        /// <returns></returns>
        public async Task<UserViewModel> GetById(Guid guid)
        {
            var user = await _userRepository.GetById(guid);
            //если пользователь не найден
            if (user is null)
                throw new UserNotFoundException($"Пользователь с Id {guid} не найден");
            
            var userView = _mapper.Map<User, UserViewModel>(user);

            return userView;

        }

        /// <summary>
        /// Получить пользователя по UserName
        /// </summary>
        /// <param name="name">UserName пользователя</param>
        /// <returns></returns>
        public async Task<UserViewModel> GetByUserName(string name)
        {
            var user = await _userRepository.GetByUserName(name);
            //если пользователь не найден
            if (user is null)
            {
                throw new UserNotFoundException($"Пользователь с именем пользователя {name} не найден");
            }

            var userView = _mapper.Map<User, UserViewModel>(user);

            return userView;

        }

        /// <summary>
        /// Войти в систему (проверка логина и пароля)
        /// </summary>
        /// <param name="signInRequest">Модель запроса на вход в систему</param>
        /// <returns></returns>
        public async Task<UserViewModel> SignIn(SignInRequest signInRequest)
        {
            var user = await _userRepository.GetByUserName(signInRequest.UserName);
            //если пользователь не найден
            if (user is null)
            {
                throw new UserNotFoundException($"Пользователь с именем пользователя {signInRequest.UserName} не найден");
            }

            if (user.Password != signInRequest.Password)
            {
                throw new UserPasswordIsWrong("Неверный пароль");
            }

            var userView = _mapper.Map<User, UserViewModel>(user);
            return userView;
        }

        /// <summary>
        /// Зарегестрироваться в системе
        /// </summary>
        /// <param name="signUpRequest">Модель запроса на регистрацию в системе</param>
        /// <returns></returns>
        public async Task<UserViewModel> SignUp(SignUpRequest signUpRequest)
        {
            var user = await _userRepository.GetByUserName(signUpRequest.UserName);
            //если пользователь уже существует
            if (!(user is null))
            {
                throw new UserNotFoundException($"Пользователь с именем пользователя {signUpRequest.UserName} уже существует");
            }

            user = _mapper.Map<SignUpRequest, User>(signUpRequest);
            


            await _userRepository.Create(user, new List<Article>(), new List<Comment>());

            user = await _userRepository.GetByUserName(signUpRequest.UserName);

            var userView = _mapper.Map<User, UserViewModel>(user);
            return userView;
        }
    }
}
