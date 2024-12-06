namespace BlogAspNetMVC.Data.Queries
{
    public class UpdateTagQuery
    {

        /// <summary>
        /// Новое название тега
        /// </summary>
        public string NewName { get; set; }

        public UpdateTagQuery(string newName = null)
        {
            NewName = newName;
        }
    }
}
