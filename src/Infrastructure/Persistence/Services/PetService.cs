﻿using Application.Additional;
using Application.DTOs.PetDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;

public class PetService:IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;
    private PetAdditional _petAdditional;

    public PetService(IPetRepository petRepository,IMapper mapper, PetAdditional petAdditional)
    {
        _petRepository = petRepository;
        _mapper = mapper;
        _petAdditional = petAdditional;
    }
    
    public IEnumerable<PetDto> GetAllUsers()
    {
        var pets = _petRepository.GetAllAsync();
        var petsDto = _mapper.Map<IEnumerable<PetDto>>(pets);
        return petsDto;
    }

    public async Task<IEnumerable<PetDto>> GetPetBySpecies(string species)
    {
        var pets = await _petRepository.GetOneOrManyPets(obj => obj.Species == species);
        var petsDto = _mapper.Map<IEnumerable<PetDto>>(pets);
        return petsDto;
    }

    public async  Task<IEnumerable<PetDto>> GetPetByType(string type)
    {
        var pets = await _petRepository.GetOneOrManyPets(obj => obj.TypeOfPet == type);
        var petsDto = _mapper.Map<IEnumerable<PetDto>>(pets);
        return petsDto;
    }

    public async Task AddPet(UpsertPetDto petDto)
    {
        var pet = _mapper.Map<Pet>(petDto);
        await _petRepository.InsertAsync(pet);
        await _petRepository.SaveChangesAsync();
    }

    public async Task UpdatePetName(string petName, UpsertPetDto petDto)
    {
        petDto.Name = petName;
        await _petAdditional.PetUpdate(petDto);
    }

    public async Task UpdateDescription(string text, UpsertPetDto petDto)
    {
        petDto.Description = text;
        await _petAdditional.PetUpdate(petDto);
    }

    public async Task DeletePetFromSite(int id)
    {
        var pet = await _petRepository.GetOneAsync(obj => obj.Id == id);
        _petRepository.DeleteAsync(pet);
        await _petRepository.SaveChangesAsync();
    }
}