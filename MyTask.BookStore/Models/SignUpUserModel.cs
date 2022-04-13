using System.ComponentModel.DataAnnotations;

namespace MyTask.BookStore.Models
{
    public class SignUpUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage ="Please enter your email")]
        [EmailAddress(ErrorMessage ="Please enter a valid email")]
        [Display(Name ="Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter your password")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Please enter confirm password")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm passsword")]
        [Compare("Password", ErrorMessage = "Password dos not mached")]
        public string ConfirmPassword { get; set; }
    }
}
