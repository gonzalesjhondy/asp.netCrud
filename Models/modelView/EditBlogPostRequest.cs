using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloggWeb.Models.modelView
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; }
   
        public string? Heading { get; set; }

        public string? PageTitle { get; set; }

        public string? Content { get; set; }

        public string? ShortDiscription { get; set; }

        public string? FeaturedImageUrl { get; set; }

        public string? URLHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string? Author { get; set; }

        public bool Visible { get; set; }

        //Display tags   import electListItem tor render Microsoft.AspNetCore.Mvc.Rendering
        public IEnumerable<SelectListItem>? Tags { get; set; }
        //Collect multiple Tag or select multiple value
        public string[] SelectedTag { get; set; } = Array.Empty<string>();

    }
}
