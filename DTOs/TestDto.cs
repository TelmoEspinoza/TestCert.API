using System.ComponentModel.DataAnnotations;
namespace TestCert.API.DTOs;

public record TestDto
(
  int TestId,
  string? KtReferenceNumber,
  string? Description,
  [Required] decimal Total
);
