using System.Text.Json.Serialization;

namespace Q1.DTOs
{
    public class StarDetailDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;
        
        [JsonIgnore]
        public bool? Male { get; set; }

        public string? Gender => Male.HasValue ? (Male.Value ? "Male" : "Female") : "null";

        public DateTime? Dob { get; set; }

        public string? DobString => Dob?.ToString("M/dd/yyyy");

        public string? Nationality { get; set; }

        public string? Description { get; set; }

        public List<MovieInStarDto> Movies { get; set; } = new();
    }
}
