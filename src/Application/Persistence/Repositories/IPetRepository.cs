using System.Linq.Expressions;
using Application.Persistence.Repositories.Common;
using Domain.Entities;

namespace Application.Persistence.Repositories;

public interface IPetRepository:IRepository<Pet>
{
}