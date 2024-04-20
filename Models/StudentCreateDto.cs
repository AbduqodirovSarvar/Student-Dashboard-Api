namespace Student_Dashboard_Api.Models
{
    public class StudentCreateDto
    {
        public string FullName { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public IFormFile? Photo { get; set; }
    }
}
