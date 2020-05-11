using PetManager.Contracts;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Data
{
    public class PetRepository : RepositoryBase<Pet>, IPetRepository
    {
        public PetRepository(ApplicationDbContext applicationDbContext) :base(applicationDbContext)
        {
        }

        public void CreatePet(Pet pet) => Create(pet);
    }
}
