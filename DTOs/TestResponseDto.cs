using System;

namespace TestCert.API.DTOs;

public class TestResponseDto
{
        public int TestId { get; set; }
        public int CustomerId { get; set; }
        public string? KtReferenceNumber { get; set; }
        public string? Description { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public DateTime TestDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<TestItemResponseDto> TestItems { get; set; } = new();
}
