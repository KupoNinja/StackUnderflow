using System.Collections.Generic;
using System.Data;
using Dapper;
using StackUnderflow.Models;

namespace StackUnderflow.Data
{
    public class QuestionsRepository
    {
        private readonly IDbConnection _db;

        public IEnumerable<Question> GetAll()
        {
            return _db.Query<Question>("SELECT * FROM questions");
        }

        public Question GetById(string id)
        {
            var sql = @"SELECT * FROM questions WHERE id = @id";

            return _db.QueryFirstOrDefault<Question>(sql, new { id });
        }

        public Question Create(Question question)
        {
            var sql = @"INSERT INTO questions 
            (id, title, body, dateasked, dateedited, dateanswered, authorid)
            VALUES
            (@Id, @Title, @Body, @DateAsked, @DateEdited, @DateAnswered, @AuthorId)";
            _db.Execute(sql, question);

            return question;
        }

        public QuestionsRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}