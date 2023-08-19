using Microsoft.AspNetCore.Mvc;
using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using MovieApi.Services;
using System;
using System.Threading.Tasks;

namespace MovieApi.Controllers
{
    [Route("genres/[action]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _GenreService;
        readonly Validations v;
        public GenreController(IGenreService genreservice)
        {
            _GenreService = genreservice;
             v = new Validations();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenreAsync()
        {
            try
            {
                var result = await _GenreService.GetAllGenresAsync();
                if (result == null) return NoContent();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseGenre>> GetGenreByIdAsync(string id)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1)
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _GenreService.GetGenreByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<int>> AddGenreAsync(RequestGenre reqGenre)
        {
            if (!v.InputValidate(reqGenre.Name))
                return BadRequest("invalid Arguments");
            try
            {
                var id = await _GenreService.AddGenreAsync(reqGenre);
                return Created("successfully added", id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenreAsync(string id)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1)
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _GenreService.GetGenreByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                await _GenreService.DeleteGenreAsync(newid);
                return Ok("deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGenreAsync(string id,RequestGenre reqGenre)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1 || !v.InputValidate(reqGenre.Name))
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _GenreService.GetGenreByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                await _GenreService.UpdateGenreAsync(newid, reqGenre);
                return Ok("updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
