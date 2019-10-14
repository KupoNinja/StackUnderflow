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

        public QuestionsRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}