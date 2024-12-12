using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.ArticleExceptions
{
    /// <summary>
    /// Статья уже существует
    /// </summary>
    public class ArticleAlreadyExistException : Exception
    {
        public ArticleAlreadyExistException(string message) : base(message)
        {

        }
    }
}
