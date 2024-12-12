using System;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.UserExceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException(string message) : base(message)
        {

        }
    }
}
