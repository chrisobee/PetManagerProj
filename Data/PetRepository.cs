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
        public async Task<Pet> GetPet(int? id)
        {
            var pet = await FindByCondition(p => p.PetId.Equals(id));
            var SinglePet = pet.SingleOrDefault();
            return SinglePet;
        }
        public void EditPet(Pet pet) => Update(pet);
        public void DeletePet(Pet pet) => Delete(pet);
    }
}
