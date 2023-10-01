namespace _9th_Monthly_Exam.ViewModels
{
    public class GroupedDataPrimitve<T>
    {
        public string Key { get; set; } = default!;
        public T Data { get; set; } = default!;
    }
}
