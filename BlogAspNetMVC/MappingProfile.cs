using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.BusinessLogic.Requests.CommentRequest;
using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.BusinessLogic.Requests.TagRequest;
using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
using BlogAspNetMVC.BusinessLogic.ViewModels;
using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// конструктор: настройка соответствия сущностей 
        /// </summary>
        public MappingProfile()
        {
            RequestToDbModelCreateMap();
            RequestToDbQueryCreateMap();
            DbModelToBusinessViewModelCreateMap();
        }

        /// <summary>
        /// Создание маппера для преобразования модели запроса из бизнес-логики в Модель базы данных
        /// </summary>
        private void RequestToDbModelCreateMap()
        {
            CreateMap<SignUpRequest, User>();
            CreateMap<AddNewArticleRequest, Article>();
            CreateMap<AddNewCommentRequest, Comment>();
            CreateMap<AddNewTagRequest, Tag>();
            CreateMap<AddNewRoleRequest, Role>();

        }

        /// <summary>
        /// Создание маппера для преобразования модели запроса из бизнес-логики в модель запроса для базы данных
        /// </summary>
        private void RequestToDbQueryCreateMap()
        {
            CreateMap<ChangeUserRequest, UpdateUserQuery>();
            CreateMap<ChangeUserNameRequest, UpdateUserQuery>();
            CreateMap<ChangePasswordRequest, UpdateUserQuery>();
            CreateMap<ChangeArticleRequest, UpdateArticleQuery>();
            CreateMap<ChangeCommentRequest, UpdateCommentQuery>();
            CreateMap<ChangeRoleRequest, UpdateRoleQuery>();
            CreateMap<ChangeUserRoleRequest, UpdateUserQuery>();
        }

        /// <summary>
        /// Создание маппера для преобразования модели базы данных в бизнес-модель
        /// </summary>
        private void DbModelToBusinessViewModelCreateMap()
        {
            CreateMap<User, UserViewModel>()
                           .ForMember(uv => uv.RoleName,
                                   opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<Tag, TagViewModel>();
            CreateMap<Article, ArticleViewModel>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Role, RoleViewModel>();
        }
    }
}
