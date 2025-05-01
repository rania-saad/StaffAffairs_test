

using Microsoft.AspNetCore.Mvc;
using StaffAffairs.Core.Interfaces;
using StaffAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffAffairs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkStatusController : ControllerBase
    {
        private readonly IWorkStatusService _service;

        public WorkStatusController(IWorkStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkStatus>>> GetAll()
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
        public async Task<ActionResult<WorkStatus>> GetById(int id)
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
        public async Task<ActionResult<WorkStatus>> Create([FromBody] WorkStatus WorkStatus)
        {
            try
            {
                if (WorkStatus == null)
                    return BadRequest("WorkStatus object is null");

                var result = await _service.CreateAsync(WorkStatus);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkStatus WorkStatus)
        {
            try
            {
                if (id != WorkStatus.Id)
                    return BadRequest("ID mismatch");

                await _service.UpdateAsync(WorkStatus);
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
        public async Task<ActionResult<WorkStatus>> GetExact(string name)
        {
            try
            {
                var result = await _service.GetExactWorkStatusAsync(name);
                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<WorkStatus>>> Search(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("Search term cannot be empty");

                var result = await _service.SearchNationalitiesAsync(name);
                return result != null && result.Any() ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}