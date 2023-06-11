
using BloggWeb.Data;
using BloggWeb.Models;
using BloggWeb.Models.Domain;
using BloggWeb.Models.modelView;
using BloggWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BloggWeb.Controllers
{
    //dbcontext injection
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository) //create a sign field of tagRepository
        { 
            this.tagRepository = tagRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest) //input parameter this is model
        { //generate tag model sa babaw
            //mapping AddTagRequest
            var tag = new Tag // import model sa babaw para ma read ang Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
           await tagRepository.AddAsync(tag);
            return RedirectToAction("List");//redirect to other page    
            
        }

        [HttpGet] // to make sure that this is get
        [ActionName("List")]
      public async Task<IActionResult> List()
        {
            //use DbContext to read the Tag   

            var tags = await tagRepository.GetAllAsync(); //Tags is a table set naa sa DbContext


            return View(tags);//insert here Tags para ma display sa list tag form
        }

        [HttpGet]  //displAY data get the id into GetAsync
        public async Task<IActionResult> Edit(Guid Id) ///parameter must mutch of an Id to the list edit
        {
            var tag = await tagRepository.GetAsync(Id);


            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id          = tag.Id,
                    Name        = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }

            return View(null);
        }//end of function


        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var upDatedTag = await tagRepository.UpdateAsync(tag);

            if (upDatedTag != null)
            {
                //show the notification 
             
            }
            else
            {
                //error show notification 
            }
                     // send a parameter Id
            return RedirectToAction("list");

       
        } //end of function

        /// <summary>
        /// Delete Portion
        /// </summary>
        /// <param name="editTagRequest"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(EditTagRequest editTagRequest)
        {

         var deleteTag = await tagRepository.DeleteAsync(editTagRequest.Id);
  
             if(deleteTag != null)
            {
                // show success notification
                return RedirectToAction("List");     
            }

            return RedirectToAction("List");
        }
          
        }//end of function
       
    
    }


