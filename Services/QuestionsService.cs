using System;
using System.Collections.Generic;
using System.Linq;
using StackUnderflow.Data;
using StackUnderflow.Models;

namespace StackUnderflow.Services
{
    public class QuestionsService
    {
        private readonly QuestionsRepository _qs;

        public List<Question> GetAll()
        {
            var questions = _qs.GetAll().ToList();
            if (questions == null) { throw new Exception("We've learned everything we need to know. There are no questions."); }

            return questions;
        }

        public QuestionsService(QuestionsRepository qs)
        {
            _qs = qs;
        }
    }
}