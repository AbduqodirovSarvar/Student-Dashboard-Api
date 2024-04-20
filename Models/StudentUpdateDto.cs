using System.ComponentModel.DataAnnotations;

namespace Student_Dashboard_Api.Models
{
    public class StudentUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public string? FullName { get; set; } = null;
        public int? Age { get; set; } = null;
        public string? Gender { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        public IFormFile? Photo { get; set; } = null;
    }
}
