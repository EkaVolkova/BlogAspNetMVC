using System;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.TagExceptions
{
    public class TagAlreadyExistException : Exception
    {
        public TagAlreadyExistException(string message) : base(message)
        {

        }
    }
}
