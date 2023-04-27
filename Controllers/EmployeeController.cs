using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using site.Data;
using site.Models;
using site.Models.Domain;


namespace Kours.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCMagazinDbContext mvcMagazinDbContext;

        public EmployeeController(MVCMagazinDbContext mvcMagazinDbContext)
        {
            this.mvcMagazinDbContext = mvcMagazinDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Employee = await mvcMagazinDbContext.Employee.ToListAsync();
            return View(Employee);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Identifier = addEmployeeRequest.Identifier,
                PostId = addEmployeeRequest.PostId
            };

            await mvcMagazinDbContext.Employee.AddAsync(employee);
            await mvcMagazinDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcMagazinDbContext.Employee.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Identifier = employee.Identifier,
                    PostId = employee.PostId
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcMagazinDbContext.Employee.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Identifier = model.Identifier;
                employee.PostId = model.PostId;

                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcMagazinDbContext.Employee.FindAsync(model.Id);

            if (employee != null)
            {
                mvcMagazinDbContext.Employee.Remove(employee);
                await mvcMagazinDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }

}

