using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Requests.CommentRequest;
using BlogAspNetMVC.Data.Models;
using BlogAspNetMVC.Data.Queries;
using BlogAspNetMVC.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public class CommentService : ICommentService
    {
        IArticleRepository _articleRepository;
        IUserRepository _userRepository;
        ICommentRepository _commentRepository;
        IMapper _mapper;

        public CommentService(
                                IArticleRepository articleRepository,
                                IUserRepository userRepository,
                                ICommentRepository commentRepository,
                                IMapper mapper)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавить комментарий
        /// </summary>
        /// <param name="addNewCommentRequest">Модель запроса добавления комментария</param>
        /// <returns></returns>
        public async Task<IActionResult> AddComment(AddNewCommentRequest addNewCommentRequest)
        {
            var comment = await _commentRepository.GetById(addNewCommentRequest.Guid);
            if (!(comment is null))
                return new ObjectResult($"Комментарий с Id \"{addNewCommentRequest.Guid}\" не найден") { StatusCode = 400 };

            var author = await _userRepository.GetById(addNewCommentRequest.AuthorId);
            if (author is null)
            {
                return new ObjectResult("Автор комментария не найден") { StatusCode = 400 };
            }

            //Может быть null, его не проверяем
            var parComment = await _commentRepository.GetById(addNewCommentRequest.ParentCommentId);

            var article = await _articleRepository.GetById(addNewCommentRequest.ArtcleId);
            if (article is null)
            {
                return new ObjectResult($"Статья с id {addNewCommentRequest.ArtcleId} не найдена") { StatusCode = 400 };
            }

            comment = _mapper.Map<AddNewCommentRequest, Comment>(addNewCommentRequest);

            await _commentRepository.Create(comment, article, author, parComment);

            return new ObjectResult($"Комментарий создан") { StatusCode = 200 };
        }

        /// <summary>
        /// Изменить комментарий
        /// </summary>
        /// <param name="changeCommentRequest">Модель запроса на изменение комментария</param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeComment(ChangeCommentRequest changeCommentRequest)
        {
            var comment = await _commentRepository.GetById(changeCommentRequest.Id);
            if (comment is null)
                return new ObjectResult($"Комментарий с Id \"{changeCommentRequest.Id}\" не найден") { StatusCode = 400 };


            var query = _mapper.Map<ChangeCommentRequest, UpdateCommentQuery>(changeCommentRequest);
            await _commentRepository.UpdateComment(comment, query);
            return new ObjectResult($"Комментарий с Id {changeCommentRequest.Id} обновлен") { StatusCode = 200 };
        }

        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="guid">Идентификатор комментария</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteComment(Guid guid)
        {
            var comment = await _commentRepository.GetById(guid);
            if (comment is null)
                return new ObjectResult($"Комментарий с Id \"{guid}\" не найден") { StatusCode = 400 };

            await _commentRepository.DeleteComment(comment);
            return new ObjectResult($"Комментарий с Id {guid} удален") { StatusCode = 200 };
        }

        /// <summary>
        /// Получить список всех комментариев
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepository.GetAllComments();
            if (comments is null || comments.Count == 0)
                return new ObjectResult("Нет комментариев") { StatusCode = 400 };

            return new ObjectResult(comments) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить комментарий по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор комментария</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCommentById(Guid guid)
        {
            var comment = await _commentRepository.GetById(guid);
            if (comment is null)
                return new ObjectResult($"Комментарий с Id \"{guid}\" не найден") { StatusCode = 400 };

            return new ObjectResult(comment) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить все комментарии к статье
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCommentsByArticleId(Guid guid)
        {
            var comments = await _commentRepository.GetAllComments();
            
            if (comments is null)
                return new ObjectResult($"Комментарии к статье с Id \"{guid}\" не найдены") { StatusCode = 400 };
            var filteredComments = comments.Where(c => c.ArticleId == guid);
            
            if (filteredComments is null)
                return new ObjectResult($"Комментарии к статье с Id \"{guid}\" не найдены") { StatusCode = 400 };

            return new ObjectResult(filteredComments) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить все комментарии к статье
        /// </summary>
        /// <param name="articleName">Название статьи</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCommentsByArticleName(string articleName)
        {
            var comments = await _commentRepository.GetAllComments();

            if (comments is null)
                return new ObjectResult($"Комментарии к статье с названием \"{articleName}\" не найдены") { StatusCode = 400 };
            var filteredComments = comments.Where(c => c.Article.Name == articleName);

            if (filteredComments is null)
                return new ObjectResult($"Комментарии к статье с названием \"{articleName}\" не найдены") { StatusCode = 400 };

            return new ObjectResult(filteredComments) { StatusCode = 200 };

        }

        /// <summary>
        /// Получить все комментарии пользователя
        /// </summary>
        /// <param name="guid">Идентификатор автора комментариев</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCommentsByAuthorId(Guid guid)
        {
            var comments = await _commentRepository.GetAllComments();

            if (comments is null)
                return new ObjectResult($"Комментарии автора с Id \"{guid}\" не найдены") { StatusCode = 400 };
            var filteredComments = comments.Where(c => c.Author.Id == guid);

            if (filteredComments is null)
                return new ObjectResult($"Комментарий автора с Id \"{guid}\" не найдены") { StatusCode = 400 };

            return new ObjectResult(filteredComments) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить все комментарии пользователя
        /// </summary>
        /// <param name="userName">UserName автора комментариев</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCommentsByUserName(string userName)
        {
            var comments = await _commentRepository.GetAllComments();

            if (comments is null)
                return new ObjectResult($"Комментарии автора с UserName \"{userName}\" не найдены") { StatusCode = 400 };
            var filteredComments = comments.Where(c => c.Author.UserName == userName);

            if (filteredComments is null)
                return new ObjectResult($"Комментарий автора с UserName \"{userName}\" не найдены") { StatusCode = 400 };

            return new ObjectResult(filteredComments) { StatusCode = 200 };
        }
    }

}
