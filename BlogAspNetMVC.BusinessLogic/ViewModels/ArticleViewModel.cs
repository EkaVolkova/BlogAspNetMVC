using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.ViewModels
{
    public class ArticleViewModel
    {
        /// <summary>
        /// Идентификатор для статьи
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название статьи
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Текст статьи
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата создания/последнего изменения статьи
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Навигационное свойство: список id тегов к статье
        /// </summary>
        public List<TagViewModel> Tags { get; set; }

        /// <summary>
        /// Навигационное свойство: список комментариев к статье
        /// </summary>
        public List<CommentViewModel> Comments { get; set; }

        /// <summary>
        /// Навигационное свойство для идентификатора автора статьи
        /// </summary>
        public UserViewModel Author { get; set; }

    }
}
