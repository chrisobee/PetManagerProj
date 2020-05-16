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
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }

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
                    );
            builder.Entity<Frequency>()
                .HasData(
                    new Frequency { FrequencyId = 1, Interval = "Daily" },
                    new Frequency { FrequencyId = 2, Interval = "Weekly" },
                    new Frequency { FrequencyId = 3, Interval = "Monthly" }
                    );
            builder.Entity<Recommendation>()
                .HasData(
                    new Recommendation { RecommendationId = 1, TaskName = "Go on Walk", Interval = "Weekly", AnimalTypeId = 1 },
                    new Recommendation { RecommendationId = 2, TaskName = "Give Bath", Interval = "Weekly", AnimalTypeId = 1 },
                    new Recommendation { RecommendationId = 3, TaskName = "Brush Cat", Interval = "Weekly", AnimalTypeId = 2 },
                    new Recommendation { RecommendationId = 4, TaskName = "Clean Litter Box", Interval = "Daily", AnimalTypeId = 2  },
                    new Recommendation { RecommendationId = 5, TaskName = "Clean Tank/Cage", Interval = "Monthly", AnimalTypeId = 3 },
                    new Recommendation { RecommendationId = 6, TaskName = "Clean Aquarium", Interval = "Weekly", AnimalTypeId = 4 },
                    new Recommendation { RecommendationId = 7, TaskName = "Clean Cage", Interval = "Weekly", AnimalTypeId = 5 },
                    new Recommendation { RecommendationId = 8, TaskName = "Replace Horseshoes", Interval = "Monthly", AnimalTypeId = 6}
                    );

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
