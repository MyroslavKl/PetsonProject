using Application.Additional.Pet;
using Application.DTOs.ImageDTOs;
using Application.DTOs.PetDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using AutoMapper;
using CacheServices.Service;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;

public class PetService:IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    private readonly PetAdditional _petAdditional;
    private readonly ICacheService _cacheService;

    public PetService(IPetRepository petRepository,
        IMapper mapper, 
        PetAdditional petAdditional,
        IImageService imageService,
        ICacheService cacheService)
    {
        _petRepository = petRepository;
        _mapper = mapper;
        _petAdditional = petAdditional;
        _imageService = imageService;
        _cacheService = cacheService;
    }
    
    public IEnumerable<PetDto> GetAllPets()
    {
        var cacheDataPets = _cacheService.GetData<IEnumerable<PetDto>>("pet");
        if (cacheDataPets != null && cacheDataPets.Count() > 0)
        {
            return cacheDataPets;
        }
        var pets = _petRepository.GetAll();
        cacheDataPets = _mapper.Map<IEnumerable<PetDto>>(pets);
        
        var expirationTime = DateTime.Now.AddMinutes(3);
        _cacheService.SetData("pet",cacheDataPets,expirationTime);
        return cacheDataPets;
    }

    public async Task<IEnumerable<PetDto>> GetPetBySpeciesAsync(string species)
    {
        var pets = _petRepository.GetAll(obj => obj.Species == species);
        var petsDto = _mapper.Map<IEnumerable<PetDto>>(pets);
        return petsDto;
    }

    public async  Task<IEnumerable<PetDto>> GetPetByTypeAsync(string type)
    {
        var pets = _petRepository.GetAll(obj => obj.TypeOfPet == type);
        var petsDto = _mapper.Map<IEnumerable<PetDto>>(pets);
        return petsDto;
    }

    public async Task AddPetAsync(UpsertPetDto petDto)
    {
        var pet = _mapper.Map<Pet>(petDto);
        await _petRepository.InsertAsync(pet);
        await _petRepository.SaveChangesAsync();
        var dto = _mapper.Map<PetDto>(pet);
        var expirationTime = DateTime.Now.AddMinutes(3);
        _cacheService.SetData($"pet{pet.Id}",dto,expirationTime);
    }

    public async Task UpdatePetNameAsync(string petName, Pet pet)
    {
        var petCache = _cacheService.GetData<PetDto>($"pet{pet.Id}");
        var petDto = _mapper.Map<PetDto>(pet);
        var expirationTime = DateTime.Now.AddMinutes(3);
        pet.Name = petName;
        await _petAdditional.PetUpdate(pet);
        if (petCache != null)
        {
            _cacheService.RemoveData($"pet{pet.Id}");
            _cacheService.SetData($"pet{pet.Id}",petDto,expirationTime);
        }
        else
        {
            _cacheService.SetData($"pet{pet.Id}",petDto,expirationTime);
        }
        
    }

    public async Task UpdateDescriptionAsync(string text, Pet pet)
    {
        var petCache = _cacheService.GetData<PetDto>($"pet{pet.Id}");
        var petDto = _mapper.Map<PetDto>(pet);
        var expirationTime = DateTime.Now.AddMinutes(3);
        pet.Description = text;
        await _petAdditional.PetUpdate(pet);
        if (petCache != null)
        {
            _cacheService.RemoveData($"pet{pet.Id}");
            _cacheService.SetData($"pet{pet.Id}",petDto,expirationTime);
        }
        else
        {
            _cacheService.SetData($"pet{pet.Id}",petDto,expirationTime);
        }

    }

    public async Task DeletePetFromSiteAsync(Pet pet)
    {
        _petRepository.Delete(pet);
        await _petRepository.SaveChangesAsync();
        _cacheService.RemoveData($"pet{pet.Id}");
    }

    public IEnumerable<ImageDto> GetAllImages(int id)
    {
        var images = _imageService.GetAllImages(id);
        return images;
    }
}