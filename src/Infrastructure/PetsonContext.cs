using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class PetsonContext:DbContext
{
    public PetsonContext(DbContextOptions<PetsonContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Reserve> Reserves { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData([
            new Role {Id = 1, RoleName = Application.Enums.RoleEnum.Customer.ToString()},
            new Role {Id = 2, RoleName = Application.Enums.RoleEnum.Admin.ToString()}
        ]);
        
    }
    

    
    
}