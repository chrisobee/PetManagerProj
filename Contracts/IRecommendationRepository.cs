using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface IRecommendationRepository: IRepositoryBase<Recommendation>
    {
        Task<List<Recommendation>> GetRecommendation(int animalTypeId);
    }
}
