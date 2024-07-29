using Application.DTOs.PetDTOs;
using Application.Persistence.Repositories;
using AutoMapper;
using Domain.Entities;

namespace Application.Additional;

public class PetAdditional
{
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;

    public PetAdditional(IPetRepository petRepository,IMapper mapper)
    {
        _petRepository = petRepository;
        _mapper = mapper;
    }

    public async Task PetUpdate(UpsertPetDto petDto)
    {
        var pet = _mapper.Map<Pet>(petDto);
        _petRepository.UpdateAsync(pet);
        await _petRepository.SaveChangesAsync();
    }


}