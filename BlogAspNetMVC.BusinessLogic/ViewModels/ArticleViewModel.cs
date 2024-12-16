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
        public List<Guid> TagsId { get; set; } = new List<Guid>();

        /// <summary>
        /// Навигационное свойство: список id комментариев к статье
        /// </summary>
        public List<Guid> CommentsId { get; set; }
        /// <summary>
        /// Навигационное свойство для идентификатора автора статьи
        /// </summary>
        public Guid AuthorId { get; set; }

    }
}
