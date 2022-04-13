using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MyTask.BookStore.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public string GetUserId()
        {
            return httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool UserAuthenticated()
        {
            return httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
