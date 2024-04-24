using Student_Dashboard_Api.Data.Entities;

namespace Student_Dashboard_Api.Models
{
    public class FilteredStudentResponse(int count, ICollection<Student> students)
    {
        public int Count { get; set; } = count;
        public ICollection<Student> Students { get; set; } = students;
    }
}
