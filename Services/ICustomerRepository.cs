using System;
using TestCert.API.Models;

namespace TestCert.API.Services;

public interface ICustomerRepository
{
 Task<Customer?> GetByIdAsync(int id);
}
