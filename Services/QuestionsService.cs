using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
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

        public Question GetById(string id)
        {
            var question = _qs.GetById(id);

            return question;
        }

        public Question AddQuestion(Question questionData)
        {
            questionData.Id = Guid.NewGuid().ToString();
            questionData.DateAsked = DateTime.Now;
            var postedQuestion = _qs.Create(questionData);

            return postedQuestion;
        }

        public Question UpdateQuestion(Question questionData)
        {
            var question = _qs.GetById(questionData.Id);
            question.DateEdited = DateTime.Now;
            var updatedQuestion = _qs.Edit(question);

            return updatedQuestion;
        }

        public string DeleteQuestion(string id)
        {
            var question = _qs.GetById(id);
            var deleted = _qs.Delete(question.Id);
            if (!deleted) { throw new Exception("This question too STRONK! Unable to delete the question."); }

            return id;
        }

        public QuestionsService(QuestionsRepository qs)
        {
            _qs = qs;
        }
    }
}