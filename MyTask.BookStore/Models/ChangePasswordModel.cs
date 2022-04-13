using System.ComponentModel.DataAnnotations;

namespace MyTask.BookStore.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage ="Please enter curent password")]
        [DataType(DataType.Password)]
        [Display(Name = "Curent passsword")]
        public string CurentPassword { get; set; }

        [Required(ErrorMessage = "Please enter new password")]
        [DataType(DataType.Password)]
        [Display(Name = "new passsword")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter confirm new password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new passsword")]
        [Compare("NewPassword", ErrorMessage = "Password dos not mached")]
        public string ConfirmNewPassword { get; set; }
    }
}
