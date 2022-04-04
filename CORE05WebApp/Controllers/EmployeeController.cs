using CORE05.DataAccess.Data;
using CORE05.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE05WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDBContext _db;
        public EmployeeController(ApplicationDBContext db)
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
            List<Districtinf> disList = new List<Districtinf>();
            disList = (from c in _db.Districtinfs select c).ToList();
            disList.Insert(0, new Districtinf { Id = 0, District = "--Select District Name--" });
            ViewBag.message = disList;
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
