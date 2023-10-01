using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace _9th_Monthly_Exam.Models
{
    public enum Specialist { Medicine = 1, Surgery, Nephrologists, Cardiologists }
    public class Doctor
    {
        public int DoctorId { get; set; }
        [Required, StringLength(50)]
        public string DoctorName { get; set; } = default!;
        [Required, EnumDataType(typeof(Specialist))]
        public Specialist? Specialist { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal Fees { get; set; }
        [Required, StringLength(50)]
        public string? Picture { get; set; } = default!;

        public bool? OnAvilable { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
    public class Appointment
    {
        public int AppointmentId { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime? AppointmentDate { get; set; }
        public int? TotalPatient { get; set; }
        [Required, ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
    public class DoctorDbContext : DbContext
    {
        public DoctorDbContext(DbContextOptions<DoctorDbContext> options) : base(options) { }
        public DbSet<Doctor> Doctors { get; set; } = default!;
        public DbSet<Appointment> Appointments { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Random random = new Random();
            for (int i = 1; i <= 5; i++)
            {
                modelBuilder.Entity<Doctor>().HasData(
                  new Doctor { DoctorId = i, DoctorName = "Doctor" + i, Specialist = (Specialist)random.Next(1, 5), Fees = random.Next(1000, 2000) * 1.00M, OnAvilable = true, Picture = i + ".jpg" }

             );
            }
            for (int i = 1; i <= 8; i++)
            {
                modelBuilder.Entity<Appointment>().HasData(
                   new Appointment { AppointmentId = i, AppointmentDate = DateTime.Now.AddDays(-1 * random.Next(200, 500)), TotalPatient = random.Next(10, 100), DoctorId = i % 5 == 0 ? 5 : i % 5 }

               );
            }

        }
    }
}
