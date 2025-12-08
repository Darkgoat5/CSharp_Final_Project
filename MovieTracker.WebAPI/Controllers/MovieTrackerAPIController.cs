using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTracker.WebAPI.Models;
using MovieTracker.WebAPI.Repository;

namespace MovieTracker.WebAPI.Controllers
{
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
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie([FromBody] Movie movie)
        {
            await _repo.AddMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, [FromBody] Movie updatedMovie)
        {
            await _repo.UpdateMovieAsync(id, updatedMovie);
            return NoContent();
        }
    }
}
