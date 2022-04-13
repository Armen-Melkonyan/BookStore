using System.Collections;
using System.Collections.Generic;

namespace MyTask.BookStore.Data
{
    public class Language
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Generic type to pass table name. table name is Books. 
        //Using to make realitionship between Language and Book tables 
        public ICollection<Books> Books { get; set; }
    }
}
