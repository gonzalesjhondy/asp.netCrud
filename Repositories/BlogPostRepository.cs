using BloggWeb.Data;
using BloggWeb.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BloggWeb.Repositories
{                                  //import IBlogPostRepository to implement IBlogPostRepository
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieWebDbContext bloggieWebDbContext;
                                              // create assing filed to import
        public BlogPostRepository(BloggieWebDbContext bloggieWebDbContext)
        {
            this.bloggieWebDbContext = bloggieWebDbContext;
        }

        public async Task<BlogPost?> AddAsync(BlogPost blogPost)
        {
             await bloggieWebDbContext.AddAsync(blogPost);
            await bloggieWebDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
          var existingBlog = await bloggieWebDbContext.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
                bloggieWebDbContext.BlogPosts.Remove(existingBlog);
                await bloggieWebDbContext.SaveChangesAsync();  
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        { //put an include to get related property//get the x=>x.tags from the BlogPost to bring back database 001->BlogPostRepository
            return await bloggieWebDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid id)
        { //put an include to get related property//get the x=>x.tags from the BlogPost to bring back database 001->BlogPostRepository
           return await bloggieWebDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {                                      //getting the context of the tag
           var existingBlog = await bloggieWebDbContext.BlogPosts.Include(x => x.Tags).
            FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if(existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDiscription = blogPost.ShortDiscription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.URLHandle = blogPost.URLHandle;
                existingBlog.Visible = existingBlog.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags; //insert this here lain nga table
                await bloggieWebDbContext.SaveChangesAsync();       
                return existingBlog;
            }
            return null; // back to the controller
        }
    }
}
