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
    public class TestController : ControllerBase
    {
        private readonly TestCertContext _context;

        public TestController(TestCertContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestDto>>> GetTests()
        {
            var testDetailDto = await _context.Tests.Select(x => ToTestDto(x)).ToListAsync();
            return Ok(testDetailDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(int id)
        {
            var testDetailDto = await _context.Tests.FindAsync(id);
            if (testDetailDto == null) { return NotFound(); }
            else return Ok(testDetailDto);
        }
        [HttpPost]
        public async Task<ActionResult<Test>> CreateTest(TestDto testDto)
        {
            Test test = new()
            {
                KtReferenceNumber = testDto.KtReferenceNumber,
                Description = testDto.Description,
                Total = testDto.Total
            };
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTest), new { id = test.Id }, test);
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateTest(int id, TestDto testDto)
        {
            var existingTest = await _context.Tests.FindAsync(id);

            if (existingTest is null) { return Results.NotFound(); }

            if (existingTest.Id != testDto.TestId)
            {
                return Results.BadRequest();
            }
            existingTest.KtReferenceNumber = testDto.KtReferenceNumber;
            existingTest.Description = testDto.Description;
            existingTest.Total = testDto.Total;
            await _context.SaveChangesAsync();
            return Results.NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test is null)
            {
                return Results.NotFound();
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return Results.NoContent();
        }

        private static TestDto ToTestDto(Test test) => new TestDto(
            test.Id,
            test.KtReferenceNumber,
            test.Description,
            test.Total
        );
    }
}
