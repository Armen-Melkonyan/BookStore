using System.Collections.Generic;

namespace MyTask.BookStore.Models
{
    public class ViewModel
    {
        public List<BookModel> AllBooks { get; set; }
        public BookModel Book { get; set; }
        public List<BookModel> CurrentPageData { get; set; }
    }
}