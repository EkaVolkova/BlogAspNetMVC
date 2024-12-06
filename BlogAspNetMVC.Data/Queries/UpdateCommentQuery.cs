using BlogAspNetMVC.Data.Models;
using System.Collections.Generic;

namespace BlogAspNetMVC.Data.Queries
{
    public class UpdateCommentQuery
    {

        /// <summary>
        /// Новый текст комментария
        /// </summary>
        public string NewText { get; set; }

        public UpdateCommentQuery (string newText = null)
        {
            NewText = newText;
        }
    }

}
