using System.Collections.Generic;

namespace BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests
{
    public class ChangeArticleRequest
    {
        public string OldName { get; set; }
        public string NewName { get; set; }

        public string NewText { get; set; }
        public List<string> NewTags { get; set; }

    }
}
