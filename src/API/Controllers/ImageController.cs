using Application.ActionFilters;
using Application.ActionFilters.PetFilters;
using Application.DTOs.ImageDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageService imageService,IImageRepository imageRepository)
        {
            _imageService = imageService;
            _imageRepository = imageRepository;
        }

        [HttpGet("{petId}")]
        public IActionResult GetImages([FromRoute]int petId)
        {
            var images = _imageService.GetAllImages(petId);
            return Ok(images);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage([FromBody]UpsertImage upsertImage)
        {
            await _imageService.AddImageAsync(upsertImage);
            return Ok("Image successfully added");
        }

        [HttpPatch("change-url/{id}")]
        public async Task ChangePhoto(string url, [FromRoute]int id)
        {
            var image = await _imageRepository.GetOneAsync(obj => obj.Id ==id);
            await _imageService.UpdateImageAsync(url,image);
        }
    }
}
