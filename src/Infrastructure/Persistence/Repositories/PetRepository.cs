﻿using System.Linq.Expressions;
using Application.Persistence.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class PetRepository:Repository<Pet>,IPetRepository
{
    public PetRepository(PetsonContext dbContext) : base(dbContext)
    {
    }
    
}