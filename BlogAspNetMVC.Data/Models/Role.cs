using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.Data.Models
{
    [Table("Roles")]
    public class Role
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список всех пользователей с данной ролью
        /// </summary>
        public List<User> Users { get; set; }
    }

}
