using Microsoft.AspNetCore.Identity;
using MyTask.BookStore.Models;
using MyTask.BookStore.Services;
using System.Threading.Tasks;

namespace MyTask.BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUserModel> _userManager;
        private readonly SignInManager<ApplicationUserModel> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService userService;

        public AccountRepository(UserManager<ApplicationUserModel> userManager, 
            SignInManager<ApplicationUserModel> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _roleManager = roleManager;
            this.userService = userService;

        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpUserModel)
        {
            var user = new ApplicationUserModel()
            {
                Email = signUpUserModel.Email,
                UserName = signUpUserModel.Email,
                FirstName = signUpUserModel.FirstName,
                LastName = signUpUserModel.LastName
            };
            var result = await _userManager.CreateAsync(user, signUpUserModel.Password);
            return result; 
        }

        public async Task<SignInResult> SignInPasswordAsync(SigninModel signinModel)
        {
            var result = await _signinManager.PasswordSignInAsync(signinModel.Email, signinModel.Password, signinModel.RememberMe, true);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signinManager.SignOutAsync();
        }

        public async Task<IdentityResult> Changepassword(ChangePasswordModel changePassword)
        {
            var userId = userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, changePassword.CurentPassword, changePassword.NewPassword);
        }
    }
}
