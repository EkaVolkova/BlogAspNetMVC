namespace BlogAspNetMVC.Data.Queries
{
    public class UpdateRoleQuery
    {

        /// <summary>
        /// Новое название роли
        /// </summary>
        public string NewName { get; set; }

        public UpdateRoleQuery(string newName = null)
        {
            NewName = newName;
        }
    }
}
