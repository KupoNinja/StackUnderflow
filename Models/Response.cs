using System;
using StackUnderflow.Interfaces;

namespace StackUnderflow.Models
{
    public class Response : IResponse
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public DateTime Replied { get; set; }
        public DateTime Edited { get; set; }
        public string QuestionId { get; set; }
        public string AuthorId { get; set; }
    }
}