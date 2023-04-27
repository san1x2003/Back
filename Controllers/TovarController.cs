using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using site.Data;
using site.Models;
using site.Models.Domain;


namespace Kours.Controllers
{
    public class TovarController : Controller
    {
        private readonly MVCMagazinDbContext mvcMagazinDbContext;

        public TovarController(MVCMagazinDbContext mvcMagazinDbContext)
        {
            this.mvcMagazinDbContext = mvcMagazinDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Tovar = await mvcMagazinDbContext.Tovar.ToListAsync();
            return View(Tovar);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddTovarViewModel addTovarRequest)
        {
            var tovar = new Tovar()
            {
                Id = Guid.NewGuid(),
                Name = addTovarRequest.Name,
                Price = addTovarRequest.Price,
                DeliveryTime = addTovarRequest.DeliveryTime
            };

            await mvcMagazinDbContext.Tovar.AddAsync(tovar);
            await mvcMagazinDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var tovar = await mvcMagazinDbContext.Tovar.FirstOrDefaultAsync(x => x.Id == id);
            if (tovar != null)
            {
                var viewModel = new UpdateTovarViewModel()
                {
                    Id = tovar.Id,
                    Name = tovar.Name,
                    Price = tovar.Price,
                    DeliveryTime = tovar.DeliveryTime
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateTovarViewModel model)
        {
            var tovar = await mvcMagazinDbContext.Tovar.FindAsync(model.Id);
            if (tovar != null)
            {
                tovar.Name = model.Name;
                tovar.Price = model.Price;
                tovar.DeliveryTime = model.DeliveryTime;

                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateTovarViewModel model)
        {
            var tovar = await mvcMagazinDbContext.Tovar.FindAsync(model.Id);

            if (tovar != null)
            {
                mvcMagazinDbContext.Tovar.Remove(tovar);
                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }

}
