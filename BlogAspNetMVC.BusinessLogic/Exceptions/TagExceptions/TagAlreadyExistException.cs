using System;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.TagExceptions
{
    public class TagAlreadyExistException : ArgumentException
    {
        public TagAlreadyExistException(string message) : base(message)
        {

        }
    }
}
