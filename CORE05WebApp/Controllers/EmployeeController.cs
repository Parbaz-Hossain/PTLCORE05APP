using CORE05.DataAccess.Data;
using CORE05.Models;
using CORE05.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CORE05WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDBContext _db;
        private IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(ApplicationDBContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db=db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //List<Employeedetail> empDetList = new List<Employeedetail>(); //List
            List<EmployeedetailVM> empVMList = new List<EmployeedetailVM>();
            IEnumerable<Employeedetail> empDetList = _db.Employeedetails.Include(x => x.District); //IEnumerable

            foreach (var item in empDetList)
            {
                List<Hobbiesinf> hobbList = new List<Hobbiesinf>();
                hobbList = _db.Hobbiesinfs.ToList();

                foreach (var hItem in item.Hobbiesid.Split(","))
                {
                    if (hItem !="")
                    {
                        item.hobbiesName+= hobbList.Where(x => x.Id.ToString()==hItem).FirstOrDefault().Hobbies+",";
                    }

                }
            }

            foreach (var item in empDetList)
            {
                EmployeedetailVM employeeVM = new EmployeedetailVM(); //VeiwModel Object
                employeeVM.Employeeid = item.Employeeid;
                employeeVM.Employeename = item.Employeename;
                employeeVM.Joiningdate = item.Joiningdate;
                employeeVM.Gender = item.Gender;
                employeeVM.Districtid = item.Districtid;
                employeeVM.District = item.District;
                employeeVM.Active = item.Active;
                employeeVM.Salary =item.Salary;
                employeeVM.Hobbiesid=item.Hobbiesid;
                employeeVM.hobbiesName=item.hobbiesName;
                employeeVM.Address=item.Address;
                empVMList.Add(employeeVM);
            }

            return View(empVMList);
        }

        [HttpGet]
        public IActionResult Create(int EmpId)
        {

            List<Districtinf> disList = new List<Districtinf>();
            disList = _db.Districtinfs.ToList();
            disList.Insert(0, new Districtinf { Id = 0, District = "--Select District Name--" });
            ViewBag.message = disList;

            List<Hobbiesinf> hobbList = new List<Hobbiesinf>();
            hobbList = _db.Hobbiesinfs.ToList();// (from c in _db.Hobbiesinfs select c).ToList();           
            ViewBag.message1 = hobbList;
            var items = new Employeedetail() { Joiningdate=System.DateTime.Now };

            if (EmpId!=0)
            {
                items=_db.Employeedetails.Where(x => x.Id==EmpId).FirstOrDefault();
                string[] hobbiesArr = items.Hobbiesid.Split(",").ToArray();
                items.hobbiesArr=hobbiesArr;
            }
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeedetailVM empdetVM)
        {
            string fileName = UploadFile(empdetVM);

            var employee = new Employeedetail
            {
                Employeeid = empdetVM.Employeeid,
                Employeename = empdetVM.Employeename,
                Joiningdate = empdetVM.Joiningdate,
                Gender = empdetVM.Gender,
                Districtid = empdetVM.Districtid,
                Active = empdetVM.Active,
                Image= fileName,
                Salary =empdetVM.Salary,
                Hobbiesid=empdetVM.Hobbiesid,
                hobbiesName=empdetVM.hobbiesName,
                Address=empdetVM.Address
            };

            if (ModelState.IsValid)
            {
                if (employee.hobbiesArr!=null)
                {
                    foreach (var item in employee.hobbiesArr)
                    {
                        employee.Hobbiesid+=item.Trim()+",";
                    }
                }
                if (employee.Id!=0)
                {
                    _db.Employeedetails.Update(employee);
                }
                else
                {
                    _db.Employeedetails.Add(employee);
                }


                bool isChanged = Convert.ToBoolean(_db.SaveChanges());
                if (isChanged)
                {
                    TempData["success"] = "Employee Inserted Successfully";
                }
                else
                {
                    TempData["error"] = "Employee Inserted Failded!";
                }
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //IMAGE Upload
        private string UploadFile(EmployeedetailVM empdetVM)
        {
            string fileName = null;
            if (empdetVM.ProfileImage!=null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "ProfileImages");
                fileName = Guid.NewGuid().ToString() + "-" + empdetVM.ProfileImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    empdetVM.ProfileImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
