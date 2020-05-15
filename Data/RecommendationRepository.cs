using PetManager.Contracts;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Data
{
    public class RecommendationRepository:RepositoryBase<Recommendation>, IRecommendationRepository
    {
        public RecommendationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
