using System.ComponentModel.DataAnnotations;

namespace TestCert.API.DTOs;

public class EquipmentDto
{
  [Required(ErrorMessage = "EquipmentId is required.")]
  public int Id { get; set; }
  [Required(ErrorMessage = "EquipKtId is required.")]
  public string? EquipKtId { get; set; }
  public string? EquipPlasticTagId { get; set; }
  public string? EquipDescription { get; set; }
  public DateTime EquipDateCreate { get; set; }
  public DateOnly EquipLastTest { get; set; }
  public Boolean EquipActive { get; set; }
  public decimal Price { get; set; }
  public int Stock { get; set; } // Available stock for orders
}
