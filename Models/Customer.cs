using System;
using System.ComponentModel.DataAnnotations;

namespace TestCert.API.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public string Name { get; set; } = null!;  // Full name
    public string Email { get; set; } = null!;  // Unique email address

    public List<Test> Tests { get; set; } = new();
}
