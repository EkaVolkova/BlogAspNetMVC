using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Exceptions.ArticleExceptions;
using BlogAspNetMVC.BusinessLogic.Exceptions.TagExceptions;
using BlogAspNetMVC.BusinessLogic.Exceptions.UserExceptions;
using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.BusinessLogic.ViewModels;
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

        /// <summary>
        /// Добавить статью
        /// </summary>
        /// <param name="addNewArticleRequest">Модель запроса на добавление статьи</param>
        /// <returns></returns>
        public async Task<IActionResult> AddArticle(AddNewArticleRequest addNewArticleRequest)
        {
            var article = await _articleRepository.GetByName(addNewArticleRequest.Name);

            //Если статья с аналогичным названием уже существует, ее нельзя добавить
            if (!(article is null))
            {
                throw new ArticleAlreadyExistException($"Статья с названием {addNewArticleRequest.Name} уже существует");
            }

            var user = await _userRepository.GetById(addNewArticleRequest.AuthorId);
            //Если автора статьи не существует, то статью нельзя добавить
            if(user is null)
            {
                throw new UserNotFoundException($"Пользователь c Id {addNewArticleRequest.AuthorId} не найден");
            }

            List<Tag> tags = new List<Tag>();

            if(!(addNewArticleRequest.Tags is null) && addNewArticleRequest.Tags.Count > 0)
            {
                foreach (var tag in addNewArticleRequest.Tags)
                {
                    var tempTag = await _tagRepository.GetByName(tag);

                    //Если тега не существует, то его нельзя добавить
                    if(tempTag is null)
                        throw new TagNotFoundException($"Тег c тестом {tag} не найден");

                    tags.Add(tempTag);

                }
            }

            article = _mapper.Map<AddNewArticleRequest, Article>(addNewArticleRequest);

            await _articleRepository.Create(article, user, tags);

            article = await _articleRepository.GetByName(addNewArticleRequest.Name);
            
            var articleView = _mapper.Map<Article, ArticleViewModel>(article);

            return new ObjectResult(articleView) { StatusCode = 200 };
        }

        /// <summary>
        /// Изменить статью
        /// </summary>
        /// <param name="changeArticleRequest">Модель запроса на обновление статьи</param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeArticle(ChangeArticleRequest changeArticleRequest)
        {
            var article = await _articleRepository.GetByName(changeArticleRequest.OldName);
            //Если статьи не существует, статью нельзя изменить
            if (article is null)
                throw new ArticleNotFoundException($"Статья с названием {changeArticleRequest.OldName} не найдена");

            List<Tag> tags = new List<Tag>();

            if (changeArticleRequest.NewTags.Count > 0)
            {
                foreach (var tag in changeArticleRequest.NewTags)
                {
                    var tempTag = await _tagRepository.GetByName(tag);

                    //Если тега не существует, статью нельзя изменить
                    if (tempTag is null)
                        throw new TagNotFoundException($"Тег c тестом {tag} не найден");

                    tags.Add(tempTag);

                }
            }
            var query = _mapper.Map<ChangeArticleRequest, UpdateArticleQuery>(changeArticleRequest);
            query.NewTags = tags;

            await _articleRepository.UpdateArticle(article, query);

            article = await _articleRepository.GetById(article.Id);

            var articleView = _mapper.Map<Article, ArticleViewModel>(article);

            return new ObjectResult(articleView) { StatusCode = 200 };
        }

        /// <summary>
        /// Изменить теги
        /// </summary>
        /// <param name="changeArticleTagsRequest">Модель запроса на обновление тегов статьи</param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeArticleTags(ChangeArticleTagsRequest changeArticleTagsRequest)
        {
            var article = await _articleRepository.GetByName(changeArticleTagsRequest.Name);
            //Если статьи не существует, статью нельзя изменить
            if (article is null)
                throw new ArticleNotFoundException($"Статья с названием {changeArticleTagsRequest.Name} не найдена");

            List<Tag> tags = new List<Tag>();

            if (changeArticleTagsRequest.Tags.Count > 0)
            {
                foreach (var tag in changeArticleTagsRequest.Tags)
                {
                    var tempTag = await _tagRepository.GetByName(tag);

                    //Если тега не существует, статью нельзя изменить
                    if (tempTag is null)
                        throw new TagNotFoundException($"Тег c тестом {tag} не найден");

                    tags.Add(tempTag);

                }
            }
            var query = new UpdateArticleQuery { NewTags = tags };

            await _articleRepository.UpdateArticle(article, query);

            article = await _articleRepository.GetByName(changeArticleTagsRequest.Name);

            var articleView = _mapper.Map<Article, ArticleViewModel>(article);

            return new ObjectResult(articleView) { StatusCode = 200 };
        }

        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteArticle(Guid guid)
        {
            var article = await _articleRepository.GetById(guid);

            //Если статьи не существует, статью нельзя удалить
            if (article is null)
                throw new ArticleNotFoundException($"Статья с Id {guid} не найдена");

            await _articleRepository.DeleteArticle(article);
            return new ObjectResult($"Статья {article.Name} удалена") { StatusCode = 200 };
        }

        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="name">Название статьи</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteArticle(string name)
        {
            var article = await _articleRepository.GetByName(name);

            //Если статьи не существует, статью нельзя удалить
            if (article is null)
                throw new ArticleNotFoundException($"Статья с названием {name} не найдена");

            await _articleRepository.DeleteArticle(article);
            return new ObjectResult($"Статья {article.Name} удалена") { StatusCode = 200 };

        }

        /// <summary>
        /// Получить список всех статей
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleRepository.GetAllArticles();
            if (articles is null || articles.Count == 0)
                return new ObjectResult("Нет статей") { StatusCode = 400 };

            return new ObjectResult(articles) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить статью 
        /// </summary>
        /// <param name="name">Название статьи/param>
        /// <returns></returns>
        public async Task<IActionResult> GetArticleByName(string name)
        {
            var article = await _articleRepository.GetByName(name);

            //Если статьи не существует
            if (article is null)
                throw new ArticleNotFoundException($"Статья с названием {name} не найдена");

            var articleView = _mapper.Map<Article, ArticleViewModel>(article);

            return new ObjectResult(articleView) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить статью
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        public async Task<IActionResult> GetArticleById(Guid guid)
        {
            var article = await _articleRepository.GetById(guid);

            //Если статьи не существует
            if (article is null)
                throw new ArticleNotFoundException($"Статья с Id {guid} не найдена");

            var articleView = _mapper.Map<Article, ArticleViewModel>(article);

            return new ObjectResult(articleView) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить список статей пользователя
        /// </summary>
        /// <param name="userName">UserName пользователя</param>
        /// <returns></returns>
        public async Task<IActionResult> GetArticlesByUserName(string userName)
        {
            var allArticles = await _articleRepository.GetAllArticles();
            var articlesByUser = allArticles.Where(a => a.Author.UserName == userName).ToList();
            if (articlesByUser is null || articlesByUser.Count == 0)
                return new ObjectResult($"Нет статей пользователя с именем пользователя {userName}") { StatusCode = 400 };

            return new ObjectResult(articlesByUser) { StatusCode = 200 };


        }

        /// <summary>
        /// Получить список статей пользователя
        /// </summary>
        /// <param name="guid">Идентификатор пользователя</param>
        /// <returns></returns>
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
