using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using StackUnderflow.Data;
using StackUnderflow.Models;

namespace StackUnderflow.Services
{
    public class CategoriesService
    {
        private readonly CategoriesRepository _repo;

        public List<Category> GetAll()
        {
            var categories = _repo.GetAll().ToList();
            if (categories == null) { throw new Exception("No variety for you! There are no categories."); }

            return categories;
        }

        public Category GetById(string id)
        {
            var category = _repo.GetById(id);

            return category;
        }

        public Category AddCategory(Category categoryData)
        {
            categoryData.Id = Guid.NewGuid().ToString();
            var savedCategory = _repo.Create(categoryData);

            return savedCategory;
        }

        // TODO Check for duplicate?
        public bool AddCategoryToQuestion(QuestionCategory qCategory)
        {
            qCategory.Id = Guid.NewGuid().ToString();
            var category = _repo.GetById(qCategory.CategoryId);
            var success = _repo.AddCategoryToQuestion(qCategory);

            return success;
        }

        public Category UpdateCategory(Category categoryData)
        {
            var questCat = _repo.GetQuestCatByCatId(categoryData.Id);
            if (questCat != null) { throw new Exception("This has a majestic Quest Cat! You shall not edit!"); }
            var category = _repo.GetById(categoryData.Id);
            category.Name = categoryData.Name;
            var updatedCategory = _repo.Edit(category);

            return updatedCategory;
        }

        public string DeleteCategory(string id)
        {
            var questCat = _repo.GetQuestCatByCatId(id);
            if (questCat != null) { throw new Exception("This has a majestic Quest Cat! You shall not delete!"); }
            var deleted = _repo.DeleteQuestCat(id);
            if (deleted)
            {
                var success = _repo.Delete(id);
                if (!success) { throw new Exception("This category too STRONK! Unable to delete the category."); }
            }

            return id;
        }

        public CategoriesService(CategoriesRepository repo)
        {
            _repo = repo;
        }
    }
}