using System;
using StackUnderflow.Interfaces;

namespace StackUnderflow.Models
{
    public class Response : IResponse
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public DateTime DateReplied { get; set; }
        public DateTime DateEdited { get; set; }
        public string QuestionId { get; set; }
        public string AuthorId { get; set; }
    }
}