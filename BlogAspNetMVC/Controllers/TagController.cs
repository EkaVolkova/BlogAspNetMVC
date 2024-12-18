using BlogAspNetMVC.BusinessLogic.Requests.TagRequest;
using BlogAspNetMVC.BusinessLogic.Services;
using BlogAspNetMVC.BusinessLogic.Validation.TagRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    
    [Route("[controller]")]
    public class TagController : Controller
    {
        readonly ITagService _tagService;
        private readonly ILogger<TagController> _logger;

        public TagController(ILogger<TagController> logger, ITagService tagService)
        {
            _tagService = tagService;
            _logger = logger;
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet]
        [Route("CreateNewTag")]
        public IActionResult CreateNewTag()
        {
            return View();
        }


        /// <summary>
        /// Создать новый тег
        /// </summary>
        /// <param name="addNewTagRequest">Модель запроса на добавление тега</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("CreateNewTag")]
        public async Task<IActionResult> CreateNewTag(
            AddNewTagRequest addNewTagRequest)
        {
            try
            {
                var validator = new AddNewTagRequestValidation();
                var validationResult = validator.Validate(addNewTagRequest);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка добавления тега {validationResult.Errors}");
                    ModelState.AddModelError(string.Empty, validationResult.Errors.ToString());
                    return View();
                }
                var result = await _tagService.AddTag(addNewTagRequest);
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка добавления тега {ex}");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(addNewTagRequest);
        }

        /// <summary>
        /// Изменить тег
        /// </summary>
        /// <param name="changeTagRequest">Модель запроса на изменение тега</param>
        /// <returns></returns>
        [Authorize(Roles = "admin, moderator")]
        [HttpPost]
        [Route("ChangeTag")]
        public async Task<IActionResult> ChangeTag(
            [FromBody]
            ChangeTagRequest changeTagRequest)
        {
            try
            {
                var validator = new ChangeTagRequestValidation();
                var validationResult = validator.Validate(changeTagRequest);

                if (!validationResult.IsValid)
                {
                    _logger.LogError($"Ошибка валидации {validationResult.Errors}");
                    ModelState.AddModelError(string.Empty, validationResult.Errors.ToString());
                    return View();
                }
                var result = await _tagService.ChangeTag(changeTagRequest);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        [Authorize(Roles = "admin, moderator")]
        [HttpDelete]
        [Route("DeleteTag{guid}")]
        public async Task<IActionResult> DeleteTag(
            [FromRoute] Guid guid)
        {
            try
            {
                await _tagService.DeleteTag(guid);

                return StatusCode(200, "Тег удален");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Получить список всех тегов
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllTags")]
        public async Task<IActionResult> GetAllTags()
        {
            try
            {
                var result = await _tagService.GetAllTags();
                return View(result); 

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка добавления тега {ex}");
                ModelState.AddModelError(string.Empty, "Некорректное название.");
            }
            return View();
        }

        /// <summary>
        /// Получить тег
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetTagById")]
        public async Task<IActionResult> GetTagById(Guid guid)
        {
            try
            {
                var result = await _tagService.GetTagById(guid);

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка добавления тега {ex}");
                ModelState.AddModelError(string.Empty, "Некорректное название.");
            }
            return View();
        }

        /// <summary>
        /// Получить список тегов к выбранной статье
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetTagsByArtcleId{guid}")]
        public async Task<IActionResult> GetTagsByArtcleId(
            [FromRoute] Guid guid)
        {
            try
            {
                var result = await _tagService.GetTagsByArticleId(guid);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Получить список тегов к выбранной статье
        /// </summary>
        /// <param name="name">Название статьи</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetTagByArtcleName{name}")]
        public async Task<IActionResult> GetTagByArtcleName(
            [FromRoute] string name)
        {
            try
            {
                var result = await _tagService.GetTagsByArticleName(name);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

    }
}
