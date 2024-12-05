using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Models
{
    [Table("Tags")]
    public class Tag
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
        /// Навигационное свойство: список статей по данному тегу
        /// </summary>
        public List<Article> Articles { get; set; }
    }
}
