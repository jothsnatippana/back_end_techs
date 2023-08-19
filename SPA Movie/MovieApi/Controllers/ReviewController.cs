using Microsoft.AspNetCore.Mvc;
using System;
using MovieApi.Services;
using MovieApi.Domains.RequestModels;
using System.Threading.Tasks;
using MovieApi.Domains.ResponseModels;
using System.Collections.Generic;

namespace MovieApi.Controllers
{
    [Route("movies/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _ReviewService;
        readonly Validations v;
        public ReviewController(IReviewService service)
        {
            _ReviewService = service;
            v = new Validations();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseReview>>> GetAllReviewAsync()
        {
            try
            {
                var result = await _ReviewService.GetAllReviewsAsync();
                if (result == null) return NoContent();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("movies/{movieid}/reviews/")]

        public async Task<ActionResult<ResponseReview>> GetReviewByIdAsync(string movieid)
        {
            var newmovieid = v.InputTypeCheck(movieid);
            if (newmovieid == -1)
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _ReviewService.GetReviewByIdAsync(newmovieid);
                if (result == null) return NotFound("No Content Found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("movies/{movieid}/reviews")]
        public async Task<ActionResult<int>> AddReviewAsync(RequestReview reqReview,string movieid)
        {
            var newmovieid = v.InputTypeCheck(movieid);
            if (newmovieid == -1)
                return BadRequest("invalid Arguments");
            if (!v.InputValidate(reqReview.Message))
                return BadRequest("invalid Arguments");
            try
            {
                var id=await _ReviewService.AddReviewAsync(reqReview, newmovieid);
                return Created("successfully added", id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("movies/{movieid}/reviews/{id}")]
        public async Task<ActionResult> DeleteReviewAsync(string id)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1)
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _ReviewService.GetReviewByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                await _ReviewService.DeleteReviewAsync(newid);
                return Ok("deleted successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReviewAsync(string id, RequestReview reqReview,string movieid)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1)
                return BadRequest("invalid Arguments");
            var newmovieid = v.InputTypeCheck(movieid);
            if (newmovieid == -1)
                return BadRequest("invalid Arguments");
            if (!v.InputValidate(reqReview.Message))
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _ReviewService.GetReviewByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                await _ReviewService.UpdateReviewAsync(newid, reqReview, newmovieid);
                return Ok("updated successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
