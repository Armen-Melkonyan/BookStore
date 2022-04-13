using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyTask.BookStore.Models;
using MyTask.BookStore.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyTask.BookStore.Controllers
{
    [Route("[Controller]/[action]")]
    public class BookController : Controller
    {
        //Create instance of repositorys hear. (Add Dependencis in the StartUp.cs calss "services.AddScoped<LanguageRepository, LanguageRepository>();").
        private readonly IBookRepository bookRepository = null;
        private readonly ILanguageRepository languageRepository = null;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BookController(IBookRepository bookRepository,
            ILanguageRepository languageRepository, 
            IWebHostEnvironment webHostEnvironment)
        {
            //bookRepository = new BookRepository();
            this.bookRepository = bookRepository;
            this.languageRepository = languageRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        //Use this property to set title of layout page
        [ViewData]
        public string Title { get; set; }

        [Route("~/all-books")]
        public async Task<ViewResult> GetAllBooks()
        {
            Title = "All Books";
            var data = await bookRepository.GetAllBooks();
            return View(data);
        }

        [Route("~/book-details/{id:int:min(1)}", Name = "BookDetails")]
        public async Task<ViewResult> GetBook(int id)
        {
            Title = "Book Detailes";
            var data = await bookRepository.GetBook(id);
            return View(data);
        }

        [Route("~/delete-book")]
        public IActionResult DeleteBook(int id)
        {
            bookRepository.Delete(id);
            return RedirectToAction(nameof(AddNewBook));
        }

        public List<BookModel> SearchBooks(string name)
        {
            return bookRepository.SearchBooks(name);
        }

        [Authorize]
        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0, int pg = 0)
        {
            Title = "Add New Book";
            //To set this options in select tag. Add asp-item attribute to select tag
            //ViewBag.Language =new SelectList(GetLanguage(), "ID", "Text");

            ViewBag.Language = new SelectList(await languageRepository.GetLanguage(), "ID", "Name");

            //Using to show aler message in the page of form
            ViewBag.IsSuccess = isSuccess;
            //pass id to view page
            if (bookId > 0)
            {
                ViewBag.BookId = bookId;
            }
            //_____________________________________________________________
            ViewModel viewModel = new ViewModel();

            viewModel.AllBooks = await bookRepository.GetAllBooks();
            viewModel.Book = new BookModel();

            const int pageSize = 4;
            if (pg < 1)
            {
                pg = 1;
            }
            //get count of all books 
            int bookCounts = viewModel.AllBooks.Count();

            var pager = new PaginationModel(bookCounts, pg, pageSize);
            int bookSkip = (pg - 1) * pageSize;
            viewModel.CurrentPageData = viewModel.AllBooks.Skip(bookSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel, int editId = 0)
        {
            Title = "Add New Book";

            if (ModelState.IsValid)
            {
                //Set image to database
                if (bookModel.CoverPhoto != null)
                {
                    //Path of books cover in wwwwroot
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl = await UploadImages(folder, bookModel.CoverPhoto);

                }

                //Set Gallery to database
                if (bookModel.GalleryFile != null)
                {
                    //Path of gallery in wwwwroot
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();

                    foreach (var file in bookModel.GalleryFile)
                    {

                        var gallery = new GalleryModel()
                        {
                            Name = file.Name,
                            URL = await UploadImages(folder, file)
                        };  
                        bookModel.Gallery.Add(gallery);
                    }
                }

                //Set pdf to database
                if (bookModel.BookPdf != null)
                {
                    //Path of pdf in wwwwroot
                    string folder = "books/pdf/";
                    bookModel.BookPdfURL = await UploadImages(folder, bookModel.BookPdf);

                }

                var id = 0;
                //Call bookRepository.AddNewBook and transfer bookModel
                //Get returned id from bookRepository.AddNewBook to check it if isn't equal to 0 then redirect to AddNewBook method
                if (editId > 0)
                {
                    id = bookRepository.Update(bookModel, editId);
                }
                else
                {
                    id = await bookRepository.AddNewBook(bookModel);
                }

                //if is used to giv a empty form by redirecting to Add New Book method
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id, pg = 0 });
                }
            }

            ViewBag.Language = new SelectList(await languageRepository.GetLanguage(), "ID", "Name");

            //_____________________________________________________________
            ViewModel viewModel = new ViewModel();

            viewModel.AllBooks = await bookRepository.GetAllBooks();
            viewModel.Book = new BookModel();

            return View(viewModel);
        }

        private async Task<string> UploadImages(string folderPath, IFormFile file)
        {
            //Append image to folder. Using guid to make the name unque
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            //set Folder pathe to model 
            //bookModel.CoverImageUrl = "/" + folder;

            //create a string of wwwroot + folder
            string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folderPath);

            //copy image to folder
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

        //Used to get data from language model and set data to select tag by SelectList method
        //private List<LanguagesModel> GetLanguage()
        //{
        //    return new List<LanguagesModel>()
        //    {
        //        new LanguagesModel(){ID= 1, Text = "English"},
        //        new LanguagesModel(){ID= 2, Text = "Armenian"},
        //        new LanguagesModel(){ID= 3, Text = "Russian"}
        //    };
        //}
    }
}