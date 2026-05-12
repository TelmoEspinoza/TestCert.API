using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCert.API.Data;
using TestCert.API.DTOs;
using TestCert.API.Exceptions;
using TestCert.API.Models;
using TestCert.API.Services;

namespace TestCert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowTestCertWeb")]
    [Authorize]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentsController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestDto>>> GetTests()
        {
            //var testDetailDto = await _context.Tests.Select(x => ToTestDto(x)).ToListAsync();
            //return Ok(testDetailDto);
            try
            {
                var equipments = await _equipmentService.GetEquipmentAsync();
                if (equipments == null)
                    return NotFound();
                return Ok(equipments);
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
        [HttpGet("{id}")]
        [Authorize(Policy ="TestCertAPI.Read")]
        public async Task<ActionResult<EquipmentDto>> GetEquipment(int id)
        {
            try
            {
                var equipmentDto = await _equipmentService.GetEquipmentByIdAsync(id);// _context.Tests.FindAsync(id);
                if (equipmentDto == null) { return NotFound(); }
                else return Ok(equipmentDto);
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }


        }
        [HttpPost]
        public async Task<IActionResult> CreateEquipmentAsync([FromBody] EquipmentDto equipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _equipmentService.CreateEquipmentAsync(equipmentDto);

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }


        }
         [HttpPut]
        public async Task<IActionResult> UpdateEquipmentAsync([FromBody] EquipmentDto equipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _equipmentService.UpdateEquipmentAsync(equipmentDto);

                return Ok(NoContent());
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }


        }
         [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipmentAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
               await _equipmentService.DeleteEquipmentAsync(id);

                return Ok(NoContent());
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }


        }

    }
}
