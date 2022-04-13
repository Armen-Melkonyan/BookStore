using Microsoft.EntityFrameworkCore;
using MyTask.BookStore.Data;
using MyTask.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTask.BookStore.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        //Create an instance of context class 
        private readonly BookStoreContext bookStoreContext = null;

        public LanguageRepository(BookStoreContext context)
        {
            bookStoreContext = context;
        }

        public async Task<List<LanguagesModel>> GetLanguage()
        {
            //First method to get all languages
            //var languages = new List<LanguagesModel>();
            //var allLanguages = await bookStoreContext.Language.ToListAsync(); 

            //second method to get all languages by usin linq
            return await bookStoreContext.Language.Select(x => new LanguagesModel()
            {
                ID = x.ID,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();

        }

    }
}
