using System;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.ArticleExceptions
{
    /// <summary>
    /// Статья не найдена
    /// </summary>
    public class ArticleNotFoundException : Exception
    {
        public ArticleNotFoundException(string message) : base(message)
        {

        }
    }
}
