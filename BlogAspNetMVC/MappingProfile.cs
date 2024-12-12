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
            CreateMap<User, UserViewModel>()
                            .ForMember(uv => uv.CommentsId,
                                    opt => opt.MapFrom(src => src.Comments.Select(c => c.Id).ToList()))
                            .ForMember(uv => uv.ArticlesId,
                                    opt => opt.MapFrom(src => src.Articles.Select(c => c.Id).ToList()))
                            .ForMember(uv => uv.RoleName,
                                    opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<Tag, TagViewModel>()
                .ForMember(tv => tv.ArticlesId, opt => opt.MapFrom(src => src.Articles.Select(a => a.Id)));
            CreateMap<ChangeUserRoleRequest, UpdateUserQuery>();

        }
    }
}
