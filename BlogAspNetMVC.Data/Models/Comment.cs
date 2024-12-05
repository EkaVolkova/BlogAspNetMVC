using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Models
{
    [Table("Comments")]
    public class Comment
    {
        /// <summary>
        /// Идентификатор комментария
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата создания/последнего изменения комментария
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Навигационное свойство: автор комментария
        /// </summary>
        public User Author { get; set; }

        /// <summary>
        /// Навигационное свойство: статья, к которой оставлен комментарий
        /// </summary>
        public Article Article { get; set; }

        /// <summary>
        /// Навигационное свойство: идентификатор статьи, к которой оставлен комментарий
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// Родительский комментарий в ветке
        /// </summary>
        public Comment ParentComment { get; set; }
    }
}
