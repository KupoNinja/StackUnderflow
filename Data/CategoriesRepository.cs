using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using StackUnderflow.Models;

namespace StackUnderflow.Data
{
    public class CategoriesRepository
    {
        private readonly IDbConnection _db;

        public IEnumerable<Category> GetAll()
        {
            return _db.Query<Category>("SELECT * FROM categories;");
        }

        public Category GetById(string id)
        {
            var sql = @"SELECT * FROM categories WHERE id = @id;";

            return _db.QueryFirstOrDefault<Category>(sql, new { id });
        }

        public QuestionCategory GetQuestCatByCatId(string id)
        {
            var sql = @"SELECT * FROM questioncategories WHERE categoryid = @id;";

            return _db.QueryFirstOrDefault<QuestionCategory>(sql, new { id });
        }

        public Category Create(Category category)
        {
            var sql = @"INSERT INTO categories 
            (id, name) VALUES (@Id, @Name);";
            _db.Execute(sql, category);

            return category;
        }

        public Category Edit(Category category)
        {
            var sql = @"
            UPDATE categories SET 
            name = @Name
            WHERE id = @Id;";
            _db.Execute(sql, category);

            return category;
        }

        public bool AddCategoryToQuestion(QuestionCategory qCategory)
        {
            var sql = @"
            INSERT INTO questioncategories
            (id, questionid, categoryid) VALUES
            (@Id, @QuestionId, @CategoryId)
            ;";
            var success = _db.Execute(sql, qCategory);
            if (success == 1) { return true; }

            return false;
        }

        public bool Delete(string id)
        {
            var success = _db.Execute(@"DELETE FROM categories WHERE id = @Id", new { id });
            if (success == 1) { return true; }

            return false;
        }

        public bool DeleteQuestCat(string id)
        {
            var success = _db.Execute(@"DELETE FROM questioncategories WHERE categoryid = @Id", new { id });
            if (success == 1) { return true; }

            return false;
        }

        public CategoriesRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}