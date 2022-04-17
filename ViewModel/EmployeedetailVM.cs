using CORE05.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE05.ViewModel
{
    public class EmployeedetailVM
    {
        public int Id { get; set; }
        [StringLength(12, MinimumLength = 8)]
        [Required(ErrorMessage = "Please Enter 12 didgit Id")]
        public string Employeeid { get; set; }
        [StringLength(255)]
        [Display(Name = "Employee Name")]
        [Required(ErrorMessage = "Please Enter Employee Name")]
        public string Employeename { get; set; }
        public DateTime Joiningdate { get; set; }
        public string Gender { get; set; }
        public int Districtid { get; set; }
        public bool Active { get; set; }
        public string Image { get; set; }
        public decimal Salary { get; set; }
        public string Hobbiesid { get; set; }
        [NotMapped]
        public string hobbiesName { get; set; }
        [NotMapped]
        [Display(Name = "Hobbies")]
        public string[] hobbiesArr { get; set; }

        public string Address { get; set; }
        public IFormFile ProfileImage { get; set; }

        public virtual Districtinf District { get; set; }
    }
}
