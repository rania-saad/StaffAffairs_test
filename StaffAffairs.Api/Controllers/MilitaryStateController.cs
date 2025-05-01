

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
    public class MilitaryStateController : ControllerBase
    {
        private readonly IMilitaryStateService _service;

        public MilitaryStateController(IMilitaryStateService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MilitaryState>>> GetAll()
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
        public async Task<ActionResult<MilitaryState>> GetById(int id)
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
        public async Task<ActionResult<MilitaryState>> Create([FromBody] MilitaryState MilitaryState)
        {
            try
            {
                if (MilitaryState == null)
                    return BadRequest("MilitaryState object is null");

                var result = await _service.CreateAsync(MilitaryState);
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
        public async Task<IActionResult> Update(int id, [FromBody] MilitaryState MilitaryState)
        {
            try
            {
                if (id != MilitaryState.Id)
                    return BadRequest("ID mismatch");

                await _service.UpdateAsync(MilitaryState);
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
        public async Task<ActionResult<MilitaryState>> GetExact(string name)
        {
            try
            {
                var result = await _service.GetExactMilitaryStateAsync(name);
                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<MilitaryState>>> Search(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("Search term cannot be empty");

                var result = await _service.SearchMilitaryStateAsync(name);
                return result != null && result.Any() ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}