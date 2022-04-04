using System;
using System.Collections.Generic;

#nullable disable

namespace CORE05.Models
{
    public partial class Districtinf
    {
        public Districtinf()
        {
            Employeedetails = new HashSet<Employeedetail>();
        }

        public int Id { get; set; }
        public string District { get; set; }

        public virtual ICollection<Employeedetail> Employeedetails { get; set; }
    }
}
