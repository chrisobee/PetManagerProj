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

        public async Task<int> GetFrequencyByIntervalName(string interval)
        {
            var result = await FindByCondition(f => f.Interval == interval);
            var frequency = result.Select(f => f.FrequencyId).FirstOrDefault();
            return frequency;
        }
    }
}
