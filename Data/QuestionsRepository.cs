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
            return _db.Query<Question>("SELECT * FROM questions;");
        }

        public Question GetById(string id)
        {
            var sql = @"SELECT * FROM questions WHERE id = @id;";

            return _db.QueryFirstOrDefault<Question>(sql, new { id });
        }

        public Question Create(Question question)
        {
            var sql = @"INSERT INTO questions 
            (id, title, body, dateasked, dateedited, dateanswered, authorid)
            VALUES
            (@Id, @Title, @Body, @DateAsked, @DateEdited, @DateAnswered, @AuthorId);";
            _db.Execute(sql, question);

            return question;
        }

        public Question Edit(Question question)
        {
            var sql = @"
            UPDATE questions SET 
            title = @Title,
            body = @Body,
            dateasked = @DateAsked,
            dateedited = @DateEdited,
            dateanswered = @DateAnswered,
            authorid = @AuthorId,
            answerid = @AnswerId
            WHERE id = @ID;";
            _db.Execute(sql, question);

            return question;
        }

        public bool Delete(string id)
        {
            var success = _db.Execute(@"DELETE FROM questions WHERE id = @Id", new { id });
            if (success == 1) { return true; }

            return false;
        }

        public QuestionsRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}