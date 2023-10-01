using _9th_Monthly_Exam.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace _9th_Monthly_Exam.ViewModels.Input
{
    public class DoctorInputModel
    {
        public int DoctorId { get; set; }
        [Required, StringLength(50)]
        public string DoctorName { get; set; } = default!;
        [Required, EnumDataType(typeof(Specialist))]
        public Specialist Specialist { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal Fees { get; set; }
        [Required]
        public IFormFile Picture { get; set; } = default!;

        public bool OnAvilable { get; set; }
    }
}
