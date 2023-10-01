using _9th_Monthly_Exam.Models;
using _9th_Monthly_Exam.ViewModels;
using _9th_Monthly_Exam.ViewModels.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace _9th_Monthly_Exam.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly DoctorDbContext db;
        private readonly IWebHostEnvironment env;
        public DoctorsController(DoctorDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public async Task <IActionResult> Index()
        {
            return View(await db.Doctors.ToListAsync());
        }
        public async Task<IActionResult> Aggregates()
        {
            var data = await db.Appointments.Include(x => x.Doctor)
                .ToListAsync();
            return View(data);
        }
        public IActionResult Grouping()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Grouping(string groupby)
        {

            if (groupby == "doctorname")
            {
                var data = db.Appointments.Include(x => x.Doctor)
               .ToList()
               .GroupBy(x => x.Doctor?.DoctorName)
               .Select(g => new GroupedData { Key = g.Key, Data = g })
               .ToList();

                return View("GroupingResult", data);
            }
            if (groupby == "year month")
            {
                var data = db.Appointments.Include(x => x.Doctor)
                    .OrderByDescending(x => x.AppointmentDate)
               .ToList()
               .GroupBy(x => $"{x.AppointmentDate:MMM, yyyy}")
               .Select(g => new GroupedData { Key = g.Key, Data = g })
               .ToList();

                return View("GroupingResult", data);
            }
            if (groupby == "count")
            {
                var data = db.Appointments.Include(x => x.Doctor)
                    .OrderByDescending(x => x.AppointmentDate)
               .ToList()
               .GroupBy(x => x.Doctor?.DoctorName)
               .Select(g => new GroupedDataPrimitve<int> { Key = g.Key, Data = g.Count() })
               .ToList();

                return View("GroupingResultPrimitive", data);
            }

            return RedirectToAction("Grouping");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorInputModel model)
        {
            if (ModelState.IsValid)
            {
                var Doctor = new Doctor
                {
                    DoctorName = model.DoctorName,
                    Fees = model.Fees,
                    Specialist = model.Specialist,
                    OnAvilable = model.OnAvilable
                };
                string ext = Path.GetExtension(model.Picture.FileName);
                string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                string savePath = Path.Combine(env.WebRootPath, "Pictures", fileName);
                FileStream fs = new FileStream(savePath, FileMode.Create);
                await model.Picture.CopyToAsync(fs);
                Doctor.Picture = fileName;
                fs.Close();
                db.Database.ExecuteSqlInterpolated($"EXEC InsertDoctor {Doctor.DoctorName}, {Doctor.Fees}, {(int)Doctor.Specialist}, {Doctor.Picture}, {(model.OnAvilable ? 1 : 0)}");
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await db.Doctors.FirstOrDefaultAsync(x => x.DoctorId == id);
            if (data == null) return NotFound();
            return View(new DoctorEditModel
            {
                DoctorId = data.DoctorId,
                DoctorName = data.DoctorName,
                Fees = data.Fees,
                Specialist = data.Specialist ?? Specialist.Medicine,
                OnAvilable = data.OnAvilable ?? false

            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DoctorEditModel model)
        {
            if (ModelState.IsValid)
            {
                var Doctor = await db.Doctors.FirstOrDefaultAsync(x => x.DoctorId == model.DoctorId);
                if (Doctor == null) return NotFound();
                Doctor.DoctorId = model.DoctorId;
                Doctor.DoctorName = model.DoctorName;
                Doctor.Fees = model.Fees;
                Doctor.Specialist = model.Specialist;
                Doctor.OnAvilable = model.OnAvilable;

                if (model.Picture != null)
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(env.WebRootPath, "Pictures", fileName);
                    FileStream fs = new FileStream(savePath, FileMode.Create);
                    await model.Picture.CopyToAsync(fs);
                    Doctor.Picture = fileName;
                    fs.Close();
                }
                db.Database.ExecuteSqlInterpolated($"EXEC UpdateDoctor {Doctor.DoctorId}, {Doctor.DoctorName}, {Doctor.Fees}, {(int)Doctor.Specialist}, {Doctor.Picture}, {(model.OnAvilable ? 1 : 0)}");
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            db.Database.ExecuteSqlInterpolated($"EXEC DeleteDoctor {id}");
            return Json(new { success = true, id });
        }
    }
}
