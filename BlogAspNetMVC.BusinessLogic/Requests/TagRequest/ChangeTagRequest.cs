using System;

namespace BlogAspNetMVC.BusinessLogic.Requests.TagRequest
{
    public class ChangeTagRequest
    {
        public Guid Id { get; set; }
        public string NewText { get; set; }
    }
}
