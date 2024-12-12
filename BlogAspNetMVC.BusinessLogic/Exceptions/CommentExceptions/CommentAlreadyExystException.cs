using System;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.CommentExceptions
{
    public class CommentAlreadyExystException : Exception
    {
        public CommentAlreadyExystException(string message) : base(message)
        {

        }
    }
}
