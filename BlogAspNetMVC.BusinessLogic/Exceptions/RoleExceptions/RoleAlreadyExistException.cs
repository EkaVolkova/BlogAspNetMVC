using System;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.RoleExceptions
{
    public class RoleAlreadyExistException : ArgumentException
    {
        public RoleAlreadyExistException(string message) : base(message)
        {

        }
    }
}
