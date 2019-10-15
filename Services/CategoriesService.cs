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

        // NOTE No similar method needed
        // public List<Category> GetAllByQuestion(string id)
        // {
        //     // var categories = _repo.GetAllByQuestion(id).ToList();
        //     if (categories == null) { throw new Exception("No variety for you! There are no categories."); }

        //     return categories;
        // }

        public Category AddCategory(Category categoryData)
        {
            categoryData.Id = Guid.NewGuid().ToString();
            var savedCategory = _repo.Create(categoryData);

            return savedCategory;
        }

        // TODO Finish this
        public bool AddCategoryToQuestion(QuestionCategory qCategory)
        {

            qCategory.Id = Guid.NewGuid().ToString();
            var category = _repo.GetById(qCategory.CategoryId);
            var success = _repo.AddCategoryToQuestion(qCategory);

            return success;
        }

        public Category UpdateCategory(Category categoryData)
        {
            var category = _repo.GetById(categoryData.Id);
            var success = _repo.CheckQuestCatExists(category.Id);
            if (success) { throw new Exception("This category has been sealed with a question. You shall not edit!"); }
            category.Name = categoryData.Name;
            var updatedCategory = _repo.Edit(category);

            return updatedCategory;
        }

        public string DeleteCategory(string id)
        {
            var category = _repo.GetById(id);
            var success = _repo.CheckQuestCatExists(category.Id);
            if (success) { throw new Exception("This category has been sealed with a question. You shall not edit!"); }
            var deleted = _repo.Delete(category.Id);
            if (!deleted) { throw new Exception("This category too STRONK! Unable to delete the category."); }

            return id;
        }

        public CategoriesService(CategoriesRepository repo)
        {
            _repo = repo;
        }
    }
}