using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.DTOs;
using Q1.Models;
using System.Reflection;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarController : ControllerBase
    {
        private readonly PePrnFall22B1Context _context;
        public StarController(PePrnFall22B1Context context)
        {
            _context = context;
        }

        [HttpGet("GetStars/{nationality}/{gender}")]
        public async Task<ActionResult<List<Star>>> GetStars(string nationality, string gender)
        {
            bool? isMale = gender.ToLower() == "male" ? true : gender.ToLower() == "female" ? false : null;
            var stars = await _context.Stars.Where(s => s.Nationality == nationality && s.Male == isMale).Select(s => new StarDto {
                Id = s.Id,
                FullName = s.FullName,
                Male = s.Male,
                Dob = s.Dob,
                Description = s.Description,
                Nationality = s.Nationality
                }).ToListAsync();
            return Ok(stars);
        }

        [HttpGet("GetStar/{id}")]
        public async Task<ActionResult<StarDetailDto>> GetStar(int id)
        {
            var star = await _context.Stars.Include(s => s.Movies).FirstOrDefaultAsync(s => s.Id == id);
            if (star == null)
            {
                return NotFound();
            }
            else
            {
                var starDetail = new StarDetailDto
                {
                    Id = star.Id,
                    FullName = star.FullName,
                    Male = star.Male,
                    Dob = star.Dob,
                    Nationality = star.Nationality,
                    Description = star.Description,
                    Movies = star.Movies.Select(m => new MovieInStarDto
                    {
                        Id = m.Id,
                        Title = m.Title,
                        ReleaseDate = m.ReleaseDate,
                        Description = m.Description,
                        Language = m.Language,
                        ProducerId = m.ProducerId,
                        DirectorId = m.DirectorId,
                        ProducerName = null,
                        DirectorName = null,
                        Genres = new List<string>(),
                        Stars = new List<string>()
                    }).ToList()
                };

                return Ok(starDetail);
            }
        }
    }
}
