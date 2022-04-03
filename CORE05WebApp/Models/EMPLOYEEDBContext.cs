using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;

#nullable disable

namespace CORE05WebApp.Models
{
    public partial class EMPLOYEEDBContext : DbContext
    {
        public EMPLOYEEDBContext(DbContextOptions<EMPLOYEEDBContext> options): base(options)
        {
        }

        public virtual DbSet<Districtinf> Districtinfs { get; set; }
        public virtual DbSet<Employeedetail> Employeedetails { get; set; }
        public virtual DbSet<Hobbiesinf> Hobbiesinfs { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Districtinf>(entity =>
            {
                entity.ToTable("DISTRICTINF");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(520)
                    .HasColumnName("DISTRICT");
            });

            modelBuilder.Entity<Employeedetail>(entity =>
            {
                entity.ToTable("EMPLOYEEDETAILS");

                entity.HasIndex(e => e.Id, "IX_EMPLOYEEDETAILS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active).HasColumnName("ACTIVE");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("ADDRESS")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Districtid).HasColumnName("DISTRICTID");

                entity.Property(e => e.Employeeid)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("EMPLOYEEID")
                    .HasDefaultValueSql("('000000000000')")
                    .IsFixedLength(true);

                entity.Property(e => e.Employeename)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("EMPLOYEENAME")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("GENDER")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Hobbiesid)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("HOBBIESID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("IMAGE")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Joiningdate)
                    .HasColumnType("datetime")
                    .HasColumnName("JOININGDATE")
                    .HasDefaultValueSql("('01-Jan-1900')");

                entity.Property(e => e.Salary)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("SALARY")
                    .HasDefaultValueSql("((0.00))");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Employeedetails)
                    .HasForeignKey(d => d.Districtid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEEDETAILS_EMPLOYEEDETAILS");
            });

            modelBuilder.Entity<Hobbiesinf>(entity =>
            {
                entity.ToTable("HOBBIESINF");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Hobbies)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("HOBBIES");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
