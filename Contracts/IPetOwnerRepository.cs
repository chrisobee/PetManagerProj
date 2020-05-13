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
        Task<PetOwner> FindOwnerWithId(int? id);
        Task<int> FindOwnerId(string userId);
        Task<PetOwner> FindOwnerByEmail(string email);
        void CreatePetOwner(PetOwner petOwner);
        void EditPetOwner(PetOwner petOwner);
        void DeletePetOwner(PetOwner petOwner);
    }
}
