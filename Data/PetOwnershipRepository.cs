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
    }
}
