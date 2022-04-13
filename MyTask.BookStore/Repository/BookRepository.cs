using Microsoft.EntityFrameworkCore;
using MyTask.BookStore.Data;
using MyTask.BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTask.BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        //Creat a new instance of BookStorContext 
        private readonly BookStoreContext bookStoreContext = null;
        //Using constracture to creat instance 
        public BookRepository(BookStoreContext context)
        {
            bookStoreContext = context;
        }

        //Creat a method to add new book to DB. Using class Books from Data folder there are table columns ther
        public async Task<int> AddNewBook(BookModel model)
        {
            //Creat a new instance of class Books in data folder to add details from class model
            var newBook = new Books()
            {
                ID = model.ID,
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                Image = model.Image,
                LanguageID = model.LanguageID,
                Pages = model.Pages,
                CoverImageUrl = model.CoverImageUrl,
                ReleaseDate = model.ReleaseDate,
                Category = model.Category,
                BookPdfURL = model.BookPdfURL,
                CreatedOn = DateTime.UtcNow,
                UpdateOn = DateTime.UtcNow
            };

            newBook.BookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.BookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            //Set the newBook to context books in the class BookStoreContext and save changes and return ID
            //"Use AddAsyinc to add data to DB"
            await bookStoreContext.Books.AddAsync(newBook);
            await bookStoreContext.SaveChangesAsync();

            //without async
            // bookStoreContext.Books.Add(newBook);
            // bookStoreContext.SaveChanges();

            return newBook.ID;
        }


        public async Task<List<BookModel>> GetAllBooks()
        {
            return await bookStoreContext.Books.Select(x => new BookModel()
            {
                ID = x.ID,
                Title = x.Title,
                Author = x.Author,
                Description = x.Description,
                Image = x.Image,
                LanguageID = x.LanguageID,
                Language = x.language.Name,//Foriegn key
                Pages = x.Pages,
                CoverImageUrl = x.CoverImageUrl,
                ReleaseDate = x.ReleaseDate,
                Category = x.Category,
                CreatedOn = DateTime.UtcNow,
                UpdateOn = DateTime.UtcNow
            }).ToListAsync();
        }

        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await bookStoreContext.Books.Select(x => new BookModel()
            {
                ID = x.ID,
                Title = x.Title,
                Author = x.Author,
                Description = x.Description,
                Image = x.Image,
                LanguageID = x.LanguageID,
                Language = x.language.Name,//Foriegn key
                Pages = x.Pages,
                CoverImageUrl = x.CoverImageUrl,
                ReleaseDate = x.ReleaseDate,
                Category = x.Category,
                CreatedOn = DateTime.UtcNow,
                UpdateOn = DateTime.UtcNow
            }).Take(count).ToListAsync();
        }

        public async Task<BookModel> GetBook(int id)
        {
            //Get book by id from DB 
            //"Use FindAsinc to get one book from DB"
            return await bookStoreContext.Books.Where(x => x.ID == id)
                .Select(book => new BookModel()
                {
                    ID = book.ID,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Image = book.Image,
                    LanguageID = book.LanguageID,
                    Language = book.language.Name,//Foriegn key
                    Pages = book.Pages,
                    CoverImageUrl = book.CoverImageUrl,
                    ReleaseDate = book.ReleaseDate,
                    Category = book.Category,
                    CreatedOn = DateTime.UtcNow,
                    UpdateOn = DateTime.UtcNow,
                    Gallery = book.BookGallery.Select(x => new GalleryModel()
                    {
                        ID = x.ID,
                        Name = x.Name,
                        URL = x.URL
                    }).ToList(),
                    BookPdfURL = book.BookPdfURL
                }).FirstOrDefaultAsync();

            //if book isn't equal to null
            //if (book != null)
            //{
            //    var bookDetails = new BookModel()
            //    {
            //        ID=book.ID, 
            //        Title = book.Title,
            //        Author = book.Author,
            //        Description = book.Description,
            //        Image = book.Image,
            //        LanguageID = book.LanguageID,
            //        Language = book.language.Name,//Foriegn key
            //        Pages = book.Pages,
            //        ReleaseDate = book.ReleaseDate,
            //        Category = book.Category,
            //        CreatedOn = DateTime.UtcNow,
            //        UpdateOn = DateTime.UtcNow
            //    };
            //    return bookDetails;
            //}
            //return null;
        }

        public List<BookModel> SearchBooks(string name)
        {
            //return DataSource().Where(x => (x.Title).ToLower().Contains(name)).ToList();
            return null;
        }

        public void Delete(int id)
        {
            var data = bookStoreContext.Books.FirstOrDefault(x => x.ID == id);
            bookStoreContext.Books.Remove(data);
            bookStoreContext.SaveChanges();
        }

        public int Update(BookModel model, int id)
        {
            var data = bookStoreContext.Books.FirstOrDefault(x => x.ID == id);
            if (data != null)
            {
                data.Title = model.Title;
                data.Author = model.Author;
                data.Description = model.Description;
                data.CreatedOn = DateTime.UtcNow;
                data.UpdateOn = DateTime.UtcNow;
                data.Pages = model.Pages;
                data.Category = model.Category;
                data.LanguageID = model.LanguageID;
                data.Pages = model.Pages;
                data.ReleaseDate = model.ReleaseDate;
                data.CoverImageUrl = model.CoverImageUrl;
                data.BookPdfURL = model.BookPdfURL; 
                data.BookGallery = new List<BookGallery>();
                foreach (var file in model.Gallery)
                {
                    data.BookGallery.Add(new BookGallery()
                    {
                        Name = file.Name,
                        URL = file.URL
                    });
                }

                bookStoreContext.Books.Update(data);
                bookStoreContext.SaveChanges();
            }
            return data.ID;
        }

        //public List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel() {ID = 1, Title = "C#", Author = "Armen", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eget egestas diam.", Image = "~/Images/Books/unity01.png", Language = "English", Category = "Development", Pages = 220, ReleaseDate = DateTime.Now.ToString("yy/M/dd")},
        //        new BookModel() {ID = 2, Title = "Java", Author = "Poxos", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eget egestas diam.", Image = "~/Images/Books/unity02.png", Language = "English", Category = "Development", Pages = 220, ReleaseDate = DateTime.Now.ToString("yy/M/dd")},
        //        new BookModel(){ID = 3, Title ="ASP.NET COR MVC", Author = "Petros", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eget egestas diam.", Image = "~/Images/Books/unity03.png", Language = "English", Category = "Development", Pages = 220, ReleaseDate = DateTime.Now.ToString("yy/M/dd")},
        //        new BookModel(){ID = 4, Title ="ASP.NET COR MVC", Author = "Petros", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eget egestas diam.", Image = "~/Images/Books/unity03.png", Language = "English", Category = "Development", Pages = 220, ReleaseDate = DateTime.Now.ToString("yy/M/dd")},
        //        new BookModel(){ID = 5, Title ="ASP.NET COR MVC", Author = "Petros", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eget egestas diam.", Image = "~/Images/Books/unity03.png", Language = "English", Category = "Development", Pages = 220, ReleaseDate = DateTime.Now.ToString("yy/M/dd")}
        //    };
        //}
    }
}
