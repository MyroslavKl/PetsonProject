using Application.DTOs.ImageDTOs;
using Application.DTOs.PetDTOs;
using Application.DTOs.ReserveDTOs;
using Application.DTOs.UserDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class ProfileMapper:Profile
{
    public ProfileMapper()
    {
        CreateMap<User,UserDto>().ReverseMap();
        CreateMap<CreateUserDto, User>();
        CreateMap<Pet, PetDto>().ReverseMap();
        CreateMap<UpsertPetDto, Pet>();
        CreateMap<UpsertReserveDto, Reserve>();
        CreateMap<ReserveDto, Reserve>().ReverseMap();
        CreateMap<UpsertImage, Image>();
        CreateMap<ImageDto, Image>().ReverseMap();

    }
}