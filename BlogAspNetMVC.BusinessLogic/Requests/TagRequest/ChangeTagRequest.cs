using System;

namespace BlogAspNetMVC.BusinessLogic.Requests.TagRequest
{
    public class ChangeTagRequest
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
