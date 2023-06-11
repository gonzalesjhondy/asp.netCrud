using BloggWeb.Models.Domain;
using BloggWeb.Models.modelView;
using BloggWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BloggWeb.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get tags from repository
            var tags = await tagRepository.GetAllAsync();

            var Model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
        };
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //Map the view model to domain model
            var blogPostDomain = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDiscription = addBlogPostRequest.ShortDiscription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                URLHandle = addBlogPostRequest.URLHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };
            //Map Tags from selected Tags
            var selectedTag = new List<Tag>();
            foreach(var selectedTagId in addBlogPostRequest.SelectedTag)
            {
                var SelectedTagIdGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(SelectedTagIdGuid);

                if(existingTag != null)
                {
                    selectedTag.Add(existingTag);
                }
            }
            //mapping tags back to admin model
            blogPostDomain.Tags = selectedTag; 

            await blogPostRepository.AddAsync(blogPostDomain);

           return RedirectToAction("Add");

        }

        [HttpGet]
        public async Task<IActionResult> list()
        {
            // call repository to get the data
         var blogPosts =  await blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        { 
            //retrieve the result from the repository
           var blogPost =  await blogPostRepository.GetAsync(id);
            var TagsDomainModel = await tagRepository.GetAllAsync();
            if(blogPost != null)
            {
                //map the domain model into the view model
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    URLHandle = blogPost.URLHandle,
                    ShortDiscription = blogPost.ShortDiscription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = TagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),

                    SelectedTag = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }


            //pass data to view
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            //Map view model back to domain model
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDiscription = editBlogPostRequest.ShortDiscription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                PublishedDate = editBlogPostRequest.PublishedDate,
                URLHandle = editBlogPostRequest.URLHandle,
                Visible = editBlogPostRequest.Visible,
            };

            //Maps Tags into Domain Model
            var selectedTags = new List<Tag>();

            foreach (var selectTag in editBlogPostRequest.SelectedTag)
            {
                if(Guid.TryParse(selectTag, out var tag))
                {
                     var foundTag = await tagRepository.GetAsync(tag);

                    if (foundTag != null) 
                    {
                     selectedTags.Add(foundTag);
                    }
                }

            }
             blogPostDomainModel.Tags = selectedTags;
            //submit information to repository
           var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);//map there and pass here
            if(updatedBlog != null)
            {
                //show notification
                return RedirectToAction("Edit");
            }

            return null;
            // error notification
        }
    }
}
