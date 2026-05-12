using System;
using Microsoft.AspNetCore.Mvc;
using TestCert.API.Models;

namespace TestCert.API.Repositories;

public interface IEquipmentRepository
{
Task<ActionResult<IEnumerable<Equipment>>> GetEquipmentsAsync();

Task<Equipment?> GetEquipmentByIdAsync(int id);

Task AddEquipmentAsync(Equipment equipment);

Task UpdateEquipmentAsync (Equipment equipment);

Task DeleteEquipmentAsync (Equipment equipment);

Task SaveChangesEquipmentAsync();
}
