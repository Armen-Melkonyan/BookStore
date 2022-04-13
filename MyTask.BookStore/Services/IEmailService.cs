using MyTask.BookStore.Models;
using System.Threading.Tasks;

namespace MyTask.BookStore.Services
{
    public interface IEmailService
    {
        Task SendtestEmail(UserEmailOptions userEmailOptions);
    }
}