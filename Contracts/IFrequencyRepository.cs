﻿using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface IFrequencyRepository : IRepositoryBase<Frequency>
    {
        Task<List<Frequency>> GetFrequencies();
        Task<int> GetFrequencyByIntervalName(string interval);
    }
}
