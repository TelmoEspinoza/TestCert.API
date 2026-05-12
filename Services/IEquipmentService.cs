using System;
using Microsoft.AspNetCore.Mvc;
using TestCert.API.DTOs;

namespace TestCert.API.Services;

public interface IEquipmentService
{
    Task<ActionResult<IEnumerable<EquipmentDto>>> GetEquipmentAsync();

    Task<EquipmentDto?> GetEquipmentByIdAsync(int id);

    Task<EquipmentDto?> CreateEquipmentAsync(EquipmentDto equipmentDto);

    Task UpdateEquipmentAsync(EquipmentDto equipmentDto);

    Task DeleteEquipmentAsync(int id);
}
