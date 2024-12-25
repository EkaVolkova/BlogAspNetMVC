using BlogAspNetMVC.BusinessLogic.Exceptions.ArticleExceptions;
using BlogAspNetMVC.BusinessLogic.Exceptions.UserExceptions;
using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.BusinessLogic.Services;
using BlogAspNetMVC.BusinessLogic.Validation.ArticleRequests;
using BlogAspNetMVC.BusinessLogic.ViewModels;
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

        [Authorize]
        [HttpGet]
        [Route("CreateNewArticle")]
        public IActionResult CreateNewArticle()
        {
            _logger.LogTrace("Открыта вкладка создания статьи");
            return View();
        }

        /// <summary>
        /// Создать новую статью
        /// </summary>
        /// <param name="addNewArticleRequest">Запрос на добавление статьи</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("CreateNewArticle")]
        public async Task<IActionResult> CreateNewArticle(
            AddNewArticleRequest addNewArticleRequest)
        {
            try
            {
                _logger.LogInformation($"Попытка создания статьи {addNewArticleRequest.Name}");

                var userName = HttpContext.User.Claims.ToList()[0].Value;

                var user = await _userService.GetByUserName(userName);
                // Десериализуем теги из JSON
                addNewArticleRequest.DeserializeTags(addNewArticleRequest.TagsJson); // Предполагается, что вы передаете JSON в поле TagsJson

                addNewArticleRequest.AuthorId = user.Id;

                var validator = new AddNewArticleRequestValidation();
                var validationResult = validator.Validate(addNewArticleRequest);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка валидации. {validationResult.Errors}");
                    ModelState.AddModelError(string.Empty, validationResult.Errors.ToString());
                    return BadRequest(validationResult.Errors);
                }

                var result = await _articleService.AddArticle(addNewArticleRequest);
                return RedirectToAction("GetArticleByName", "Article", new { name = result.Name });

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
        /// <param name="articleViewModel">Запрос на редактирование статьи</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ChangeArticle")]
        public async Task<IActionResult> ChangeArticle(
            [FromBody]
            ArticleViewModel articleViewModel)
        {
            try
            {
                _logger.LogTrace("Открыта вкладка обновления статьи");
                _logger.LogInformation($"Попытка обновления статьи {articleViewModel.Name}");

                var result = await _articleService.ChangeArticle(articleViewModel);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка изменения статьи {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();


        }

        /// <summary>
        /// Удалить статью по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор статьи</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("DeleteArticle")]
        public async Task<IActionResult> DeleteArticle(Guid id, bool confirm = true)
        {
            if(confirm)
                await DeleteArticle(id);

            return RedirectToAction("GetAllArticles", "Article");
        }

        /// <summary>
        /// Удалить статью по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор статьи</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("DeleteArticle")]
        public async Task<IActionResult> DeleteArticle(
            Guid id)
        {
            var Model = await _articleService.GetArticleById(id);
            var i = User.Identity?.Name == Model.Author.UserName;
            try
            {
                await _articleService.DeleteArticle(id);
                _logger.LogInformation($"Статья с id {id} удалена");

                return RedirectToAction("GetAllArticles", "Article");
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
        [Authorize]
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
        [AllowAnonymous]
        [HttpGet]
        [Route("GetArticleById")]
        public async Task<IActionResult> GetArticleById(Guid guid)
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpGet]
        [Route("GetArticlesByAuthorUserName")]
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
