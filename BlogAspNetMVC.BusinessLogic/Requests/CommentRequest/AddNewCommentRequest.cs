using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Requests.CommentRequest
{
    public class AddNewCommentRequest
    {
        public Guid Guid { get; set; }
        public string Text { get; set; }
        public Guid ParentCommentId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid ArticleId { get; set; }
    }
}
