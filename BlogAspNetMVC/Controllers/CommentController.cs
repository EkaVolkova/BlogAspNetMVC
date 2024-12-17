using BlogAspNetMVC.BusinessLogic.Requests.CommentRequest;
using BlogAspNetMVC.BusinessLogic.Services;
using BlogAspNetMVC.BusinessLogic.Validation.CommentRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    [Authorize(Roles = "admin, moderator, user")]//доступ к комментариям только для авторизованных пользователей
    [Route("[controller]")]
    public class CommentController : Controller
    {
        readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;
        readonly IUserService _userService;

        public CommentController(ILogger<CommentController> logger, ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("CreateNewComment")]
        public IActionResult CreateNewComment()
        {
            return View();
        }

        /// <summary>
        /// Создать новый комментарий
        /// </summary>
        /// <param name="addNewCommentRequest">Запрос на создание нового комментария</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateNewComment")]
        public async Task<IActionResult> CreateNewComment(
            AddNewCommentRequest addNewCommentRequest)
        {
            try
            {
                var userName = HttpContext.User.Claims.ToList()[0].Value;

                var user = await _userService.GetByUserName(userName);

                addNewCommentRequest.AuthorId = user.Id;

                var validator = new AddNewCommentRequestValidation();
                var validationResult = validator.Validate(addNewCommentRequest);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var result = await _commentService.AddComment(addNewCommentRequest);

                return RedirectToAction("GetArticleByName", "Article", new { name = result.Article.Name });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка входа. {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("GetArticleById", "Article", addNewCommentRequest.ArticleId);
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
            try
            {
                var validator = new ChangeCommentRequestValidation();
                var validationResult = validator.Validate(changeCommentRequest);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var result = await _commentService.ChangeComment(changeCommentRequest);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
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
            try
            {
                await _commentService.DeleteComment(guid);

                return StatusCode(200, "Комментарий удален");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }

        /// <summary>
        /// Получить все комментарии
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllComments")]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var result = await _commentService.GetAllComments();

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
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
            try
            {
                var result = await _commentService.GetCommentById(guid);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
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
            try
            {
                var result = await _commentService.GetCommentsByAuthorId(guid);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
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
            try
            {
                var result = await _commentService.GetCommentsByUserName(name);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }

        /// <summary>
        /// Получить комментарии по идентификатору статьи
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCommentsByArticleId{guid}")]
        public async Task<IActionResult> GetCommentsByArticleId(
            [FromRoute] Guid guid)
        {
            try
            {
                var result = await _commentService.GetCommentsByArticleId(guid);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
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
            try
            {
                var result = await _commentService.GetCommentsByArticleName(name);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }
        }

    }
}
