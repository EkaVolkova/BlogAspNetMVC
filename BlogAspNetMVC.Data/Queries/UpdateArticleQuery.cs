using BlogAspNetMVC.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Queries
{
    public class UpdateArticleQuery
    {
        /// <summary>
        /// Новое название статьи
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        /// Новый текст статьи
        /// </summary>
        public string NewText { get; set; }

        /// <summary>
        /// Новый список тегов
        /// </summary>
        public List<Tag> NewTags { get; set; }

        public UpdateArticleQuery(string newName = null, string newText = null, List<Tag> newTags = null)
        {
            NewName = newName;
            NewText = newText;
            NewTags = newTags;
        }
    }
}
