using System.ComponentModel.DataAnnotations;

namespace TestCert.API.DTOs;

public record EquipmentDetailDto
(
  int Id,
    [Required] string EquipKtId,
    [StringLength(50)] string EquipPlasticTagId,
    [StringLength(250)] string EquipDescription,
    DateTime EquipDateCreate,
    DateOnly EquipLastTest,
    Boolean EquipActive,
    decimal Price,
    string TestNumber,
    int TestId
);
