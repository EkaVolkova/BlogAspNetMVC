using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.BusinessLogic.Requests.CommentRequest;
using BlogAspNetMVC.BusinessLogic.Requests.RoleRequest;
using BlogAspNetMVC.BusinessLogic.Requests.TagRequest;
using BlogAspNetMVC.BusinessLogic.Requests.UserRequests;
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
            CreateMap<SignUpRequest, User>();
            CreateMap<AddNewArticleRequest, Article>();
            CreateMap<AddNewCommentRequest, Comment>();
            CreateMap<AddNewTagRequest, Tag>();
            CreateMap<AddNewRoleRequest, Role>();
            CreateMap<ChangeUserNameRequest, UpdateUserQuery>();
            CreateMap<ChangePasswordRequest, UpdateUserQuery>();
            CreateMap<ChangeArticleRequest, UpdateArticleQuery>();
            CreateMap<ChangeCommentRequest, UpdateCommentQuery>();
            CreateMap<ChangeRoleRequest, UpdateRoleQuery>();
            CreateMap<ChangeUserRoleRequest, UpdateUserQuery>();

        }
    }
}
