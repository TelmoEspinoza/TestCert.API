using System;
using TestCert.API.Data;
using TestCert.API.Models;

namespace TestCert.API.Services;

public class CustomerRepository : ICustomerRepository
{
     private readonly TestCertContext _dbContext;
    public CustomerRepository(TestCertContext dbContext) { _dbContext = dbContext; }
    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _dbContext.Customers.FindAsync(id);
    }
}
