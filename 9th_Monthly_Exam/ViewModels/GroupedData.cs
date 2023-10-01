using _9th_Monthly_Exam.Models;

namespace _9th_Monthly_Exam.ViewModels
{
    public class GroupedData
    {
        public string Key { get; set; } = default!;
        public IEnumerable<Appointment> Data { get; set; } = default!;
    }
}
