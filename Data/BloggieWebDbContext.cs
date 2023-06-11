using BloggWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloggWeb.Data
{
    //first: type: DbContext, 2nd import DbContext: result: using Microsoft.EntityFrameworkCore;
    public class BloggieWebDbContext : DbContext //3rd highlight BloggiewebDbContext import: result: Generate Constructor with Baase(options)
    {
        public BloggieWebDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost>? BlogPosts{get; set;} //BlogPost Table

        public DbSet<Tag>? Tags { get; set;}//Tags Table

    }
}
