namespace MyTask.BookStore.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool UserAuthenticated();
    }
}