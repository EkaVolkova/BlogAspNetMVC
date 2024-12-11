using BlogAspNetMVC.BusinessLogic.Requests.TagRequest;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Services
{
    public interface ITagService
    {
        /// <summary>
        /// Добавить тег
        /// </summary>
        /// <param name="addNewTagRequest">Моедль запроса на добавление тега</param>
        /// <returns></returns>
        Task<IActionResult> AddTag(AddNewTagRequest addNewTagRequest);

        /// <summary>
        /// Изменить тег
        /// </summary>
        /// <param name="changeTagRequest">Модель запроса на изменение тега</param>
        /// <returns></returns>
        Task<IActionResult> ChangeTag(ChangeTagRequest changeTagRequest);
        
        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        Task<IActionResult> DeleteTag(Guid guid);

        /// <summary>
        /// Получить список всех тегов
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> GetAllTags();

        /// <summary>
        /// Получить тег по идентификатору
        /// </summary>
        /// <param name="guid">Идентификатор тега</param>
        /// <returns></returns>
        Task<IActionResult> GetTagById(Guid guid);

        /// <summary>
        /// Получить список тегов по названию статьи, к которой они оставлены
        /// </summary>
        /// <param name="articleName">Название статьи</param>
        /// <returns></returns>
        Task<IActionResult> GetTagsByArticleName(string articleName);

        /// <summary>
        /// Получить список тегов по идентификатору статьи, к которой они оставлены
        /// </summary>
        /// <param name="guid">Идентификатор статьи</param>
        /// <returns></returns>
        Task<IActionResult> GetTagsByArticleId(Guid guid);

    }
}
