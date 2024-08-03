﻿using Application.DTOs.ImageDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;

public class ImageService:IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public ImageService(IImageRepository imageRepository,IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;

    }
    public IEnumerable<ImageDto> GetAllImages(int petId)
    {
        var images = _imageRepository.GetAllAsync(obj => obj.PetId == petId);
        var imagesDto = _mapper.Map<IEnumerable<ImageDto>>(images);
        return imagesDto;
    }

    public async Task AddImage(UpsertImage imageDto)
    {
        var image = _mapper.Map<Image>(imageDto);
        await _imageRepository.InsertAsync(image);
        await _imageRepository.SaveChangesAsync();
    }

    public async Task UpdateImage(string url, Image image)
    {
        image.Url = url;
        _imageRepository.UpdateAsync(image);
        await _imageRepository.SaveChangesAsync();
    }
}