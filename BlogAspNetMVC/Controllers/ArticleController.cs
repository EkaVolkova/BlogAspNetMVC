using BlogAspNetMVC.BusinessLogic.Exceptions.ArticleExceptions;
using BlogAspNetMVC.BusinessLogic.Exceptions.UserExceptions;
using BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests;
using BlogAspNetMVC.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    /// <summary>
    /// Контроллер для работы со статьей
    /// </summary>
    [AllowAnonymous]
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        IArticleService _articleService;
        ILogger<ArticleController> _logger;

        public ArticleController(ILogger<ArticleController> logger, IArticleService articleService)
        {
            _articleService = articleService;
            _logger = logger;
        }

        /// <summary>
        /// Создать новую статью
        /// </summary>
        /// <param name="addNewArticleRequest">Запрос на добавление статьи</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateNewArticle")]
        public async Task<IActionResult> CreateNewArticle(
            [FromBody]
            AddNewArticleRequest addNewArticleRequest)
        {
            try
            {
                var result = await _articleService.AddArticle(addNewArticleRequest);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }



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
        [HttpGet]
        [Route("GetAllArticle")]
        public async Task<IActionResult> GetAllArticles()
        {
            try
            {
                var result = await _articleService.GetAllArticles();

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

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
        [Route("GetArticleByName{name}")]
        public async Task<IActionResult> GetArticleByName(
            [FromRoute] string name)
        {
            try
            {
                var result = await _articleService.GetArticleByName(name);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

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
