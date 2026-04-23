using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCert.API.Models;

public partial class Test
{
public int Id{ get; set; }

public string? KtReferenceNumber{ get; set; }

public string? Description{ get; set; }
 [Column(TypeName = "decimal(18,2)")]
public decimal Total { get; set; }

public virtual ICollection<Models.Equipment> TestEquipments { get; set; } = new List<Equipment>();
}
