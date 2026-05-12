using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Mvc;

using TestCert.API.DTOs;
using TestCert.API.Exceptions;
using TestCert.API.Services;

namespace TestCert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowTestCertWeb")]
    [Authorize]
    [Authorize(Policy ="TestCertAPI.Read")]
    public class testsController : ControllerBase
    {

        private readonly ITestService _testService;

        public testsController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestDto>>> GetTests()
        {
            //var testDetailDto = await _context.Tests.Select(x => ToTestDto(x)).ToListAsync();
            //return Ok(testDetailDto);
            try
            {
                var tests = await _testService.GetTestsAsync();
                if (tests == null)
                    return NotFound();
                return Ok(tests);
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestDto>> GetTest(int id)
        {
            try
            {
                var testDetailDto = await _testService.GetTestByIdAsync(id);// _context.Tests.FindAsync(id);
                if (testDetailDto == null) { return NotFound(); }
                else return Ok(testDetailDto);
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }

        }
        /*
                [HttpGet("{id}/equipments")]
                public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetEquipmentsByTestId(int id)
                {
                    var equipmentDetailDtos = await _context.Equipments.Where(c => c.TestId == id).ToListAsync();

                    return Ok(equipmentDetailDtos);
                }
                */


        [HttpPost]
        public async Task<ActionResult<TestResponseDto>> CreateTest([FromBody] TestCreateDto testDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var order = await _testService.CreateTestAsync(testDto);
                return Ok(order);
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
        /*

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
                */
    }

}
