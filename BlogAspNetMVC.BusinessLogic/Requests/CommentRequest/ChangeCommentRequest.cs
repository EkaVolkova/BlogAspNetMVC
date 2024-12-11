using System;

namespace BlogAspNetMVC.BusinessLogic.Requests.CommentRequest
{
    public class ChangeCommentRequest
    {
        public Guid Id { get; set; }
        public string NewText { get; set; }
    }
}
