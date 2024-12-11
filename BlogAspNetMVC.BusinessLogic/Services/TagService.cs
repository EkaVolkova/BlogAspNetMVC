using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Requests.TagRequest;
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
    public class TagService : ITagService
    {
        ITagRepository _tagRepository;
        IMapper _mapper;

        public TagService(
                                ITagRepository tagRepository,
                                IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавить тег
        /// </summary>
        /// <param name="addNewTagRequest">Моедль запроса на добавление тега</param>
        /// <returns></returns>
        public async Task<IActionResult> AddTag(AddNewTagRequest addNewTagRequest)
        {
            var tag = await _tagRepository.GetByName(addNewTagRequest.Name);
            if (!(tag is null))
                return new ObjectResult($"Тег с названием \"{addNewTagRequest.Name}\" уже существует") { StatusCode = 400 };



            tag = _mapper.Map<AddNewTagRequest, Tag>(addNewTagRequest);

            await _tagRepository.Create(tag, new List<Article>());

            return new ObjectResult($"Тег {tag.Name} создан") { StatusCode = 200 };
        }

        /// <summary>
        /// Изменить тег
        /// </summary>
        /// <param name="changeTagRequest">Модель запроса на изменение тега</param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeTag(ChangeTagRequest changeTagRequest)
        {
            var tag = await _tagRepository.GetById(changeTagRequest.Id);
            if (tag is null)
                return new ObjectResult($"Тег с Id \"{changeTagRequest.Id}\" не найден") { StatusCode = 400 };


            var query = _mapper.Map<ChangeTagRequest, UpdateTagQuery>(changeTagRequest);
            await _tagRepository.UpdateTag(tag, query);
            return new ObjectResult($"Тег с Id {changeTagRequest.Id} обновлен") { StatusCode = 200 };
        }

        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteTag(Guid guid)
        {
            var tag = await _tagRepository.GetById(guid);
            if (tag is null)
                return new ObjectResult($"Тег с Id \"{guid}\" не найден") { StatusCode = 400 };

            await _tagRepository.DeleteTag(tag);
            return new ObjectResult($"Тег с Id {guid} удален") { StatusCode = 200 };
        }

        /// <summary>
        /// Получить список всех тегов
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagRepository.GetAllTags();
            if (tags is null || tags.Count == 0)
                return new ObjectResult("Нет тегов") { StatusCode = 400 };

            return new ObjectResult(tags) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить тег по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        public async Task<IActionResult> GetTagById(Guid guid)
        {
            var tag = await _tagRepository.GetById(guid);
            if (tag is null)
                return new ObjectResult($"Тег с Id \"{guid}\" не найден") { StatusCode = 400 };

            return new ObjectResult(tag) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить список тегов по идентификатору статьи, к которой они оставлены
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        public async Task<IActionResult> GetTagsByArticleId(Guid guid)
        {
            var tags = await _tagRepository.GetAllTags();

            if (tags is null)
                return new ObjectResult($"Комментарии к статье с Id \"{guid}\" не найдены") { StatusCode = 400 };
            var filteredTags = tags.Where(tag => tag.Articles.Any(article => article.Id == guid)).ToList();

            if (filteredTags is null)
                return new ObjectResult($"Комментарии к статье с Id \"{guid}\" не найдены") { StatusCode = 400 };

            return new ObjectResult(filteredTags) { StatusCode = 200 };
        }

        /// <summary>
        /// Получить список тегов по названию статьи, к которой они оставлены
        /// </summary>
        /// <param name="articleName">Название статьи</param>
        /// <returns></returns>
        public async Task<IActionResult> GetTagsByArticleName(string articleName)
        {
            var tags = await _tagRepository.GetAllTags();

            if (tags is null)
                return new ObjectResult($"Комментарии к статье с названием \"{articleName}\" не найдены") { StatusCode = 400 };
            var filteredTags = tags.Where(tag => tag.Articles.Any(article => article.Name == articleName)).ToList();

            if (filteredTags is null)
                return new ObjectResult($"Комментарии к статье с названием \"{articleName}\" не найдены") { StatusCode = 400 };

            return new ObjectResult(filteredTags) { StatusCode = 200 };

        }

    }

}
