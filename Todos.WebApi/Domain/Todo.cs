using System;

namespace Todos.WebApi.Domain
{
    public class Todo
    {
        public Guid TodoId { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}