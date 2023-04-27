using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using site.Data;
using site.Models;
using site.Models.Domain;


namespace Kours.Controllers
{
    public class PostController : Controller
    {
        private readonly MVCMagazinDbContext mvcMagazinDbContext;

        public PostController(MVCMagazinDbContext mvcMagazinDbContext)
        {
            this.mvcMagazinDbContext = mvcMagazinDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Post = await mvcMagazinDbContext.Post.ToListAsync();
            return View(Post);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddPostViewModel addPostRequest)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Identificator = addPostRequest.Identificator,
                Poost = addPostRequest.Poost,
                TittlePost = addPostRequest.TittlePost
            };

            await mvcMagazinDbContext.Post.AddAsync(post);
            await mvcMagazinDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var post = await mvcMagazinDbContext.Post.FirstOrDefaultAsync(x => x.Id == id);
            if (post != null)
            {
                var viewModel = new UpdatePostViewModel()
                {
                    Id = post.Id,
                    Identificator = post.Identificator,
                    Poost = post.Poost,
                    TittlePost = post.TittlePost
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdatePostViewModel model)
        {
            var post = await mvcMagazinDbContext.Post.FindAsync(model.Id);
            if (post != null)
            {
                post.Identificator = model.Identificator;
                post.Poost = model.Poost;
                post.TittlePost = model.TittlePost;

                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdatePostViewModel model)
        {
            var post = await mvcMagazinDbContext.Post.FindAsync(model.Id);

            if (post != null)
            {
                mvcMagazinDbContext.Post.Remove(post);
                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }

}
