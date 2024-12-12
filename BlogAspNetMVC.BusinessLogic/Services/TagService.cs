using AutoMapper;
using BlogAspNetMVC.BusinessLogic.Exceptions.TagExceptions;
using BlogAspNetMVC.BusinessLogic.Requests.TagRequest;
using BlogAspNetMVC.BusinessLogic.ViewModels;
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
        public async Task<TagViewModel> AddTag(AddNewTagRequest addNewTagRequest)
        {
            var tag = await _tagRepository.GetByName(addNewTagRequest.Name);
            //если тег уже был добавлен
            if (!(tag is null))
                throw new TagAlreadyExistException($"Тег с названием \"{addNewTagRequest.Name}\" уже существует");


            tag = _mapper.Map<AddNewTagRequest, Tag>(addNewTagRequest);

            await _tagRepository.Create(tag, new List<Article>());

            var tagView = _mapper.Map<Tag, TagViewModel>(tag);

            return tagView;
        }

        /// <summary>
        /// Изменить тег
        /// </summary>
        /// <param name="changeTagRequest">Модель запроса на изменение тега</param>
        /// <returns></returns>
        public async Task<TagViewModel> ChangeTag(ChangeTagRequest changeTagRequest)
        {
            var tag = await _tagRepository.GetByName(changeTagRequest.OldName);
            //если тег не найден
            if (tag is null)
                throw new TagNotFoundException($"Тег с названием \"{changeTagRequest.OldName}\" не найден");


            var query = _mapper.Map<ChangeTagRequest, UpdateTagQuery>(changeTagRequest);

            await _tagRepository.UpdateTag(tag, query);

            tag = await _tagRepository.GetByName(changeTagRequest.NewName);

            var tagView = _mapper.Map<Tag, TagViewModel>(tag);

            return tagView;
        }

        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        public async Task DeleteTag(Guid guid)
        {
            var tag = await _tagRepository.GetById(guid);
            //если тег не найден
            if (tag is null)
                throw new TagNotFoundException($"Тег с Id \"{guid}\" не найден");

            await _tagRepository.DeleteTag(tag);
        }

        /// <summary>
        /// Получить список всех тегов
        /// </summary>
        /// <returns></returns>
        public async Task<List<TagViewModel>> GetAllTags()
        {
            var tags = await _tagRepository.GetAllTags();
            if (tags is null || tags.Count == 0)
                return new List<TagViewModel>();

            var tagsView = _mapper.Map<List<Tag>, List<TagViewModel>>(tags);

            return tagsView;
        }

        /// <summary>
        /// Получить тег по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        public async Task<TagViewModel> GetTagById(Guid guid)
        {
            var tag = await _tagRepository.GetById(guid);
            //если тег не найден
            if (tag is null)
                throw new TagNotFoundException($"Тег с Id \"{guid}\" не найден");

            var tagView = _mapper.Map<Tag, TagViewModel>(tag);

            return tagView;
        }

        /// <summary>
        /// Получить список тегов по идентификатору статьи, к которой они оставлены
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        public async Task<List<TagViewModel>> GetTagsByArticleId(Guid guid)
        {
            var tags = await _tagRepository.GetAllTags();

            if (tags is null)
                return new List<TagViewModel>();
            var filteredTags = tags.Where(tag => tag.Articles.Any(article => article.Id == guid)).ToList();

            if (filteredTags is null)
                return new List<TagViewModel>();

            var tagsView = _mapper.Map<List<Tag>, List<TagViewModel>>(filteredTags);

            return tagsView;
        }

        /// <summary>
        /// Получить список тегов по названию статьи, к которой они оставлены
        /// </summary>
        /// <param name="articleName">Название статьи</param>
        /// <returns></returns>
        public async Task<List<TagViewModel>> GetTagsByArticleName(string articleName)
        {
            var tags = await _tagRepository.GetAllTags();

            if (tags is null)
                return new List<TagViewModel>();
            var filteredTags = tags.Where(tag => tag.Articles.Any(article => article.Name == articleName)).ToList();

            if (filteredTags is null)
                return new List<TagViewModel>();

            var tagsView = _mapper.Map<List<Tag>, List<TagViewModel>>(filteredTags);

            return tagsView;

        }

    }

}
