using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface IAnimalTypeRepository : IRepositoryBase<AnimalType>
    {
        Task<List<AnimalType>> GetAnimalTypes();
        Task<AnimalType> GetAnimalTypeById(int? id);
    }
}
