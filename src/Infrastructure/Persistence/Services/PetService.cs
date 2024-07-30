﻿using Application.Additional;
using Application.DTOs.ImageDTOs;
using Application.DTOs.PetDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;

public class PetService:IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    private PetAdditional _petAdditional;

    public PetService(IPetRepository petRepository,IMapper mapper, PetAdditional petAdditional,IImageService imageService)
    {
        _petRepository = petRepository;
        _mapper = mapper;
        _petAdditional = petAdditional;
        _imageService = imageService;
    }
    
    public IEnumerable<PetDto> GetAllPets()
    {
        var pets = _petRepository.GetAllAsync();
        var petsDto = _mapper.Map<IEnumerable<PetDto>>(pets);
        return petsDto;
    }

    public async Task<IEnumerable<PetDto>> GetPetBySpecies(string species)
    {
        var pets = _petRepository.GetAllAsync(obj => obj.Species == species);
        var petsDto = _mapper.Map<IEnumerable<PetDto>>(pets);
        return petsDto;
    }

    public async  Task<IEnumerable<PetDto>> GetPetByType(string type)
    {
        var pets = _petRepository.GetAllAsync(obj => obj.TypeOfPet == type);
        var petsDto = _mapper.Map<IEnumerable<PetDto>>(pets);
        return petsDto;
    }

    public async Task AddPet(UpsertPetDto petDto)
    {
        var pet = _mapper.Map<Pet>(petDto);
        await _petRepository.InsertAsync(pet);
        await _petRepository.SaveChangesAsync();
    }

    public async Task UpdatePetName(string petName, Pet pet)
    {
        pet.Name = petName;
        await _petAdditional.PetUpdate(pet);
    }

    public async Task UpdateDescription(string text, Pet pet)
    {
        pet.Description = text;
        await _petAdditional.PetUpdate(pet);
    }

    public async Task DeletePetFromSite(Pet pet)
    {
        _petRepository.DeleteAsync(pet);
        await _petRepository.SaveChangesAsync();
    }

    public IEnumerable<ImageDto> GetAllImages(int id)
    {
        var images = _imageService.GetAllImages(id);
        return images;
    }
}