using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCert.API.Data;
using TestCert.API.Models;

namespace TestCert.API.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly TestCertContext _context;

    public EquipmentRepository(TestCertContext context)
    {
        _context = context;
    }

    public async Task<Equipment?> GetEquipmentByIdAsync(int id)
    {
        return await _context.Equipments.FindAsync(id);
    }

    public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipmentsAsync()
    {
        return await _context.Equipments.ToListAsync();
    }
    public async Task AddEquipmentAsync(Equipment equipment)
    {
        await _context.Equipments.AddAsync(equipment);
    }

    public async Task UpdateEquipmentAsync(Equipment equipment)
    {
        _context.Equipments.Update(equipment);
    }
    public async Task DeleteEquipmentAsync(Equipment equipment)
    {
        _context.Equipments.Remove(equipment);
    }
    public async Task SaveChangesEquipmentAsync()
    {
        await _context.SaveChangesAsync();
    }
}
