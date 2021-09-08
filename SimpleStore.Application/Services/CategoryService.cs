using AutoMapper;
using SimpleStore.Application.Mapper;
using SimpleStore.Application.Models;
using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;
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
        CategoryModel GetCategoryByName(string categoryName);
        CategoryModel AddCategory(CategoryModel categoryModel);
        void UpdateCategoryParent(int categoryId, int parentCategoryId);
        void AssignCategory(int categoryId, int itemId);
        CategoryModel AssignCategory(CategoryModel categoryModel, int itemId);
        void UnassignCategory(int categoryId, int itemId);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IItemRepository itemRepository;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IItemRepository itemRepository)
        {
            this.categoryRepository = categoryRepository;
            this.itemRepository = itemRepository;
        }

        private readonly IMapper mapper = ObjectMapper.Mapper;

        public CategoryModel AddCategory(CategoryModel categoryModel)
        {
            var category = mapper.Map<Category>(categoryModel);
            category.ParentCategory = new Category() { Name = "Root" };
            category = categoryRepository.Add(category);
            return mapper.Map<CategoryModel>(category);
        }

        public void AssignCategory(int categoryId, int itemId)
        {
            var item = itemRepository.GetById(itemId);
            item.CategoryId = categoryId;
            itemRepository.Update(item);
        }

        public CategoryModel AssignCategory(CategoryModel categoryModel, int itemId)
        {
            var category = categoryRepository.Entities
                .SingleOrDefault(e => e.CategoryId == categoryModel.CatagoryId);

            if (category is null)
            {
                category = mapper.Map<Category>(categoryModel);
                category = categoryRepository.Add(category);
            }

            AssignCategory(category.CategoryId, itemId);

            return mapper.Map<CategoryModel>(category);
        }

        public List<CategoryModel> GetCategoryList()
        {
            var categories = categoryRepository.Entities.ToList();
            return mapper.Map<List<CategoryModel>>(categories);
        }

        public void UnassignCategory(int categoryId, int itemId)
        {
            var item = itemRepository.GetById(itemId);
            item.CategoryId = null;
            itemRepository.Update(item);
        }

        public void UpdateCategoryParent(int categoryId, int parentCategoryId)
        {
            var category = categoryRepository.Entities
                .Single(e => e.CategoryId == categoryId);

            category.ParentCategoryId = parentCategoryId;

            categoryRepository.Update(category);
        }

        public CategoryModel GetCategoryByName(string categoryName)
        {
            var category = categoryRepository
                .Entities.FirstOrDefault(e => e.Name == categoryName);

            if (category == default)
                return null;

            return mapper.Map<CategoryModel>(category);
        }
    }
}
