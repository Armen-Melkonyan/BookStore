using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTask.BookStore.Models;

namespace MyTask.BookStore.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUserModel>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) 
            : base(options)
        {

        }

        //Add a property from DB table named "Books" this table are avalable in the Books class. Table name is Books
        public DbSet<Books> Books { get; set; }

        //We can add more columns hear from diferent tables
        //public DbSet<Table2> Table2 { get; set; }
        //public DbSet<Table3> Table3 { get; set; }

        //Set Language table to database
        public DbSet<Language> Language { get; set; }

        //Set BookGallery table to database
        public DbSet<BookGallery> BookGallery { get; set; }
    }
}
