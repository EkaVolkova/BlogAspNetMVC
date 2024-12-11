using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using BlogAspNetMVC.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public class ArticleService : IArticleService
    {
        IArticleRepository _articleRepository;
        IUserRepository _userRepository;
        ITagRepository _tagRepository;
        IMapper _mapper;

        public ArticleService(
                                IArticleRepository articleRepository,
                                IUserRepository userRepository,
                                ITagRepository tagRepository,
                                IMapper mapper)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> AddArticle(AddNewArticleRequest addNewArticleRequest)
        {
            var article = await _articleRepository.GetByName(addNewArticleRequest.Name);
            if (!(article is null))
            {
                return new ObjectResult("Статья с таким названием уже существует") { StatusCode = 400 };
            }

            var user = await _userRepository.GetById(addNewArticleRequest.AuthorId);
            if(user is null)
            {
                return new ObjectResult("Пользователь не найден") { StatusCode = 400 };
            }

            List<Tag> tags = new List<Tag>();

            if(!(addNewArticleRequest.Tags is null) && addNewArticleRequest.Tags.Count > 0)
            {
                foreach (var tag in addNewArticleRequest.Tags)
                {
                    var tempTag = await _tagRepository.GetByName(tag);

                    if(tempTag is null)
                        return new ObjectResult("Тег не найден") { StatusCode = 400 };

                    tags.Add(tempTag);

                }
            }

            article = _mapper.Map<AddNewArticleRequest, Article>(addNewArticleRequest);

            await _articleRepository.Create(article, user, tags);
            return new ObjectResult($"Статья {article.Name} создана") { StatusCode = 200 };
        }
        public async Task<IActionResult> ChangeArticle(ChangeArticleRequest changeArticleRequest)
        {
            var article = await _articleRepository.GetByName(changeArticleRequest.OldName);
            if(article is null)
                return new ObjectResult($"Статья с названием \"{changeArticleRequest.OldName}\" не найдена") { StatusCode = 400 };

            List<Tag> tags = new List<Tag>();

            if (changeArticleRequest.NewTags.Count > 0)
            {
                foreach (var tag in changeArticleRequest.NewTags)
                {
                    var tempTag = await _tagRepository.GetByName(tag);

                    if (tempTag is null)
                        return new ObjectResult($"Тег {tag} не найден") { StatusCode = 400 };

                    tags.Add(tempTag);

                }
            }
            var query = _mapper.Map<ChangeArticleRequest, UpdateArticleQuery>(changeArticleRequest);
            query.NewTags = tags;

            await _articleRepository.UpdateArticle(article, query);
            return new ObjectResult($"Статья {article.Name} обновлена") { StatusCode = 200 };
        }
        public async Task<IActionResult> ChangeArticleTags(ChangeArticleTagsRequest changeArticleTagsRequest)
        {
            var article = await _articleRepository.GetByName(changeArticleTagsRequest.Name);
            if (article is null)
                return new ObjectResult($"Статья с названием \"{changeArticleTagsRequest.Name}\" не найдена") { StatusCode = 400 };

            List<Tag> tags = new List<Tag>();

            if (changeArticleTagsRequest.Tags.Count > 0)
            {
                foreach (var tag in changeArticleTagsRequest.Tags)
                {
                    var tempTag = await _tagRepository.GetByName(tag);

                    if (tempTag is null)
                        return new ObjectResult($"Тег {tag} не найден") { StatusCode = 400 };

                    tags.Add(tempTag);

                }
            }
            var query = new UpdateArticleQuery { NewTags = tags };

            await _articleRepository.UpdateArticle(article, query);
            return new ObjectResult($"Статья {article.Name} обновлена") { StatusCode = 200 };
        }

        public async Task<IActionResult> DeleteArticle(Guid guid)
        {
            var article = await _articleRepository.GetById(guid);
            if (article is null)
                return new ObjectResult($"Статья с идентификатором \"{guid}\" не найдена") { StatusCode = 400 };
            
            await _articleRepository.DeleteArticle(article);
            return new ObjectResult($"Статья {article.Name} удалена") { StatusCode = 200 };
        }
        public async Task<IActionResult> DeleteArticle(string name)
        {
            var article = await _articleRepository.GetByName(name);

            if (article is null)
                return new ObjectResult($"Статья с названием \"{name}\" не найдена") { StatusCode = 400 };
            
            await _articleRepository.DeleteArticle(article);
            return new ObjectResult($"Статья {article.Name} удалена") { StatusCode = 200 };

        }
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleRepository.GetAllArticles();
            if (articles is null || articles.Count == 0)
                return new ObjectResult("Нет статей") { StatusCode = 400 };

            return new ObjectResult(articles) { StatusCode = 200 };
        }
        public async Task<IActionResult> GetArticleByName(string name)
        {
            var article = await _articleRepository.GetByName(name);
            if (article is null)
                return new ObjectResult($"Статья с именем \"{name}\" не найдена") { StatusCode = 400 };
            
            return new ObjectResult(article) { StatusCode = 200 };
        }
        public async Task<IActionResult> GetArticleById(Guid guid)
        {
            var article = await _articleRepository.GetById(guid);
            if (article is null)
                return new ObjectResult($"Статья с именем \"{guid}\" не найдена") { StatusCode = 400 };

            return new ObjectResult(article) { StatusCode = 200 };
        }

        public async Task<IActionResult> GetArticlesByUserName(string userName)
        {
            var allArticles = await _articleRepository.GetAllArticles();
            var articlesByUser = allArticles.Where(a => a.Author.UserName == userName).ToList();
            if (articlesByUser is null || articlesByUser.Count == 0)
                return new ObjectResult($"Нет статей пользователя с именем пользователя {userName}") { StatusCode = 400 };

            return new ObjectResult(articlesByUser) { StatusCode = 200 };


        }
        public async Task<IActionResult> GetArticlesByAuthorId(Guid guid)
        {
            var allArticles = await _articleRepository.GetAllArticles();
            var articlesByUser = allArticles.Where(a => a.AuthorId == guid).ToList();
            if (articlesByUser is null || articlesByUser.Count == 0)
                return new ObjectResult($"Нет статей с Id пользователя {guid}") { StatusCode = 400 };

            return new ObjectResult(articlesByUser) { StatusCode = 200 };


        }

    }
}
