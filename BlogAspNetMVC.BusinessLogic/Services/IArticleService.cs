using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public interface IArticleService
    {
        Task<IActionResult> AddArticle(AddNewArticleRequest addNewArticleRequest);
        Task<IActionResult> ChangeArticle(ChangeArticleRequest changeArticleRequest);
        Task<IActionResult> ChangeArticleTags(ChangeArticleTagsRequest changeArticleTagsRequest);
        Task<IActionResult> DeleteArticle(Guid guid);
        Task<IActionResult> DeleteArticle(string name);
        Task<IActionResult> GetAllArticles();
        Task<IActionResult> GetArticleByName(string name);
        Task<IActionResult> GetArticleById(Guid guid);
        Task<IActionResult> GetArticlesByUserName(string userName);
        Task<IActionResult> GetArticlesByAuthorId(Guid guid);
    }

}
