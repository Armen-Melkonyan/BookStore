using System;
using System.Collections;
using System.Collections.Generic;

namespace MyTask.BookStore.Data
{
    public class Books
    {
        //These are Columns of Databace 
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string CoverImageUrl { get; set; }
        public string BookPdfURL { get; set; }
        public int LanguageID { get; set; }
        public int Pages { get; set; }
        public string ReleaseDate { get; set; }
        public string Category { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateOn { get; set; }

        //Foreign key for realitionship. Using in database table to make realitionship between to tables
        public Language language { get; set; }

        //Add foreign key
        public ICollection<BookGallery> BookGallery { get; set; }
    }
}
