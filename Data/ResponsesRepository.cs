using System.Collections.Generic;
using System.Data;
using Dapper;
using StackUnderflow.Models;

namespace StackUnderflow.Data
{
    public class ResponsesRepository
    {
        private readonly IDbConnection _db;

        public IEnumerable<Response> GetAll()
        {
            return _db.Query<Response>("SELECT * FROM responses;");
        }

        public Response GetById(string id)
        {
            var sql = @"SELECT * FROM responses WHERE id = @id;";

            return _db.QueryFirstOrDefault<Response>(sql, new { id });
        }

        public IEnumerable<Response> GetAllByQuestion(string id)
        {
            return _db.Query<Response>(
                "SELECT * FROM responses WHERE questionid = @id;",
                new { id }
            );
        }

        public Response Create(Response response)
        {
            var sql = @"INSERT INTO responses 
            (id, body, datereplied, dateedited, questionid, authorid)
            VALUES
            (@Id, @Body, @DateReplied, @DateEdited, @QuestionId, @AuthorId);";
            _db.Execute(sql, response);

            return response;
        }

        public Response Edit(Response response)
        {
            var sql = @"
            UPDATE responses SET 
            body = @Body,
            datereplied = @DateReplied,
            dateedited = @DateEdited,
            questionid = @QuestionId,
            authorid = @AuthorId
            WHERE id = @Id;";
            _db.Execute(sql, response);

            return response;
        }

        public bool Delete(string id)
        {
            var success = _db.Execute(@"DELETE FROM responses WHERE id = @Id", new { id });
            if (success == 1) { return true; }

            return false;
        }

        public ResponsesRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}