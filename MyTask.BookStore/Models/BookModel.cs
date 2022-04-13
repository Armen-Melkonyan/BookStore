using Microsoft.AspNetCore.Http;
using MyTask.BookStore.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyTask.BookStore.Models
{
    public class BookModel
    {
        public int ID { get; set; }

        //To using required we must using System.ComponentModel.DataAnnotations name space 
        [Required(ErrorMessage ="Please enter yore book name")]
        public string Title { get; set; }

        [MyCustomValidationAttribute("Author_ ")]
        public string Author { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public int LanguageID { get; set; }

        public string Language { get; set; }

        public int Pages { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string ReleaseDate { get; set; }

        public string Category { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateOn { get; set; }

        [Display(Name ="Choose the cover photo of your book.")]
        [Required]
        public IFormFile CoverPhoto { get; set; }

        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the cover photo of your book.")]
        [Required]
        public IFormFileCollection GalleryFile { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        [Display(Name = "Upload your book in pdf format.")]
        [Required]
        public IFormFile BookPdf { get; set; }

        public string BookPdfURL { get; set; }
    }
}