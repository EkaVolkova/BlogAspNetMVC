using BlogAspNetMVC.BusinessLogic.Requests.TagRequest;
using BlogAspNetMVC.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Controllers
{
    [Route("[controller]")]
    public class TagController : Controller
    {
        ITagService _tagService;
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
        [HttpPost]
        [Route("CreateNewTag")]
        public async Task<IActionResult> CreateNewTag(
            [FromBody]
            AddNewTagRequest addNewTagRequest)
        {
            var result = await _tagService.AddTag(addNewTagRequest);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Изменить тег
        /// </summary>
        /// <param name="changeTagRequest">Модель запроса на изменение тега</param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeTag")]
        public async Task<IActionResult> ChangeTag(
            [FromBody]
            ChangeTagRequest changeTagRequest)
        {
            var result = await _tagService.ChangeTag(changeTagRequest);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteTag{guid}")]
        public async Task<IActionResult> DeleteTag(
            [FromRoute] Guid guid)
        {
            var result = await _tagService.DeleteTag(guid);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить список всех тегов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllTags")]
        public async Task<IActionResult> GetAllTags()
        {
            var result = await _tagService.GetAllTags();

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить тег
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTagById{guid}")]
        public async Task<IActionResult> GetTagById(
            [FromRoute] Guid guid)
        {
            var result = await _tagService.GetTagById(guid);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить список тегов к выбранной статье
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTagsByArtcleId{guid}")]
        public async Task<IActionResult> GetTagsByArtcleId(
            [FromRoute] Guid guid)
        {
            var result = await _tagService.GetTagsByArticleId(guid);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

        /// <summary>
        /// Получить список тегов к выбранной статье
        /// </summary>
        /// <param name="name">Название статьи</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTagByArtcleName{name}")]
        public async Task<IActionResult> GetTagByArtcleName(
            [FromRoute] string name)
        {
            var result = await _tagService.GetTagsByArticleName(name);

            var status = (ObjectResult)result;
            return StatusCode((int)(status.StatusCode), status.Value);
        }

    }
}
