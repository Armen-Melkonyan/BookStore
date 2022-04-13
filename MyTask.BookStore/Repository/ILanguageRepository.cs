using MyTask.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTask.BookStore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguagesModel>> GetLanguage();
    }
}