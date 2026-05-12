using System;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using TestCert.API.Models;

namespace TestCert.API.Data;

public class TestCertContext : DbContext
{
    public TestCertContext()
    { }

    public TestCertContext(DbContextOptions<TestCertContext> options) : base(options)
    { }
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Equipment> Equipments => Set<Equipment>();

    public DbSet<Customer> Customers => Set<Customer>();
}
