using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCert.API.Data;
using TestCert.API.DTOs;
using TestCert.API.Models;

namespace TestCert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowTestCertWeb")]
    public class EquipmentController : ControllerBase
    {
        private readonly TestCertContext _context;

        public EquipmentController(TestCertContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentDetailDto>>> GetEquipments()
        {
            var equipmentDetailDtos = await _context.Equipments
                .Include(e => e.Test)
                .Select(x => ToEquipmentDetailDto(x))
                .ToListAsync();
            return Ok(equipmentDetailDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDetailDto>> GetEquipment(int id)
        {
            var equipment = await _context.Equipments
                .Include(e => e.Test)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (equipment == null) 
            { 
                return NotFound(); 
            }
            
            return Ok(ToEquipmentDetailDto(equipment));
        }

        [HttpPost]
        public async Task<ActionResult<Equipment>> CreateEquipment(EquipmentDetailDto equipmentDto)
        {
            var equipment = new Equipment()
            {
                EquipKtId = equipmentDto.EquipKtId,
                EquipPlasticTagId = equipmentDto.EquipPlasticTagId,
                EquipDescription = equipmentDto.EquipDescription,
                EquipDateCreate = equipmentDto.EquipDateCreate,
                EquipLastTest = equipmentDto.EquipLastTest,
                EquipActive = equipmentDto.EquipActive,
                Price = equipmentDto.Price,
                TestId = equipmentDto.TestId
            };

            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEquipment), new { id = equipment.Id }, equipment);
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateEquipment(int id, EquipmentDetailDto equipmentDto)
        {
            var existingEquipment = await _context.Equipments.FindAsync(id);

            if (existingEquipment is null) 
            { 
                return Results.NotFound(); 
            }

            if (existingEquipment.Id != equipmentDto.Id)
            {
                return Results.BadRequest();
            }

            existingEquipment.EquipKtId = equipmentDto.EquipKtId;
            existingEquipment.EquipPlasticTagId = equipmentDto.EquipPlasticTagId;
            existingEquipment.EquipDescription = equipmentDto.EquipDescription;
            existingEquipment.EquipLastTest = equipmentDto.EquipLastTest;
            existingEquipment.EquipActive = equipmentDto.EquipActive;
            existingEquipment.Price = equipmentDto.Price;

            await _context.SaveChangesAsync();
            return Results.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteEquipment(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            
            if (equipment is null)
            {
                return Results.NotFound();
            }

            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();
            return Results.NoContent();
        }

        private static EquipmentDetailDto ToEquipmentDetailDto(Equipment equipment) => new EquipmentDetailDto(
            equipment.Id,
            equipment.EquipKtId,
            equipment.EquipPlasticTagId ?? string.Empty,
            equipment.EquipDescription ?? string.Empty,
            equipment.EquipDateCreate,
            equipment.EquipLastTest,
            equipment.EquipActive,
            equipment.Price,
            equipment.Test?.KtReferenceNumber ?? "N/A",
            equipment.TestId
        );
    }
}
