using System;

namespace TestCert.API.DTOs;

public class TestItemResponseDto
{
    public int TestItemId { get; set; }
    public int EquipmentId { get; set; }
    public string EquipKtId { get; set; } = string.Empty;
    public string? EquipDescription { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }

}
