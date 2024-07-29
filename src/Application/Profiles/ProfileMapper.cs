using Application.DTOs.PetDTOs;
using Application.DTOs.UserDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class ProfileMapper:Profile
{
    public ProfileMapper()
    {
        CreateMap<User,UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<Pet, PetDto>();
        CreateMap<UpsertPetDto, Pet>();

    }
}