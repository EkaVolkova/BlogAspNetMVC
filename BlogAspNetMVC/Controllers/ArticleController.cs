using BlogAspNetMVC.BusinessLogic.Exceptions.ArticleExceptions;
using BlogAspNetMVC.BusinessLogic.Exceptions.UserExceptions;
using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.BusinessLogic.Services;
using BlogAspNetMVC.BusinessLogic.Validation.ArticleRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    /// <summary>
    /// Контроллер для работы со статьей
    /// </summary>
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        readonly IArticleService _articleService;
        readonly IUserService _userService;
        readonly ILogger<ArticleController> _logger;

        public ArticleController(ILogger<ArticleController> logger, IArticleService articleService, IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;
            _logger = logger;
        }

        [Authorize(Roles = "admin, moderator, user")]
        [HttpGet]
        [Route("CreateNewArticle")]
        public IActionResult CreateNewArticle()
        {
            return View();
        }

        /// <summary>
        /// Создать новую статью
        /// </summary>
        /// <param name="addNewArticleRequest">Запрос на добавление статьи</param>
        /// <returns></returns>
        [Authorize(Roles = "admin, moderator, user")]
        [HttpPost]
        [Route("CreateNewArticle")]
        public async Task<IActionResult> CreateNewArticle(
            AddNewArticleRequest addNewArticleRequest)
        {
            try
            {
                var userName = HttpContext.User.Claims.ToList()[0].Value;

                var user = await _userService.GetByUserName(userName);
                // Десериализуем теги из JSON
                addNewArticleRequest.DeserializeTags(addNewArticleRequest.TagsJson); // Предполагается, что вы передаете JSON в поле TagsJson

                addNewArticleRequest.AuthorId = user.Id;

                var validator = new AddNewArticleRequestValidation();
                var validationResult = validator.Validate(addNewArticleRequest);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка добавления тега");
                    ModelState.AddModelError(string.Empty, "Некорректное название.");
                    return BadRequest(validationResult.Errors);
                }

                var result = await _articleService.AddArticle(addNewArticleRequest);
                return View(addNewArticleRequest);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка добавления тега {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();

        }

        /// <summary>
        /// Отредактировать статью
        /// </summary>
        /// <param name="changeArticleRequest">Запрос на редактирование статьи</param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeArticle")]
        public async Task<IActionResult> ChangeArticle(
            [FromBody]
            ChangeArticleRequest changeArticleRequest)
        {
            try
            {
                var validator = new ChangeArticleRequestValidation();
                var validationResult = validator.Validate(changeArticleRequest);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var result = await _articleService.ChangeArticle(changeArticleRequest);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Удалить статью по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteArticle{guid}")]
        public async Task<IActionResult> DeleteArticle(
            [FromRoute] 
            Guid guid)
        {
            try
            {
                await _articleService.DeleteArticle(guid);

                return StatusCode(200, "Статья успешно удалена");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Удалить статью по названию
        /// </summary>
        /// <param name="name">название статьи</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteArticle{name}")]
        public async Task<IActionResult> DeleteArticle(
            [FromRoute] string name)
        {
            try
            {
                await _articleService.DeleteArticle(name);

                return StatusCode(200, "Статья успешно удалена");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }


        /// <summary>
        /// Получить все статьи
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllArticles")]
        public async Task<IActionResult> GetAllArticles()
        {
            try
            {
                var result = await _articleService.GetAllArticles();

                return View(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return View();
        }

        /// <summary>
        /// Получить статью по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetArticleById{guid}")]
        public async Task<IActionResult> GetArticleById(
            [FromRoute] Guid guid)
        {
            try
            {
                var result = await _articleService.GetArticleById(guid);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Получить статью по названию
        /// </summary>
        /// <param name="name">Название статьи</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetArticleByName")]
        public async Task<IActionResult> GetArticleByName(string name)
        {
            try
            {
                var result = await _articleService.GetArticleByName(name);
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка получения статьи {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        /// <summary>
        /// Получить статьи по идентификатору автора
        /// </summary>
        /// <param name="guid">Идентификатор автора</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetArticlesByAuthorId{guid}")]
        public async Task<IActionResult> GetArticlesByAuthorId(
            [FromRoute] Guid guid)
        {
            try
            {
                var result = await _articleService.GetArticlesByAuthorId(guid);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Получить статьи по UserName автора
        /// </summary>
        /// <param name="name">UserName автора</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetArticlesByAuthorUserName{name}")]
        public async Task<IActionResult> GetArticlesByAuthorUserName(
            [FromRoute] string name)
        {
            try
            {
                var result = await _articleService.GetArticlesByUserName(name);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }
    }
}
