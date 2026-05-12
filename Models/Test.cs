using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCert.API.Models;

public partial class Test
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public string? KtReferenceNumber { get; set; }
    public string? Description { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    public List<TestItem> TestItems { get; set; } = new();

}
