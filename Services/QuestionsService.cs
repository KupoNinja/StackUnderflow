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
        private readonly QuestionsRepository _repo;

        public List<Question> GetAll()
        {
            var questions = _repo.GetAll().ToList();
            if (questions == null) { throw new Exception("We've learned everything we need to know. There are no questions."); }

            return questions;
        }

        public Question GetById(string id)
        {
            var question = _repo.GetById(id);

            return question;
        }

        public Question AddQuestion(Question questionData)
        {
            questionData.Id = Guid.NewGuid().ToString();
            questionData.DateAsked = DateTime.Now;
            var postedQuestion = _repo.Create(questionData);

            return postedQuestion;
        }

        public Question UpdateQuestion(Question questionData)
        {
            var question = _repo.GetById(questionData.Id);
            question.Title = questionData.Title;
            question.Body = questionData.Body;
            question.DateAnswered = questionData.DateAnswered;
            question.DateEdited = DateTime.Now;
            var updatedQuestion = _repo.Edit(question);

            return updatedQuestion;
        }

        public string DeleteQuestion(string id)
        {
            var question = _repo.GetById(id);
            var deleted = _repo.Delete(question.Id);
            if (!deleted) { throw new Exception("This question too STRONK! Unable to delete the question."); }

            return id;
        }

        public QuestionsService(QuestionsRepository repo)
        {
            _repo = repo;
        }
    }
}