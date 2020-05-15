using PetManager.Contracts;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Data
{
    public class FrequencyRepository : RepositoryBase<Frequency>, IFrequencyRepository
    {
        public FrequencyRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<Frequency>> GetFrequencies()
        {
            var results = await FindAll();
            List<Frequency> frequencies = results.ToList();
            return frequencies;
        }
    }
}
