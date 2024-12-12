using System;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.RoleExceptions
{
    public class RoleAlreadyExistException : Exception
    {
        public RoleAlreadyExistException(string message) : base(message)
        {

        }
    }
}
