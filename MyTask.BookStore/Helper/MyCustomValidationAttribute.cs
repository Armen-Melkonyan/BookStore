using System.ComponentModel.DataAnnotations;

namespace MyTask.BookStore.Helper
{
    public class MyCustomValidationAttribute : ValidationAttribute
    {
         //We can pass teh if condition by using property or constracture
        public string Text { get; set; }

        //We can using constracture to pass validate text and check it by if condition
        public MyCustomValidationAttribute(string text)
        {
            Text = text;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // Convert value to string and check if it has "mvc" return success message
                string bookName = value as string;
                if (bookName.Contains(Text))
                {
                    return ValidationResult.Success;
                }
                //Or can return ValidationResult.Success directly without if condition
            }

            //Pass the error message to ValidationResult
            return new ValidationResult(ErrorMessage ?? "From helper Value is empty!");
        }
    }
}
