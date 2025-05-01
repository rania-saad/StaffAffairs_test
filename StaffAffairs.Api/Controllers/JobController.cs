using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffAffairs.Core.DTOs;
using StaffAffairs.Core.Interfaces;
using StaffAffairs.Core.Models;
using StaffAffairs.Core.Services;
using StaffAffairs.Infrastructure.Data;

namespace StaffAffairs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {

        private readonly IJobService _service;

        public JobController(IJobService service)
        {
            _service = service;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetAll()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTO>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<JobDTO>> CreateJob([FromBody] JobDTO JobDto)
        {
            try
            {
                var createdJob = await _service.CreateJobAsync(JobDto);
                return CreatedAtAction(nameof(GetById), new { id = createdJob.Id }, createdJob);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] updateJobDTO Job)
        {
            try
            {
                if (id != Job.Id)
                    return BadRequest("ID mismatch");

                await _service.UpdateAsync(Job);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("exact/{name}")]
        public async Task<ActionResult<JobDTO>> GetExact(string name)
        {
            try
            {
                var result = await _service.GetExactAsync(name);
                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<JobDTO>>> Search(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("Search term cannot be empty");

                var result = await _service.SearchAsync(name);
                return result != null && result.Any() ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
