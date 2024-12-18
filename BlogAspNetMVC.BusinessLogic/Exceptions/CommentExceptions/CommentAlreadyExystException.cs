using System;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.CommentExceptions
{
    public class CommentAlreadyExystException : ArgumentException
    {
        public CommentAlreadyExystException(string message) : base(message)
        {

        }
    }
}
