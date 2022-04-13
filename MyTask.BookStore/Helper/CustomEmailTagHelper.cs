using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyTask.BookStore.Helper
{
    public class CustomEmailTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a"; //To using <a></a> tag
            output.Attributes.SetAttribute("href", "mailto:armen.melkonyan2014@gmail.com");//to set an attribute mail
            output.Content.SetContent("My-Email");//value of tag <a>value</a>
            output.Attributes.Add("id", "email");//add id
        }
    }
}
