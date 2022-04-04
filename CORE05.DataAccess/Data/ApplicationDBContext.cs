using CORE05.Models;
using Microsoft.EntityFrameworkCore;
using System;

#nullable disable

namespace CORE05.DataAccess.Data
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
        }

        public virtual DbSet<Districtinf> Districtinfs { get; set; }
        public virtual DbSet<Employeedetail> Employeedetails { get; set; }
        public virtual DbSet<Hobbiesinf> Hobbiesinfs { get; set; }   

    }
}
