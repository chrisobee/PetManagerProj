using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface IPetRepository : IRepositoryBase<Pet>
    {
        void CreatePet(Pet pet);
        Task<Pet> GetPet(int? id);
        void EditPet(Pet pet);
        void DeletePet(Pet pet);        
    }
}
