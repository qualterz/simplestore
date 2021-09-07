using SimpleStore.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStore.Application.Services
{
    public interface ICategoryService
    {
        List<CategoryModel> GetCategoryList();
        void AddCategory(CategoryModel model);
        void UpdateCategoryParent(CategoryModel model);
    }

    public class CategoryService
    {
    }
}
