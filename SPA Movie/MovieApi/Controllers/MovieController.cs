using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Domains.RequestModels;
using MovieApi.Services;
using System;
using MovieApi.Domains.ResponseModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Storage;

namespace MovieApi.Controllers
{
    [Route("movies/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _MovieService;
        readonly Validations v;
        public MovieController(IMovieService movieservice)
        {
            _MovieService = movieservice;
            v = new Validations();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseMovie>> GetMoviesByIdAsync(string id)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1)
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _MovieService.GetMovieByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseMovie>>> GetAllMoviesAsync()
        {
            try
            {
                var result = await _MovieService.GetAllMoviesAsync();
                if (result == null) return NotFound("No Content Found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddMovieAsync(RequestMovie reqMovie)
        {
            if (!v.InputValidate(reqMovie.Name) || !v.InputValidate(reqMovie.Plot) || !v.InputValidate(reqMovie.CoverImage) ||
                !v.yearCheck(reqMovie.YearOfRelease) || !v.IdsCheck(reqMovie.ActorIds) || !v.IdsCheck(reqMovie.GenreIds))
                return BadRequest("invalid Arguments");
            
            try
            {
                var id = await _MovieService.AddMovieAsync(reqMovie);
                if (id == -1)
                    return BadRequest("Actorids/generids cannot be found , enter valid data");
                if (id == -2)
                    return BadRequest("Producer id couldn't be found");
                return Created("successfully added", id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovieAsync(string id)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1)
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _MovieService.GetMovieByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                await _MovieService.DeleteMovieAsync(newid);
                return Ok("deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovieAsync(string id, RequestMovie reqMovie)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1 || !v.InputValidate(reqMovie.Name) || !v.InputValidate(reqMovie.Plot) || !v.InputValidate(reqMovie.CoverImage) ||
                !v.yearCheck(reqMovie.YearOfRelease) || !v.IdsCheck(reqMovie.ActorIds) || !v.IdsCheck(reqMovie.GenreIds))
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _MovieService.GetMovieByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                var ids = await _MovieService.UpdateMovieAsync(newid, reqMovie);
                if (ids == -1)
                    return BadRequest("Actorids/generids cannot be found , enter valid data");
                if (ids == -2)
                    return BadRequest("Producer id couldn't be found");
                return Ok("updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("upload")]
        
         public async Task<IActionResult> UploadFile(IFormFile file)
          {
            try
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");
                var task = new FirebaseStorage("imdbapp-96886.appspot.com")
                        .Child("movies_poster")
                        .Child(Guid.NewGuid().ToString() + ".jpg")
                        .PutAsync(file.OpenReadStream());
                var url = await task;
                return Ok(url);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         }
    }
}
