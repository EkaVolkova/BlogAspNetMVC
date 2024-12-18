using System;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.UserExceptions
{
    public class UserAlreadyExistException : ArgumentException
    {
        public UserAlreadyExistException(string message) : base(message)
        {

        }
    }
}
