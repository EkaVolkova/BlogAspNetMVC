using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.ViewModels
{
    public class CommentViewModel
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
        /// Навигационное свойство: id авторa комментария
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Навигационное свойство: идентификатор статьи, к которой оставлен комментарий
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// Id родительского комментария в ветке
        /// </summary>
        public Guid ParentCommentId { get; set; }
    }
}
