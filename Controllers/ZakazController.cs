using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using site.Data;
using site.Models.Domain;
using site.Models;

namespace Kours.Controllers
{
    public class ZakazController : Controller
    {
        private readonly MVCMagazinDbContext mvcMagazinDbContext;

        public ZakazController(MVCMagazinDbContext mvcMagazinDbContext)
        {
            this.mvcMagazinDbContext = mvcMagazinDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Zakaz = await mvcMagazinDbContext.Zakaz.ToListAsync();
            return View(Zakaz);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddZakazViewModel addZakazRequest)
        {
            var zakaz = new Zakaz()
            {
                Id = Guid.NewGuid(),
                ClientID = addZakazRequest.ClientID,
                SkladId = addZakazRequest.SkladId,
                EmployeeId = addZakazRequest.EmployeeId,
                TovarId = addZakazRequest.TovarId,
                NumberContact = addZakazRequest.NumberContact,
                Data = addZakazRequest.Data,
                Adress = addZakazRequest.Adress,
                OrderDate = addZakazRequest.OrderDate
            };

            await mvcMagazinDbContext.Zakaz.AddAsync(zakaz);
            await mvcMagazinDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var zakaz = await mvcMagazinDbContext.Zakaz.FirstOrDefaultAsync(x => x.Id == id);
            if (zakaz != null)
            {
                var viewModel = new UpdateZakazViewModel()
                {
                    Id = zakaz.Id,
                    ClientID = zakaz.ClientID,
                    SkladId = zakaz.SkladId,
                    EmployeeId = zakaz.EmployeeId,
                    TovarId = zakaz.TovarId,
                    NumberContact = zakaz.NumberContact,
                    Data = zakaz.Data,
                    Adress = zakaz.Adress,
                    OrderDate = zakaz.OrderDate
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateZakazViewModel model)
        {
            var zakaz = await mvcMagazinDbContext.Zakaz.FindAsync(model.Id);
            if (zakaz != null)
            {
                zakaz.ClientID = model.ClientID;
                zakaz.SkladId = model.SkladId;
                zakaz.EmployeeId = model.EmployeeId;
                zakaz.TovarId = model.TovarId;
                zakaz.NumberContact = model.NumberContact;
                zakaz.Data = model.Data;
                zakaz.Adress = model.Adress;
                zakaz.OrderDate = model.OrderDate;

                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateZakazViewModel model)
        {
            var zakaz = await mvcMagazinDbContext.Zakaz.FindAsync(model.Id);

            if (zakaz != null)
            {
                mvcMagazinDbContext.Zakaz.Remove(zakaz);
                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }

}

