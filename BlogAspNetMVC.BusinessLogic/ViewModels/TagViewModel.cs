using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.ViewModels
{
    public class TagViewModel
    {
        /// <summary>
        /// Идентификатор тега
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название тега
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Навигационное свойство: список id статей по данному тегу
        /// </summary>
        public List<Guid> ArticlesId { get; set; }
    }
}
