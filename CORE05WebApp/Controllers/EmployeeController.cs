using CORE05WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE05WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EMPLOYEEDBContext _db;
        public EmployeeController(EMPLOYEEDBContext db)
        {
            _db=db;
        }
        public IActionResult Index()
        {
            IEnumerable<Employeedetail> objList = _db.Employeedetails;
            return View(objList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employeedetail employee)
        {
            if(ModelState.IsValid)
            {
                _db.Employeedetails.Add(employee);
                bool isChanged = Convert.ToBoolean(_db.SaveChanges());
                if(isChanged)
                {
                    ViewBag.Message = "Employee Inserted Successfully";
                }
                else
                {
                    ViewBag.Message = "Employee Inserted Failded!";
                }
                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}
