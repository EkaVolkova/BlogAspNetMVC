using System;

namespace BlogAspNetMVC.BusinessLogic.Exceptions.ArticleExceptions
{
    /// <summary>
    /// Статья не найдена
    /// </summary>
    public class ArticleNotFoundException : ArgumentException
    {
        public ArticleNotFoundException(string message) : base(message)
        {

        }
    }
}
