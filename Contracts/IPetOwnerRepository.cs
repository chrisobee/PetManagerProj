using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface IPetOwnerRepository : IRepositoryBase<PetOwner>
    {
        Task<PetOwner> FindOwner(string userId);
        Task<int> FindOwnerId(string userId);
        void CreatePetOwner(PetOwner petOwner);
        void EditPetOwner(PetOwner petOwner);
        void DeletePetOwner(PetOwner petOwner);
    }
}
