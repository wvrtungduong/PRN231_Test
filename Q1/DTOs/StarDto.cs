using Q1.Models;

namespace Q1.DTOs
{
    public class StarDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public bool? Male { get; set; }

        public string? Gender => Male.HasValue ? (Male.Value ? "Male" : "Female") : "null";

        public DateTime? Dob { get; set; }

        public string? DobString => Dob?.ToString("M/dd/yyyy");

        public string? Description { get; set; }

        public string? Nationality { get; set; }
    }
}
