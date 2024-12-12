using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.CommentExceptions
{
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException(string message) : base(message)
        {

        }
    }
}
