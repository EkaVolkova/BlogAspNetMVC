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
            ViewModelToDbQueryCreateMap();
            BusinessToDbModelViewModelCreateMap();
            DbModelToBusinessViewModelCreateMap();
        }

        /// <summary>
        /// Создание маппера для преобразования модели запроса из бизнес-логики в Модель базы данных
        /// </summary>
        private void RequestToDbModelCreateMap()
        {
            CreateMap<SignUpRequest, User>();
            CreateMap<AddNewArticleRequest, Article>()
                .ForMember(m => m.Tags, r => r.Ignore());
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
        /// Создание маппера для преобразования модели запроса из бизнес-логики в модель запроса для базы данных
        /// </summary>
        private void ViewModelToDbQueryCreateMap()
        {
            CreateMap<UserViewModel, UpdateUserQuery>()
                .ForMember(desc => desc.NewName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(desc => desc.NewPassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(desc => desc.NewRoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(desc => desc.NewEmail, opt => opt.MapFrom(src => src.Email));
            ;
            CreateMap<ArticleViewModel, UpdateArticleQuery>()
                .ForMember(desc => desc.NewName, opt => opt.MapFrom(src => src.Name))
                .ForMember(desc => desc.NewText, opt => opt.MapFrom(src => src.Text))
                .ForMember(desc => desc.NewTags, opt => opt.MapFrom(src => src.Tags));

            CreateMap<CommentViewModel, UpdateCommentQuery>()
                .ForMember(desc => desc.NewText, opt => opt.MapFrom(src => src.Text));
               
            CreateMap<RoleViewModel, UpdateRoleQuery>()
                .ForMember(desc => desc.NewName, opt => opt.MapFrom(src => src.Name));
        }

        /// <summary>
        /// Создание маппера для преобразования модели базы данных в бизнес-модель
        /// </summary>
        private void DbModelToBusinessViewModelCreateMap()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Tag, TagViewModel>();
            CreateMap<Article, ArticleViewModel>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Role, RoleViewModel>();
        }

        /// <summary>
        /// Создание маппера для преобразования бизнес-модели в модель базы данных
        /// </summary>
        private void BusinessToDbModelViewModelCreateMap()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<TagViewModel, Tag>();
            CreateMap<ArticleViewModel, Article>();
            CreateMap<CommentViewModel, Comment>();
            CreateMap<RoleViewModel, Role>();
        }
    }
}
