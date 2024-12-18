using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.TagExceptions
{
    public class TagNotFoundException : ArgumentException
    {
        public TagNotFoundException(string message) : base(message)
        {

        }
    }
}
