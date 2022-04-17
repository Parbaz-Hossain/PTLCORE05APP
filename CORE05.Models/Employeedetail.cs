using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CORE05.Models
{
    public partial class Employeedetail
    {
        public int Id { get; set; }
        public string Employeeid { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name ="Employee Name")]
        public string Employeename { get; set; }
        public DateTime Joiningdate { get; set; }
        public string Gender { get; set; }
        public bool Active { get; set; }
        public string Image { get; set; }
        public decimal Salary { get; set; }
        public string Hobbiesid { get; set; }
        public string Address { get; set; }

        public int Districtid { get; set; }
        [ForeignKey("Districtid")]
        public virtual Districtinf District { get; set; }
    }
}
