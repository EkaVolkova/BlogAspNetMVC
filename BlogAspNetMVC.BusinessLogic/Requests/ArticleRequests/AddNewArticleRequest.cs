using BlogAspNetMVC.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests
{
    public class AddNewArticleRequest
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public Guid AuthorId { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

        public string TagsJson { get; set; }

        // Метод для десериализации тегов
        public void DeserializeTags(string tagsJson)
        {
            Tags = JsonSerializer.Deserialize<List<string>>(tagsJson);
        }
    }
}
