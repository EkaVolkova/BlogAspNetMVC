﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.RoleExceptions
{
    public class RoleNotFoundException : ArgumentException
    {
        public RoleNotFoundException(string message) : base(message)
        {

        }
    }
}
