namespace BloggWeb.Models.Domain
{
    public class BlogPost
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
        // blogpost can have multiple Tags// #001get this property in to 001-
        public ICollection<Tag>? Tags{ get; set; } //many to many relationship
    }
}
