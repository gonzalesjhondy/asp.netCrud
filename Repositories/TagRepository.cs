using Azure;
using BloggWeb.Data;
using BloggWeb.Models.Domain;
using BloggWeb.Models.modelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BloggWeb.Repositories
{                      //import Implement Interface sa ITagRepository
    public class TagRepository : ITagRepository //ITagRepository kay interface class
    {
        private readonly BloggieWebDbContext bloggieWebDbContext;

        public TagRepository(BloggieWebDbContext bloggieWebDbContext)
        {
            this.bloggieWebDbContext = bloggieWebDbContext;
        }


        public async Task<Tag?> AddAsync(Tag tag)
        {
            await bloggieWebDbContext.Tags.AddAsync(tag);
            await bloggieWebDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
           var existingtag = await bloggieWebDbContext.Tags.FindAsync(id);
           if(existingtag != null) 
            { 
              bloggieWebDbContext.Tags.Remove(existingtag);
              await bloggieWebDbContext.SaveChangesAsync();
            }

            //error notification
            return null;
        }   

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
          return await bloggieWebDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return await bloggieWebDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggieWebDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await bloggieWebDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}
