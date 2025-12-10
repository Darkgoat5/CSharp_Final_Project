using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTracker.WebAPI.Models;
using MovieTracker.WebAPI.Repository;

namespace MovieTracker.WebAPI.Controllers
{
    // endpoint URL/api/MovieTrackerAPI 
    [Route("api/[controller]")]
    [ApiController]
    public class MovieTrackerAPIController : ControllerBase
    {
        private readonly IMovieRepository _repo;

        public MovieTrackerAPIController(IMovieRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()          
        {
            var movies = await _repo.GetAllMoviesAsync();
            return Ok(movies);
        }

        // never actually gets used, just used for testing the API seperate
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await _repo.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            await _repo.DeleteMovieAsync(id);
            // returns 204 status code
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie([FromBody] Movie movie)
        {
            await _repo.AddMovieAsync(movie);
            // returns 201 status code with location header, this is returned to follow REST convention
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, [FromBody] Movie updatedMovie)
        {
            await _repo.UpdateMovieAsync(id, updatedMovie);
            // returns 204 status code
            return NoContent();
        }
    }
}
