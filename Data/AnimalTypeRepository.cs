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

        public async Task<IEnumerable<AnimalType>> GetAnimalTypes()
        {
            var results = await FindAll();
            var types = results.ToList();
            return types;
        }
    }
}
