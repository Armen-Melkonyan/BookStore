using Microsoft.AspNetCore.Mvc;
using MyTask.BookStore.Models;
using MyTask.BookStore.Repository;
using System.Threading.Tasks;

namespace MyTask.BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        //new commet for push
        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        //Registration
        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel signUpUserModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accountRepository.CreateUserAsync(signUpUserModel);
                if (!result.Succeeded)
                {
                    foreach (var resultErrors in result.Errors)
                    {
                        ModelState.AddModelError("", resultErrors.Description);
                    }
                    return View(signUpUserModel);
                }
                ModelState.Clear();
            }
            return View();
        }

        [Route("login")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SigninModel signinModel, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var result = await accountRepository.SignInPasswordAsync(signinModel);
                if (result.Succeeded)
                {
                    //Check if url is from another page 
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not allowed to login");
                }else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account blocked. Try after some time!");
                }
                else
                {
                    ModelState.AddModelError("", "Invaliv credential");
                }
            }
            return View();
        }

        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("/Change_Passwword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("/Change_Passwword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePassword)
        {
            if (ModelState.IsValid)
            {
                var result = await accountRepository.Changepassword(changePassword);

                if (result.Succeeded)
                {
                    ViewBag.isSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(changePassword);
        }
    }
}
