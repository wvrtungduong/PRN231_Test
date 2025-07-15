using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly PePrnFall22B1Context _context;
        public MovieController(PePrnFall22B1Context context)
        {
            _context = context;
        }

        [HttpDelete("RemoveMovieFromGenre/{genreId}/{movieId}")]
        public async Task<IActionResult> RemoveMovieFromGenre(int genreId, int movieId)
        {
            try
            {
                var genre = await _context.Genres.Include(g => g.Movies).FirstOrDefaultAsync(g => g.Id == genreId);
                if (genre == null)
                {
                    return NotFound("The requested genre could not be found.");
                }
                var movie = await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == movieId);

                if (movie == null)
                {
                    return NotFound("The requested movie could not be found in the list of movies of the requested genre.");
                }

                genre.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Conflict();
            }
            return Ok();
        }
    }
}
