using _9th_Monthly_Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _9th_Monthly_Exam.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly DoctorDbContext db;
        public AppointmentsController(DoctorDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Appointments.Include(x => x.Doctor).ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.Doctors = db.Doctors.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Appointment model)
        {
            if (ModelState.IsValid)
            {
                // Check if the DoctorId is valid
                if (db.Doctors.Any(d => d.DoctorId == model.DoctorId))
                {
                    db.Database.ExecuteSqlInterpolated($"EXEC InsertAppointment {model.AppointmentDate}, {model.TotalPatient}, {model.DoctorId}");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DoctorId", "Invalid DoctorId");
                }
            }

            ViewBag.Doctors = db.Doctors.ToList();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var data = db.Appointments.FirstOrDefault(x => x.AppointmentId == id);
            if (data == null) { return NotFound(); }
            ViewBag.Doctors = db.Doctors.ToList();
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Appointment model)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlInterpolated($"EXEC UpdateAppointment {model.AppointmentId}, {model.AppointmentDate}, {model.TotalPatient}, {model.DoctorId}");
                return RedirectToAction("Index");

            }
            ViewBag.Doctors = db.Doctors.ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            db.Database.ExecuteSqlInterpolated($"EXEC DeleteAppointment {id}");
            return Json(new { success = true, id });
        }
    }
}
