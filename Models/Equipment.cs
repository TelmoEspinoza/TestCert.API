
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCert.API.Models;

public partial class Equipment
{
    [Key]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(10)")]
    public required string EquipKtId { get; set; }
    [Column(TypeName = "nvarchar(10)")]
    public string? EquipPlasticTagId { get; set; }
    [Column(TypeName = "nvarchar(200)")]
    public string? EquipDescription { get; set; }
    [Required]
    public DateTime EquipDateCreate { get; set; } = DateTime.UtcNow;

    public DateOnly EquipLastTest { get; set; }
    [Required]
    public Boolean EquipActive { get; set; } = true;
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public required decimal Price { get; set; }

    public Test? Test { get; set; }

    public int TestId { get; set; }


}
