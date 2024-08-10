using Application.ActionFilters;
using Application.ActionFilters.PetFilters;
using Application.DTOs.PetDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IPetRepository _petRepository;

        public PetController(IPetService petService, IPetRepository petRepository)
        {
            _petService = petService;
            _petRepository = petRepository;
        }

        [HttpGet]
        public IEnumerable<PetDto> GetPets()
        {
            var pets = _petService.GetAllPets();
            return pets;
        }

        [HttpGet("petSpecies")]
        [TypeFilter(typeof(PetExistBySpeciesFilterAttribute))]
        public async Task<IEnumerable<PetDto>> GetPetsBySpecies(string species)
        {
            var pets = await _petService.GetPetBySpeciesAsync(species);
            return pets;
        }
        
        [HttpGet("petType")]
        [TypeFilter(typeof(PetExistByTypeFilterAttribute))]
        public async Task<IEnumerable<PetDto>> GetPetsByType(string type)
        {
            var pets = await _petService.GetPetByTypeAsync(type);
            return pets;
        }

        [HttpPost]
        [ModelStateFilter]
        public async Task AddPet([FromBody]UpsertPetDto pet)
        {
            await _petService.AddPetAsync(pet);
        }

        [HttpPatch("name-update/{id}")]
        [TypeFilter(typeof(PetExistByIdFilterAttribute))]
        public async Task UpdateName([FromRoute]int id,[FromBody]string name)
        {
            var pet = await _petRepository.GetOneAsync(obj => obj.Id == id);
            await _petService.UpdatePetNameAsync(name,pet);
        }
        [HttpPatch("description-update/{id}")]
        [TypeFilter(typeof(PetExistByIdFilterAttribute))]
        public async Task UpdateDescription([FromRoute]int id,[FromBody]string description)
        {
            var pet = await _petRepository.GetOneAsync(obj => obj.Id == id);
            await _petService.UpdateDescriptionAsync(description,pet);
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(PetExistByIdFilterAttribute))]
        public async Task<IActionResult> DeletePet([FromRoute] int id)
        {
            var pet = await _petRepository.GetOneAsync(obj => obj.Id == id);
            await _petService.DeletePetFromSiteAsync(pet);
            return Ok("Pet is deleted successfully");
        }
    }
}
