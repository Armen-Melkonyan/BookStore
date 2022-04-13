using System.Collections.Generic;

namespace MyTask.BookStore.Models
{
    public class UserEmailOptions
    {
        public List<string> ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
