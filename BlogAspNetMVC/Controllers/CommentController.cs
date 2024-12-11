using BlogAspNetMVC.BusinessLogic.Requests.CommentRequest;
using BlogAspNetMVC.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger, ICommentService commentService)
        {
            _commentService = commentService;
            _logger = logger;
        }

        /// <summary>
        /// Создать новый комментарий
        /// </summary>
        /// <param name="addNewCommentRequest">Запрос на создание нового комментария</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateNewComment")]
        public async Task<IActionResult> CreateNewComment(
            [FromBody]
            AddNewCommentRequest addNewCommentRequest)
        {
            var result = await _commentService.AddComment(addNewCommentRequest);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Изменить комментарий
        /// </summary>
        /// <param name="changeCommentRequest">Запрос на изменение комментария</param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeComment")]
        public async Task<IActionResult> ChangeComment(
            [FromBody]
            ChangeCommentRequest changeCommentRequest)
        {
            var result = await _commentService.ChangeComment(changeCommentRequest);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="guid">Идентификатор комментария</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteComment{guid}")]
        public async Task<IActionResult> DeleteComment(
            [FromRoute] Guid guid)
        {
            var result = await _commentService.DeleteComment(guid);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить все комментарии
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllComments")]
        public async Task<IActionResult> GetAllComments()
        {
            var result = await _commentService.GetAllComments();

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить комментарий по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор комментария</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCommentById{guid}")]
        public async Task<IActionResult> GetCommentById(
            [FromRoute] Guid guid)
        {
            var result = await _commentService.GetCommentById(guid);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить комментарии по идентификатору автора
        /// </summary>
        /// <param name="guid">Идентификатор автора</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCommentsByAuthorId{guid}")]
        public async Task<IActionResult> GetCommentsByAuthorId(
            [FromRoute] Guid guid)
        {
            var result = await _commentService.GetCommentsByAuthorId(guid);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить комментарии по UserName автора
        /// </summary>
        /// <param name="name">UserName автора</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCommentByAuthorUserName{name}")]
        public async Task<IActionResult> GetCommentByAuthorUserName(
            [FromRoute] string name)
        {
            var result = await _commentService.GetCommentsByUserName(name);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить комментарии по идентификатору статьи
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCommentsByArtcleId{guid}")]
        public async Task<IActionResult> GetCommentsByArtcleId(
            [FromRoute] Guid guid)
        {
            var result = await _commentService.GetCommentsByArticleId(guid);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить комментарии по названию статьи
        /// </summary>
        /// <param name="name">Название статьи</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCommentByArtcleUserName{name}")]
        public async Task<IActionResult> GetCommentByArtcleUserName(
            [FromRoute] string name)
        {
            var result = await _commentService.GetCommentsByArticleName(name);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

    }
}
