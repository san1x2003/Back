using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using site.Data;
using site.Models;
using site.Models.Domain;


namespace Kours.Controllers
{
    public class ClientController : Controller
    {
        private readonly MVCMagazinDbContext mvcMagazinDbContext;

        public ClientController(MVCMagazinDbContext mvcMagazinDbContext)
        {
            this.mvcMagazinDbContext = mvcMagazinDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Client = await mvcMagazinDbContext.Client.ToListAsync();
            return View(Client);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddClientViewModel addClientRequest)
        {
            var client = new Client()
            {
                Id = Guid.NewGuid(),
                Email = addClientRequest.Email,
                PhoneNumber = addClientRequest.PhoneNumber,
                Address = addClientRequest.Address
            };

            await mvcMagazinDbContext.Client.AddAsync(client);
            await mvcMagazinDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var client = await mvcMagazinDbContext.Client.FirstOrDefaultAsync(x => x.Id == id);
            if (client != null)
            {
                var viewModel = new UpdateClientViewModel()
                {
                    Id = client.Id,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber,
                    Address = client.Address
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateClientViewModel model)
        {
            var client = await mvcMagazinDbContext.Client.FindAsync(model.Id);
            if (client != null)
            {
                client.Email = model.Email;
                client.PhoneNumber = model.PhoneNumber;
                client.Address = model.Address;

                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateClientViewModel model)
        {
            var client = await mvcMagazinDbContext.Client.FindAsync(model.Id);

            if (client != null)
            {
                mvcMagazinDbContext.Client.Remove(client);
                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }

}
