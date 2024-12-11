﻿using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
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
        IUserRepository _userRepository;
        IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var user = await _userRepository.GetByUserName(changePasswordRequest.UserName);
            if (user is null)
            {
                return new ObjectResult("Пользователь не найден") { StatusCode = 400 };
            }

            if (user.Password != changePasswordRequest.OldPassword)
            {
                return new ObjectResult("Неверный пароль") { StatusCode = 401 };
            }

            var query = _mapper.Map<ChangePasswordRequest, UpdateUserQuery>(changePasswordRequest);

            await _userRepository.UpdateUser(user, query);
            return new ObjectResult("Пароль обновлен") { StatusCode = 200 };
        }

        public async Task<IActionResult> ChangeUserName(ChangeUserNameRequest changeUserNameRequest)
        {
            var user = await _userRepository.GetByUserName(changeUserNameRequest.OldUserName);
            if (user is null)
            {
                return new ObjectResult("Пользователь не найден") { StatusCode = 400 };
            }


            var query = _mapper.Map<ChangeUserNameRequest, UpdateUserQuery>(changeUserNameRequest);
            await _userRepository.UpdateUser(user, query);
            return new ObjectResult("Имя пользователя обновлено") { StatusCode = 200 };
        }

        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllUsers();
            if (users is null || users.Count == 0)
                return new ObjectResult("Нет пользователей") { StatusCode = 400};

            return new ObjectResult(users) { StatusCode = 200};

        }
        public async Task<IActionResult> GetById(Guid guid)
        {
            var user = await _userRepository.GetById(guid);
            if (user is null)
                return new ObjectResult("Пользователь не найден") { StatusCode = 400 };

            return new ObjectResult(user) { StatusCode = 200 };

        }
        public async Task<IActionResult> SignIn(SignInRequest signInRequest)
        {
            var user = await _userRepository.GetByUserName(signInRequest.UserName);
            if (user is null)
            {
                return new ObjectResult("Пользователь не найден") { StatusCode = 400 };
            }

            if (user.Password != signInRequest.Password)
            {
                return new ObjectResult("Неверный пароль") { StatusCode = 401 };
            }

            return new ObjectResult("Вход успешен") { StatusCode = 200 };
        }

        public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
        {
            var user = await _userRepository.GetByUserName(signUpRequest.UserName);
            if (!(user is null))
            {
                return new ObjectResult("Пользователь с таким именем уже существует") { StatusCode = 400 };
            }

            user = _mapper.Map<SignUpRequest, User>(signUpRequest);
            


            await _userRepository.Create(user, new List<Article>(), new List<Comment>());

            return new ObjectResult("Пользователь зарегестрирован") { StatusCode = 200 };
        }
    }
}