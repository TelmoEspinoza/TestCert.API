using System;
using Microsoft.AspNetCore.Mvc;
using TestCert.API.DTOs;
using TestCert.API.Models;
using TestCert.API.Repositories;

namespace TestCert.API.Services;

public class EquipmentService : IEquipmentService
{

    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentService(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetEquipmentAsync()
    {
        // var listTest = await _testRepository.GetTestsAsync();
        var equipmentsDto = await _equipmentRepository.GetEquipmentsAsync() ?? throw new Exception("No Tests found !!");//x => ToTestDto(x)).ToListAsync();

        return equipmentsDto.Value?.Select(x => ToEquipmentDto(x)).ToList() ?? [];
    }

    public async Task<EquipmentDto?> GetEquipmentByIdAsync(int id)
    {
        var equipment = await _equipmentRepository.GetEquipmentByIdAsync(id);
        return equipment is null ? null : ToEquipmentDto(equipment);
    }

    public async Task<EquipmentDto?> CreateEquipmentAsync(EquipmentDto equipmentDto)
    {
        try
        {
            var newEquipment = new Equipment
            {
                EquipKtId = equipmentDto.EquipKtId,
                EquipDescription = equipmentDto.EquipDescription,
                EquipActive = true,
                EquipDateCreate = DateTime.Now,
                EquipLastTest = equipmentDto.EquipLastTest,
                EquipPlasticTagId = equipmentDto.EquipPlasticTagId,
                Price = equipmentDto.Price,
                Stock = equipmentDto.Stock
            };
            await _equipmentRepository.AddEquipmentAsync(newEquipment);
            await _equipmentRepository.SaveChangesEquipmentAsync();
            return ToEquipmentDto(newEquipment);

        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public async Task UpdateEquipmentAsync(EquipmentDto equipmentDto)
    {
        try
        {

            var updatedEquipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentDto.Id);
            if (updatedEquipment == null)
                throw new Exception("No equipment data found! Please try again");
            updatedEquipment.EquipKtId = equipmentDto.EquipKtId;
            updatedEquipment.EquipDescription = equipmentDto.EquipDescription;
            updatedEquipment.EquipActive = true;
            updatedEquipment.EquipLastTest = equipmentDto.EquipLastTest;
            updatedEquipment.EquipPlasticTagId = equipmentDto.EquipPlasticTagId;
            updatedEquipment.Price = equipmentDto.Price;
            updatedEquipment.Stock = equipmentDto.Stock;
            await _equipmentRepository.UpdateEquipmentAsync(updatedEquipment);
            await _equipmentRepository.SaveChangesEquipmentAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    /// <summary>
    /// Deleting <see cref="Equipment"/>
    /// </summary> Function to delete an equipment.
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteEquipmentAsync(int id)
    {
        try
        {
            var deletingEquipment = await _equipmentRepository.GetEquipmentByIdAsync(id);
            if (deletingEquipment == null)
                throw new Exception("No equipment data found! Please try again");
            await _equipmentRepository.DeleteEquipmentAsync(deletingEquipment);
            await _equipmentRepository.SaveChangesEquipmentAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }


    private EquipmentDto ToEquipmentDto(Equipment equipment)
    {
        return new EquipmentDto
        {
            Id = equipment.Id,
            EquipKtId = equipment.EquipKtId,
            EquipDescription = equipment.EquipDescription,
            EquipActive = equipment.EquipActive,
            EquipDateCreate = equipment.EquipDateCreate,
            EquipLastTest = equipment.EquipLastTest,
            EquipPlasticTagId = equipment.EquipPlasticTagId,
            Price = equipment.Price,
            Stock = equipment.Stock


        };
    }










}
