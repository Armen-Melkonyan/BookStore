using Microsoft.Extensions.Options;
using MyTask.BookStore.Models;

namespace MyTask.BookStore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        //Create instance of configuration to use appsettings.json in controller
        private readonly IOptionsMonitor<NewBookAlertModel> _newBookAlertModel;
        public MessageRepository(IOptionsMonitor<NewBookAlertModel> newBookAlertModel)
        {
            _newBookAlertModel = newBookAlertModel;
        }

        public string GatName()
        {
            return _newBookAlertModel.CurrentValue.Alert;
        }
    }
}
