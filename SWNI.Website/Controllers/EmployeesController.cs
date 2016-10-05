using SWNI.Entities;
using SWNI.Services;
using SWNI.Website;
using SWNI.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SWNI.Website.Controllers
{
    [Authorize(Roles="Admin")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService employeeServices;
       
        private readonly ApplicationUserManager userManager;
        

        public EmployeesController(IEmployeeService employeeServices, ApplicationUserManager userManager)
        {
            this.employeeServices = employeeServices;            
            this.userManager = userManager;            
        }

        // GET: Employees
        public ActionResult Index(int? agencyId)
        {
            if (agencyId != null)
            {
                return View(employeeServices.GetAll());            
            }
            return View(employeeServices.GetAll());            
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateEmployeeViewModel model = PrepareCreateView();
            return View(model);
        }

        private CreateEmployeeViewModel PrepareCreateView()
        {            
            CreateEmployeeViewModel model = new CreateEmployeeViewModel(CreateEmployeeViewModel.GetRoles());
            return model;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {   
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    Employee emp = new Employee() { CreatedBy = User.Identity.Name, DateCreated = DateTime.Now, Designation = model.Designation, Name = model.Name, UserName = model.UserName };
                    if (!employeeServices.Exists(emp))
                    {
                        employeeServices.Insert(emp);
                    }
                    await userManager.AddToRoleAsync(user.Id, model.Role);
                }
                return RedirectToAction("Index");
            }            
            return View();
        }
    }
}