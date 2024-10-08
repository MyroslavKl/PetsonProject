﻿using Application.DTOs.PetDTOs;
using Application.Persistence.Repositories;
using AutoMapper;
using Domain.Entities;

namespace Application.Additional.Pet;

public class PetAdditional
{
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;

    public PetAdditional(IPetRepository petRepository,IMapper mapper)
    {
        _petRepository = petRepository;
        _mapper = mapper;
    }

    public async Task PetUpdate(Domain.Entities.Pet pet)
    {
        _petRepository.Update(pet);
        await _petRepository.SaveChangesAsync();
    }


}