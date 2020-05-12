using PetManager.Contracts;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Data
{
    public class AnimalTypeRepository : RepositoryBase<AnimalType>, IAnimalTypeRepository
    {
        public AnimalTypeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<AnimalType>> GetAnimalTypes()
        {
            var results = await FindAll();
            var types = results.ToList();
            return types;
        }

        public async Task<AnimalType> GetAnimalTypeById(int? id)
        {
            var results = await FindByCondition(p => p.AnimalTypeId == id);
            var animalType = results.SingleOrDefault();
            return animalType;
        }
    }
}
