using System.ComponentModel.DataAnnotations;
namespace TestCert.API.DTOs;

public class TestDto
{
  public int TestId { get; set; }
  public string? KtReferenceNumber { get; set; }
  public string? Description { get; set; }
  public decimal Total { get; set; }
  //public List<TestItemDto> Items { get; set; } = new();
}
