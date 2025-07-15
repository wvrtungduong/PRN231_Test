using Q1.Models;

namespace Q1.DTOs
{
    public class MovieInStarDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public DateTime? ReleaseDate { get; set; }

        public int? ReleaseYear => ReleaseDate?.Year;

        public string? Description { get; set; }

        public string Language { get; set; } = null!;

        public int? ProducerId { get; set; }

        public int? DirectorId { get; set; }

        public string? ProducerName { get; set; } = null;

        public string? DirectorName { get; set; } = null;

        public List<string> Genres { get; set; } = new();

        public List<string> Stars { get; set; } = new();

    }
}
