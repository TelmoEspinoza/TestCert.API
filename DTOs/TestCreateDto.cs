using System;
using System.ComponentModel.DataAnnotations;

namespace TestCert.API.DTOs;

public class TestCreateDto
{
    [Required(ErrorMessage = "CustomerId is required.")]
    public string? KtReferenceNumber { get; set; }
    public string? Description { get; set; }

    [Required(ErrorMessage = "CustomerId is required.")]
    public int CustomerId { get; set; }
    [Required(ErrorMessage = "Order must contain at least one item.")]
    [MinLength(1, ErrorMessage = "Order must have at least one item.")]
    public List<TestItemDto> Items { get; set; } = new();

}
