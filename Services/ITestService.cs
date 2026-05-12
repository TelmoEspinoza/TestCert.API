using System;
using Microsoft.AspNetCore.Mvc;
using TestCert.API.DTOs;
using TestCert.API.Models;

namespace TestCert.API.Services;

public interface ITestService
{
    Task<IEnumerable<TestDto>> GetTestsAsync();

    Task<TestDto?> GetTestByIdAsync(int id);

    Task<TestResponseDto?> CreateTestAsync(TestCreateDto testDto);

    Task UpdateTestAsync(TestDto testDto);

    Task DeleteTestAsync(int id);

}
