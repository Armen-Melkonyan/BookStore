using Microsoft.AspNetCore.Identity;
using MyTask.BookStore.Models;
using System.Threading.Tasks;

namespace MyTask.BookStore.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpUserModel);
        Task<SignInResult> SignInPasswordAsync(SigninModel signinModel);
        Task SignOutAsync();
        Task<IdentityResult> Changepassword(ChangePasswordModel changePassword);
    }
}