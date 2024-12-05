using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Models
{
    [Table("Articles")]
    public class Article
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
        /// Навигационное свойство: список тегов к статье
        /// </summary>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Навигационное свойство: список комментариев к статье
        /// </summary>
        public List<Comment> Comments { get; set; }
        /// <summary>
        /// Навигационное свойство для идентификатора автора статьи
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Навигационное свойство для автора статьи
        /// </summary>
        public User Author { get; set; }

    }
}
