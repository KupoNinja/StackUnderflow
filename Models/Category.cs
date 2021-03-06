using StackUnderflow.Interfaces;

namespace StackUnderflow.Models
{
    public class Category : ICategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class QuestionCategory
    {
        public string Id { get; set; }
        public string QuestionId { get; set; }
        public string CategoryId { get; set; }
        public string Action { get; set; }
    }
}