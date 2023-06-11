namespace BloggWeb.Models.Domain
{
    public class Tag
    {

        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public ICollection<BlogPost>? BlogPosts{ get; set; } // many to many relationship tag can have multiple Blogpost


    }
}
