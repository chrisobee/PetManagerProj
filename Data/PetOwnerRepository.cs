using PetManager.Contracts;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Data
{
    public class PetOwnerRepository : RepositoryBase<PetOwner>, IPetOwnerRepository
    {
        public PetOwnerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreatePetOwner() => Create()

        public void DeletePetOwner()
        {
            throw new NotImplementedException();
        }

        public void EditPetOwner()
        {
            throw new NotImplementedException();
        }

        public Task<PetOwner> FindOwner(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
