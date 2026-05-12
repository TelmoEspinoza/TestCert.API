using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCert.API.Models;

public class TestItem
{
    public int TestItemId { get; set; }
    public int TestId { get; set; }
    public Test? Test { get; set; }
    public int EquipmentId { get; set; }
    public Equipment? Equipment { get; set; }
    public int Quantity { get; set; } // Units ordered
    [Column(TypeName = "decimal(12,2)")]
    public decimal UnitPrice { get; set; } // Price at time of order
    [Column(TypeName = "decimal(12,2)")]
    public decimal LineTotal { get; set; }  // Calculated: UnitPrice * Quantity

}
