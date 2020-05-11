using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface IPetOwnershipRepository : IRepositoryBase<PetHumanJxn>
    {
        void Create(int petOwnerId, int petId);
    }
}
