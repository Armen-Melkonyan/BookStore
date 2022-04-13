using System.ComponentModel.DataAnnotations;

namespace MyTask.BookStore.Models
{
    public class SigninModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
