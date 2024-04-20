namespace Student_Dashboard_Api.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = "Male";
        public string PhoneNumber { get; set; } = string.Empty;
        public string? PhotoName { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    }
}
