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
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<ToDoTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AnimalType>()
                .HasData(
                    new AnimalType { Name = "Dog" },
                    new AnimalType { Name = "Cat" },
                    new AnimalType { Name = "Small Mammal" },
                    new AnimalType { Name = "Fish" },
                    new AnimalType { Name = "Bird" },
                    new AnimalType { Name = "Equine" }
                    );

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Name = "Pet Owner",
                        NormalizedName = "PET OWNER"
                    }
            );
        }
    }
}
