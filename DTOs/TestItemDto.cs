using System;
using System.ComponentModel.DataAnnotations;

namespace TestCert.API.DTOs;

public class TestItemDto
{
        [Required(ErrorMessage = "EquipmentId is required.")]
        public int EquipmentId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
}
