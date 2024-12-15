using BlogAspNetMVC.BusinessLogic.Requests.TagRequest;
using BlogAspNetMVC.BusinessLogic.Services;
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

        /// <summary>
        /// Создать новый тег
        /// </summary>
        /// <param name="addNewTagRequest">Модель запроса на добавление тега</param>
        /// <returns></returns>
        [Authorize(Roles = "admin, moderator")]
        [HttpPost]
        [Route("CreateNewTag")]
        public async Task<IActionResult> CreateNewTag(
            [FromBody]
            AddNewTagRequest addNewTagRequest)
        {
            try
            {
                var result = await _tagService.AddTag(addNewTagRequest);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Изменить тег
        /// </summary>
        /// <param name="changeTagRequest">Модель запроса на изменение тега</param>
        /// <returns></returns>
        [Authorize(Roles = "admin, moderator")]
        [HttpPut]
        [Route("ChangeTag")]
        public async Task<IActionResult> ChangeTag(
            [FromBody]
            ChangeTagRequest changeTagRequest)
        {
            try
            {
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

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.ToString());
            }

        }

        /// <summary>
        /// Получить тег
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetTagById{guid}")]
        public async Task<IActionResult> GetTagById(
            [FromRoute] Guid guid)
        {
            try
            {
                var result = await _tagService.GetTagById(guid);

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
