using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using MovieApi.Domains.Models;
using MovieApi.Services;
using MovieApi.Domains.RequestModels;
using System;
using System.Numerics;
using System.Threading.Tasks;
using MovieApi.Domains.ResponseModels;
using System.Collections;
using System.Collections.Generic;

namespace MovieApi.Controllers
{
    [Route("producers/[action]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerService _ProducerService;
        readonly Validations v;
        public ProducerController(IProducerService producerservice)
        {
            _ProducerService = producerservice;
            v = new Validations();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseProducer>>> GetAllProducersAsync()
        {
            try
            {
                var result = await _ProducerService.GetAllProducersAsync();
                if (result == null) return NoContent();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseProducer>> GetProducerByIdAsync(string id)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1)
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _ProducerService.GetProducerByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddProducerAsync(RequestProducer reqProducer)
        {
            try
            {
                if (!v.InputValidate(reqProducer.Name) || !v.InputValidate(reqProducer.Bio) || !v.InputValidate(reqProducer.Gender))
                    return BadRequest("invalid Arguments");
                var id= await _ProducerService.AddProducerAsync(reqProducer);
                return Created("successfully added", id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducerAsync(string id)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1)
                return BadRequest("invalid Arguments");
            try
            {
                var result = await _ProducerService.GetProducerByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                await _ProducerService.DeleteProducerAsync(newid);
                return Ok("deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProducerAsync(string id, RequestProducer reqProducer)
        {
            var newid = v.InputTypeCheck(id);
            if (newid == -1|| !v.InputValidate(reqProducer.Name) || !v.InputValidate(reqProducer.Bio) || !v.InputValidate(reqProducer.Gender))
                return BadRequest("invalid Arguments");       
            try
            {
                var result = await _ProducerService.GetProducerByIdAsync(newid);
                if (result == null) return NotFound("No Content Found");
                await _ProducerService.UpdateProducerAsync(newid, reqProducer);
                return Ok("updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
