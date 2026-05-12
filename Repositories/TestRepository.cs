using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCert.API.Data;
using TestCert.API.Models;

namespace TestCert.API.Repositories;

public class TestRepository : ITestRepository
{
    private readonly TestCertContext _context;
    public TestRepository(TestCertContext context)
    {
        _context = context;
    }

    public async Task<Test?> GetTestByIdAsync(int id)
    {
        //throw new NotImplementedException();
        return await _context.Tests.FindAsync(id);
    }

    public async Task<ActionResult<IEnumerable<Test>>> GetTestsAsync()
    {
        return await _context.Tests.ToListAsync();
    }
    public async Task AddTestAsync(Test test)
    {
        await _context.Tests.AddAsync(test);
    }


    public async Task UpdateTestAsync(Test test)
    {
        _context.Tests.Update(test);
    }

    public async Task DeleteTestAsync(Test test)
    {
        _context.Tests.Remove(test);
    }
    public async Task SaveChangesTestAsync()
    {
        await _context.SaveChangesAsync();
    }
}
