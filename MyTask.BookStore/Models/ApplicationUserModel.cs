using Microsoft.AspNetCore.Identity;
using System;

namespace MyTask.BookStore.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        //Add 3 new columns to user in database by inherit class from IdentityUser
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
