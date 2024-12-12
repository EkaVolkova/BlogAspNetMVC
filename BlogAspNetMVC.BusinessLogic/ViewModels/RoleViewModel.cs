using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAspNetMVC.BusinessLogic.ViewModels
{
    public class RoleViewModel
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
        /// Список всех Id пользователей с данной ролью
        /// </summary>
        public List<Guid> UsersId { get; set; }
    }
}
