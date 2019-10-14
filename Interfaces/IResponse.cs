using System;

namespace StackUnderflow.Interfaces
{
    public interface IResponse
    {
        string Id { get; set; }
        string Body { get; set; }
        DateTime DateReplied { get; set; }
        DateTime DateEdited { get; set; }
        string QuestionId { get; set; }
        string AuthorId { get; set; }
    }
}