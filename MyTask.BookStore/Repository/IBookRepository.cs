using MyTask.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTask.BookStore.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel model);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBook(int id);
        Task<List<BookModel>> GetTopBooksAsync(int count);
        List<BookModel> SearchBooks(string name);
        void Delete(int id);
        int Update(BookModel model, int id);
    }
}