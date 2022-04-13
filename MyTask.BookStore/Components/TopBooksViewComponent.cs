using Microsoft.AspNetCore.Mvc;
using MyTask.BookStore.Repository;
using System.Threading.Tasks;

namespace MyTask.BookStore.Components
{
    public class TopBooksViewComponent :ViewComponent
    {
        private readonly IBookRepository bookRepository;
        public TopBooksViewComponent(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await bookRepository.GetTopBooksAsync(count);
            return View(books);
        }
    }
}
