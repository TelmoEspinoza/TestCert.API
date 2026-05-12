using System;
using Microsoft.AspNetCore.Mvc;
using TestCert.API.DTOs;
using TestCert.API.Exceptions;
using TestCert.API.Models;
using TestCert.API.Repositories;

namespace TestCert.API.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly ICustomerRepository _customerRepository;

    private readonly IEquipmentRepository _equipmentRepository;

    public TestService(ITestRepository testRepository, ICustomerRepository customerRepository, IEquipmentRepository equipmentRepository)
    {
        _testRepository = testRepository;
        _customerRepository = customerRepository;
        _equipmentRepository = equipmentRepository;
    }

    public async Task<IEnumerable<TestDto>> GetTestsAsync()
    {
        // var listTest = await _testRepository.GetTestsAsync();
        var testsDto = await _testRepository.GetTestsAsync() ?? throw new Exception("No Tests found !!");//x => ToTestDto(x)).ToListAsync();

        return testsDto.Value?.Select(x => ToTestDto(x)).ToList() ?? [];
    }

     public async Task<TestDto?> GetTestByIdAsync(int id)
    {
       var test = await _testRepository.GetTestByIdAsync(id);
       return test is null ? null : ToTestDto(test);

    }
    public async Task<TestResponseDto?> CreateTestAsync(TestCreateDto testCreateDto)
    {
        var customer = await _customerRepository.GetByIdAsync(testCreateDto.CustomerId);
            if (customer == null)
                throw new NotFoundException($"Customer with id {testCreateDto.CustomerId} not found.");
            if (testCreateDto.Items == null || !testCreateDto.Items.Any())
                throw new ArgumentException("Order must have at least one item.");
            try
            {
                var test = new Test
                {
                    CustomerId = customer.CustomerId,
                    Description  = testCreateDto.Description,
                    KtReferenceNumber = testCreateDto.KtReferenceNumber,
                    TestItems = new List<TestItem>()
                };
                decimal baseAmount = 0;
                foreach (var itemDto in testCreateDto.Items)
                {
                    var equipment = await _equipmentRepository.GetEquipmentByIdAsync(itemDto.EquipmentId);
                    if (equipment == null)
                        throw new NotFoundException($"Equipment with id {itemDto.EquipmentId} not found.");
                    if (itemDto.Quantity <= 0)
                        throw new ArgumentException("Quantity must be greater than zero.");
                    if (equipment.Stock < itemDto.Quantity)
                        throw new InvalidOperationException($"Not enough stock for product {equipment.EquipKtId}. Available: {equipment.Stock}, requested: {itemDto.Quantity}");
                    decimal lineTotal = equipment.Price * itemDto.Quantity;
                    test.TestItems.Add(new TestItem
                    {
                        EquipmentId = equipment.Id,
                        Quantity = itemDto.Quantity,
                        UnitPrice = equipment.Price,
                        LineTotal = lineTotal
                    });
                    baseAmount += lineTotal;
                    // Deduct stock
                    equipment.Stock -= itemDto.Quantity;
                }
               // order.BaseAmount = baseAmount;
                // Calculate discount at order level
               // order.DiscountAmount = CalculateDiscount(baseAmount);
                //order.TotalAmount = baseAmount - order.DiscountAmount;
                test.Total = baseAmount;
                await _testRepository.AddTestAsync(test);
                await _equipmentRepository.SaveChangesEquipmentAsync();
                await _testRepository.SaveChangesTestAsync();
                return ToTestResponseDto(test, customer);
            }
            catch
            {
                throw; // Let upper layer handle exceptions
            }
    }

    public Task DeleteTestAsync(int id)
    {
        throw new NotImplementedException();
    }

   

    public Task UpdateTestAsync(TestDto testDto)
    {
        throw new NotImplementedException();
    }

    private TestDto ToTestDto(Test test)
    {
        return new TestDto
        {
            TestId = test.Id,
            KtReferenceNumber = test.KtReferenceNumber,
            Description = test.Description,
            Total = test.Total
        };
    }

    private TestResponseDto ToTestResponseDto(Test test, Customer? customer)
    {
        return new TestResponseDto
        {
            TestId = test.Id,
                CustomerId = test.CustomerId,
                CustomerName = customer?.Name ?? string.Empty,
                CustomerEmail = customer?.Email ?? string.Empty,
                KtReferenceNumber = test.KtReferenceNumber,
                Description = test.Description,
                TotalAmount = test.Total,
                TestItems = test.TestItems.Select(item => new TestItemResponseDto
                {
                    TestItemId = item.TestItemId,
                    EquipmentId = item.EquipmentId,
                    EquipKtId = item.Equipment?.EquipKtId ?? string.Empty,
                    EquipDescription = item.Equipment?.EquipDescription ?? string.Empty,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    LineTotal = item.LineTotal
                }).ToList()
            
        };
    }

  
}
