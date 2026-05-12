using System;
using Microsoft.AspNetCore.Mvc;
using TestCert.API.Models;

namespace TestCert.API.Repositories;

public interface ITestRepository
{
    Task<ActionResult<IEnumerable<Test>>> GetTestsAsync();
    Task<Test?> GetTestByIdAsync(int id);
    Task AddTestAsync(Test test);
    Task UpdateTestAsync(Test test);
    Task DeleteTestAsync(Test test);
    Task SaveChangesTestAsync();
}
