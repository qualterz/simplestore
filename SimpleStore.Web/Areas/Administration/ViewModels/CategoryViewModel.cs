namespace SimpleStore.Web.Areas.Administration.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public int ParentCategoryId { get; set; }
        public string Name { get; set; }
    }
}
