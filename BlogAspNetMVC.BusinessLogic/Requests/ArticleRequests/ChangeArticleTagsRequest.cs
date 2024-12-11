using System.Collections.Generic;

namespace BlogAspNetMVC.BusinessLogic.Requests.ArticleRequests
{
    public class ChangeArticleTagsRequest
    {
        public string Name { get; set; }

        public List<string> Tags { get; set; }

    }


}
