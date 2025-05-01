

using Microsoft.AspNetCore.Mvc;
using StaffAffairs.Core.DTOs;
using StaffAffairs.Core.Interfaces;
using StaffAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffAffairs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypeController : ControllerBase
    {
        private readonly IJobTypeService _service;

        public JobTypeController(IJobTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTypeDTO>>> GetAll()
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
        public async Task<ActionResult<JobType>> GetById(int id)
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
        public async Task<ActionResult<JobType>> Create([FromBody] JobTypeDTO JobType)
        {
            try
            {
                if (JobType == null)
                    return BadRequest("JobType object is null");

                var result = await _service.CreateAsync(JobType);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] JobType JobType)
        //{
        //    try
        //    {
        //        if (id != JobType.Id)
        //            return BadRequest("ID mismatch");

        //        await _service.UpdateAsync(JobType);
        //        return NoContent();
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

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

        [HttpGet("exact/{name}")]
        public async Task<ActionResult<JobType>> GetExact(string name)
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
        public async Task<ActionResult<IEnumerable<JobType>>> Search(string name)
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
    }
}