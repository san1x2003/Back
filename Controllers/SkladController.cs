using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using site.Data;
using site.Models;
using site.Models.Domain;


namespace Kours.Controllers
{
    public class SkladController : Controller
    {
        private readonly MVCMagazinDbContext mvcMagazinDbContext;

        public SkladController(MVCMagazinDbContext mvcMagazinDbContext)
        {
            this.mvcMagazinDbContext = mvcMagazinDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Sklad = await mvcMagazinDbContext.Sklad.ToListAsync();
            return View(Sklad);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddSkladViewModel addSkladRequest)
        {
            var sklad = new Sklad()
            {
                Id = Guid.NewGuid(),
                NumberSklad = addSkladRequest.NumberSklad,
                Adress = addSkladRequest.Adress
            };

            await mvcMagazinDbContext.Sklad.AddAsync(sklad);
            await mvcMagazinDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var sklad = await mvcMagazinDbContext.Sklad.FirstOrDefaultAsync(x => x.Id == id);
            if (sklad != null)
            {
                var viewModel = new UpdateSkladViewModel()
                {
                    Id = sklad.Id,
                    NumberSklad = sklad.NumberSklad,
                    Adress = sklad.Adress
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateSkladViewModel model)
        {
            var sklad = await mvcMagazinDbContext.Sklad.FindAsync(model.Id);
            if (sklad != null)
            {
                sklad.NumberSklad = model.NumberSklad;
                sklad.Adress = model.Adress;

                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateSkladViewModel model)
        {
            var sklad = await mvcMagazinDbContext.Sklad.FindAsync(model.Id);

            if (sklad != null)
            {
                mvcMagazinDbContext.Sklad.Remove(sklad);
                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }

}