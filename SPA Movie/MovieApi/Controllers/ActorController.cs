using System;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Services;
using MovieApi.Domains.RequestModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using MovieApi.Domains.ResponseModels;
using Dapper;
using System.Drawing;

namespace MovieApi.Controllers
{

    [Route("actors/[action]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        readonly Validations v;
        public ActorController(IActorService actorservice) 
        { 
          _actorService = actorservice;
            v = new Validations();
        }    

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseActor>>> GetAllActorsAsync()
        {
            try
            {
                var result = await _actorService.GetAllActorsAsync();
                if (result == null) return NoContent();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseActor>> GetActorByIdAsync(string id)
        {
            // converting string into int ,if conversion is successfull then it returns the converted id or else it will return -1
            var newid = v.InputTypeCheck(id);
            if (newid==-1)
                return BadRequest("invalid Arguments");
                try
                {
                    var result = await _actorService.GetActorByIdAsync(newid);
                    if (result == null) return NotFound("No Content Found");
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
        }
        [HttpPost]
        public async Task<ActionResult<int>> AddActorAsync(RequestActor reqActor)
        {

            if (!v.InputValidate(reqActor.Name) || !v.InputValidate(reqActor.Bio) || !v.InputValidate(reqActor.Gender))
                return BadRequest("invalid Arguments");
                try
            {
                var id = await _actorService.AddActorAsync(reqActor);
                return Created("successfully added",id);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message); 
            }
          }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActorAsync(string id)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1)
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _actorService.GetActorByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                await _actorService.DeleteActorAsync(newid);
                return Ok("deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateActorAsync(string id,RequestActor reqActor)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1|| !v.InputValidate(reqActor.Name) || !v.InputValidate(reqActor.Bio) || !v.InputValidate(reqActor.Gender))
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _actorService.GetActorByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                await _actorService.UpdateActorAsync(newid,reqActor);
                return Ok("updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
