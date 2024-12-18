﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.UserExceptions
{
    public class UserPasswordIsWrong : ArgumentException
    {
        public UserPasswordIsWrong(string message) : base(message)
        {

        }
    }
}
