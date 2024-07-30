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

        public async Task<IEnumerable<PetDto>> GetPetsBySpecies([FromBody]string species)
        {
            var pets = await _petService.GetPetBySpecies(species);
            return pets;
        }
        [HttpGet("petType")]

        public async Task<IEnumerable<PetDto>> GetPetsByType([FromBody]string type)
        {
            var pets = await _petService.GetPetByType(type);
            return pets;
        }

        [HttpPost]
        public async Task AddPet([FromBody]UpsertPetDto pet)
        {
            await _petService.AddPet(pet);
        }

        [HttpPatch("name-update/{id}")]
        public async Task UpdateName([FromRoute]int id,[FromBody]string name)
        {
            var pet = await _petRepository.GetOneAsync(obj => obj.Id == id);
            await _petService.UpdatePetName(name,pet);
        }
        [HttpPatch("description-update/{id}")]
        public async Task UpdateDescription([FromRoute]int id,[FromBody]string description)
        {
            var pet = await _petRepository.GetOneAsync(obj => obj.Id == id);
            await _petService.UpdateDescription(description,pet);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet([FromRoute] int id)
        {
            var pet = await _petRepository.GetOneAsync(obj => obj.Id == id);
            await _petService.DeletePetFromSite(pet);
            return Ok("Pet is deleted successfully");
        }
    }
}
