using PetManager.Models;
using PetManager.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Data
{
    public class PetOwnershipRepository : RepositoryBase<PetHumanJxn>, IPetOwnershipRepository
    {
        public PetOwnershipRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
        }

        public void Create(int petOwnerId, int petId)
        {
            PetHumanJxn ownership = new PetHumanJxn
            {
                PetOwnerId = petOwnerId,
                PetId = petId
            };
            Create(ownership);
        }

        public async Task<List<int>> FindAllPets(int petOwnerId)
        {
            var results = await FindByCondition(p => p.PetOwnerId == petOwnerId);
            var pets = results.Select(p => p.PetId).ToList();
            return pets;
        }
    }
}
