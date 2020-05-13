using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetManager.Models;

namespace PetManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PetOwner> PetOwners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetHumanJxn> PetOwnerships { get; set; }
        public DbSet<ContactsJxn> Contacts { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<ToDoTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AnimalType>()
                .HasData(
                    new AnimalType { AnimalTypeId = 1, Name = "Dog" },
                    new AnimalType { AnimalTypeId = 2, Name = "Cat" },
                    new AnimalType { AnimalTypeId = 3, Name = "Small Mammal" },
                    new AnimalType { AnimalTypeId = 4, Name = "Fish" },
                    new AnimalType { AnimalTypeId = 5, Name = "Bird" },
                    new AnimalType { AnimalTypeId = 6, Name = "Equine" }
                    ); ;

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = "66d0c95c-c9b7-4471-bdee-3ff098b038a6",
                        Name = "Pet Owner",
                        NormalizedName = "PET OWNER"
                    }
            );
        }
    }
}
